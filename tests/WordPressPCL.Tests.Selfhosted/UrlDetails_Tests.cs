using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class UrlDetails_Tests
{
    [TestMethod]
    public async Task GetAsync_EncodesUrlUsesAuthenticationAndDeserializesMetadata()
    {
        RecordingHandler handler = new("""
            {
              "title": "Example",
              "icon": "https://example.net/favicon.ico",
              "description": "An example page",
              "image": "https://example.net/preview.jpg",
              "og_description": "Open Graph description",
              "og_title": "Open Graph title",
              "og_image": [
                {
                  "width": 1200,
                  "height": 630,
                  "url": "https://example.net/og.jpg",
                  "type": "image/jpeg"
                }
              ]
            }
            """);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = new(httpClient);
        client.Auth.UseBasicAuth("user", "application password");

        UrlDetails details = await client.UrlDetails.GetAsync("https://example.net/article?q=hello world&lang=en#content");

        Assert.AreEqual(
            "https://example.com/wp-json/wp-block-editor/v1/url-details?url=https%3A%2F%2Fexample.net%2Farticle%3Fq%3Dhello%20world%26lang%3Den%23content",
            handler.LastRequestUri?.OriginalString);
        Assert.AreEqual("Basic", handler.LastAuthorizationScheme);
        Assert.AreEqual("Example", details.Title);
        Assert.AreEqual("https://example.net/favicon.ico", details.Icon);
        Assert.AreEqual("An example page", details.Description);
        Assert.AreEqual("https://example.net/preview.jpg", details.Image);
        Assert.AreEqual("Open Graph description", details.OgDescription);
        Assert.AreEqual("Open Graph title", details.OgTitle);
        Assert.IsNotNull(details.OgImage);
        Assert.HasCount(1, details.OgImage);
        Assert.AreEqual(1200, details.OgImage[0].Width);
        Assert.AreEqual(630, details.OgImage[0].Height);
        Assert.AreEqual("https://example.net/og.jpg", details.OgImage[0].Url);
        Assert.AreEqual("image/jpeg", details.OgImage[0].Type);
    }

    [TestMethod]
    public void GetAsync_RejectsEmptyUrl()
    {
        RecordingHandler handler = new("{}");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = new(httpClient);

        Assert.ThrowsExactly<ArgumentException>(
            () => client.UrlDetails.GetAsync(string.Empty));
        Assert.IsNull(handler.LastRequestUri);
    }

    private static HttpClient CreateHttpClient(HttpMessageHandler handler)
    {
        return new HttpClient(handler)
        {
            BaseAddress = new Uri("https://example.com/wp-json/")
        };
    }

    private sealed class RecordingHandler(string body) : HttpMessageHandler
    {
        public Uri? LastRequestUri { get; private set; }

        public string? LastAuthorizationScheme { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequestUri = request.RequestUri;
            LastAuthorizationScheme = request.Headers.Authorization?.Scheme;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            });
        }
    }
}
