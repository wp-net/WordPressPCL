using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class MediaRequest_Tests
{
    [TestMethod]
    [DataRow("cat.jpg", "attachment; filename=cat.jpg")]
    [DataRow("photo 100% #1.jpg", "attachment; filename=\"photo 100% #1.jpg\"")]
    [DataRow("photo \"final\".jpg", "attachment; filename=\"photo _final_.jpg\"; filename*=utf-8''photo%20%22final%22.jpg")]
    [DataRow("中文.webp", "attachment; filename=__.webp; filename*=utf-8''%E4%B8%AD%E6%96%87.webp")]
    public async Task CreateAsync_Stream_WritesSafeContentDisposition(
        string filename,
        string expectedContentDisposition)
    {
        using CapturingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = new(httpClient);
        using MemoryStream stream = new([1, 2, 3]);

        await client.Media.CreateAsync(stream, filename);

        Assert.AreEqual(expectedContentDisposition, handler.ContentDisposition);
    }

    [TestMethod]
    public async Task CreateAsync_Path_WritesUnicodeContentDisposition()
    {
        string filePath = Path.GetTempFileName();
        try
        {
            await File.WriteAllBytesAsync(filePath, [1, 2, 3]);
            using CapturingHandler handler = new();
            using HttpClient httpClient = CreateHttpClient(handler);
            using WordPressClient client = new(httpClient);

            await client.Media.CreateAsync(filePath, "中文.webp");

            Assert.AreEqual(
                "attachment; filename=__.webp; filename*=utf-8''%E4%B8%AD%E6%96%87.webp",
                handler.ContentDisposition);
        }
        finally
        {
            File.Delete(filePath);
        }
    }

    [TestMethod]
    public async Task CreateAsync_RejectsHeaderInjection()
    {
        using CapturingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = new(httpClient);
        using MemoryStream stream = new([1, 2, 3]);

        await Assert.ThrowsExactlyAsync<FormatException>(
            () => client.Media.CreateAsync(stream, "photo.jpg\r\nX-Injected: true"));

        Assert.IsNull(handler.ContentDisposition);
    }

    [TestMethod]
    public async Task CreateAsync_RejectsInjectedMimeType()
    {
        using CapturingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = new(httpClient);
        using MemoryStream stream = new([1, 2, 3]);

        await Assert.ThrowsExactlyAsync<FormatException>(
            () => client.Media.CreateAsync(stream, "photo.jpg", "image/jpeg\r\nX-Injected: true"));

        Assert.IsNull(handler.ContentDisposition);
    }

    private static HttpClient CreateHttpClient(HttpMessageHandler handler)
    {
        return new HttpClient(handler)
        {
            BaseAddress = new Uri("https://example.org/wp-json/")
        };
    }

    private sealed class CapturingHandler : HttpMessageHandler
    {
        public string? ContentDisposition { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            ContentDisposition = request.Content?.Headers.ContentDisposition?.ToString();

            const string responseBody = """
                {
                    "id": 7
                }
                """;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseBody, Encoding.UTF8, "application/json")
            });
        }
    }
}
