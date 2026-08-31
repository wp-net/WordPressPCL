using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class WidgetTypes_Tests
{
    private const string WidgetTypeJson = """
        {
          "id": "block",
          "name": "Block",
          "description": "A widget containing a block.",
          "is_multi": true,
          "classname": "widget_block",
          "_links": {
            "self": [{ "href": "https://example.com/wp-json/wp/v2/widget-types/block" }]
          },
          "plugin_field": 12
        }
        """;

    [TestMethod]
    public async Task ReadOperations_UseCollectionShapeEncodedStringIdsAndAuthenticationChoice()
    {
        RecordingHandler handler = new($"[{WidgetTypeJson}]", WidgetTypeJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        List<WidgetType> types = await client.WidgetTypes.GetAsync();
        WidgetType type = await client.WidgetTypes.GetByIdAsync(
            "legacy/type & custom",
            embed: true,
            useAuth: false);

        Assert.HasCount(1, types);
        Assert.AreEqual("block", type.Id);
        Assert.AreEqual("Block", type.Name);
        Assert.IsTrue(type.IsMulti);
        Assert.AreEqual("widget_block", type.Classname);
        Assert.AreEqual(
            "https://example.com/wp-json/wp/v2/widget-types/block",
            type.Links?.Self?[0].Href);
        Assert.AreEqual(12, type.AdditionalFields?["plugin_field"].GetInt32());
        AssertRequest(
            handler.Requests[0],
            "https://example.com/wp-json/wp/v2/widget-types",
            "Basic");
        AssertRequest(
            handler.Requests[1],
            "https://example.com/wp-json/wp/v2/widget-types/legacy%2Ftype%20%26%20custom?_embed",
            null);
    }

    [TestMethod]
    public void GetByIdAsync_RejectsMissingIdsBeforeSending()
    {
        RecordingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Assert.ThrowsExactly<ArgumentNullException>(() =>
            client.WidgetTypes.GetByIdAsync(null!));
        Assert.ThrowsExactly<ArgumentException>(() =>
            client.WidgetTypes.GetByIdAsync(" "));
        Assert.IsEmpty(handler.Requests);
    }

    private static WordPressClient CreateAuthenticatedClient(HttpClient httpClient)
    {
        WordPressClient client = new(httpClient);
        client.Auth.UseBasicAuth("user", "application password");
        return client;
    }

    private static HttpClient CreateHttpClient(HttpMessageHandler handler)
    {
        return new HttpClient(handler)
        {
            BaseAddress = new Uri("https://example.com/wp-json/")
        };
    }

    private static void AssertRequest(
        CapturedRequest request,
        string uri,
        string? authorizationScheme)
    {
        Assert.AreEqual(HttpMethod.Get, request.Method);
        Assert.AreEqual(uri, request.Uri?.OriginalString);
        Assert.AreEqual(authorizationScheme, request.AuthorizationScheme);
    }

    private sealed record CapturedRequest(
        HttpMethod Method,
        Uri? Uri,
        string? AuthorizationScheme);

    private sealed class RecordingHandler(params string[] responseBodies) : HttpMessageHandler
    {
        private readonly Queue<string> _responseBodies = new(responseBodies);

        public List<CapturedRequest> Requests { get; } = [];

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Requests.Add(new CapturedRequest(
                request.Method,
                request.RequestUri,
                request.Headers.Authorization?.Scheme));
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_responseBodies.Dequeue(), Encoding.UTF8, "application/json")
            });
        }
    }
}
