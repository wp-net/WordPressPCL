using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted.Models;

[TestClass]
public class CustomFieldsRequest_Tests
{
    [TestMethod]
    public async Task CreateAsync_WritesCustomFieldsToRequest()
    {
        using CapturingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = new(httpClient);
        Post post = new()
        {
            Slug = "a-post",
            CustomFields = new Dictionary<string, object>
            {
                ["checksum"] = "123",
                ["gallery"] = new[] { 1, 2, 3 },
            },
        };

        await client.Posts.CreateAsync(post);

        using JsonDocument request = JsonDocument.Parse(handler.LastRequestBody!);
        Assert.AreEqual("123", request.RootElement.GetProperty("checksum").GetString());
        Assert.AreEqual(3, request.RootElement.GetProperty("gallery").GetArrayLength());
        Assert.IsFalse(request.RootElement.TryGetProperty("custom_fields", out _));
    }

    [TestMethod]
    public async Task UpdateAsync_WritesCustomFieldsReceivedFromApi()
    {
        using CapturingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = new(httpClient);

        Post post = await client.Posts.GetByIdAsync(7);
        Assert.IsNotNull(post.CustomFields);
        post.Slug = "updated";
        await client.Posts.UpdateAsync(post);

        using JsonDocument request = JsonDocument.Parse(handler.LastRequestBody!);
        Assert.AreEqual("from-api", request.RootElement.GetProperty("checksum").GetString());
        Assert.AreEqual("updated", request.RootElement.GetProperty("slug").GetString());
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
        public string? LastRequestBody { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Content is not null)
            {
                LastRequestBody = await request.Content.ReadAsStringAsync(cancellationToken);
            }

            const string responseBody = """
                {
                    "id": 7,
                    "slug": "a-post",
                    "checksum": "from-api"
                }
                """;
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseBody, Encoding.UTF8, "application/json")
            };
        }
    }
}
