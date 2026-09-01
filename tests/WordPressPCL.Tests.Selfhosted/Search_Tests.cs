using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Search_Tests
{
    [TestMethod]
    public async Task SearchAsync_EncodesTermAndDeserializesResults()
    {
        RecordingHandler handler = new("""
            [
              {
                "id": 42,
                "title": "Hello & world",
                "url": "https://example.com/hello-world/",
                "type": "post",
                "subtype": "post",
                "_links": {
                  "self": [{ "href": "https://example.com/wp-json/wp/v2/posts/42" }]
                }
              },
              {
                "id": "post-format-aside",
                "title": "Aside",
                "url": "https://example.com/type/aside/",
                "type": "post-format",
                "subtype": "post-format"
              }
            ]
            """);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = new(httpClient);

        List<SearchResult> results = await client.Search.SearchAsync("Hello & world");

        Assert.AreEqual("https://example.com/wp-json/wp/v2/search?search=Hello%20%26%20world", handler.LastRequestUri?.OriginalString);
        Assert.IsNull(handler.LastAuthorizationScheme);
        Assert.HasCount(2, results);
        Assert.AreEqual(42, results[0].Id.GetInt32());
        Assert.AreEqual("Hello & world", results[0].Title);
        Assert.AreEqual("https://example.com/hello-world/", results[0].Url);
        Assert.AreEqual("post", results[0].Type);
        Assert.AreEqual("post", results[0].Subtype);
        Assert.IsNotNull(results[0].Links?.Self);
        Assert.AreEqual("post-format-aside", results[1].Id.GetString());
    }

    [TestMethod]
    public async Task QueryAsync_BuildsCollectionFiltersAndUsesAuthenticationWhenRequested()
    {
        RecordingHandler handler = new("[]");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = new(httpClient);
        client.Auth.UseBasicAuth("user", "application password");
        SearchQueryBuilder query = new()
        {
            Search = "Hello World",
            Type = "post",
            Subtype = ["post", "page"],
            Exclude = [1, 2],
            Include = [3, 4],
            Page = 2,
            PerPage = 25
        };

        List<SearchResult> results = await client.Search.QueryAsync(query, useAuth: true);

        Assert.IsEmpty(results);
        Assert.AreEqual(
            "https://example.com/wp-json/wp/v2/search?search=Hello+World&type=post&subtype=post%2cpage&exclude=1%2c2&include=3%2c4&page=2&per_page=25&order=desc&context=view",
            handler.LastRequestUri?.OriginalString);
        Assert.AreEqual("Basic", handler.LastAuthorizationScheme);
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
