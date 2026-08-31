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
public class PageRevisions_Tests
{
    private const string RevisionJson = """
        {
          "id": 73,
          "parent": 42,
          "author": 3,
          "title": { "raw": "Previous title", "rendered": "Previous title" },
          "content": { "raw": "Previous content", "rendered": "Previous content" }
        }
        """;

    [TestMethod]
    public async Task ListGetAndDelete_UseAuthenticatedPageRevisionRoutesAndModels()
    {
        RecordingHandler handler = new($"[{RevisionJson}]", RevisionJson, "{}");
        using HttpClient httpClient = new(handler)
        {
            BaseAddress = new Uri("https://example.com/wp-json/")
        };
        using WordPressClient client = new(httpClient);
        client.Auth.UseBasicAuth("user", "application password");
        Client.PageRevisions revisionsClient = client.Pages.Revisions(42);

        List<PostRevision> revisions = await revisionsClient.GetAllAsync(embed: true);
        PostRevision revision = await revisionsClient.GetByIdAsync(73);
        bool deleted = await revisionsClient.DeleteAsync(73);

        Assert.HasCount(1, revisions);
        Assert.AreEqual(73, revisions[0].Id);
        Assert.AreEqual(42, revisions[0].Parent);
        Assert.AreEqual("Previous content", revision.Content?.Raw);
        Assert.IsTrue(deleted);
        AssertRequest(handler.Requests[0], HttpMethod.Get, "https://example.com/wp-json/wp/v2/pages/42/revisions?_embed");
        AssertRequest(handler.Requests[1], HttpMethod.Get, "https://example.com/wp-json/wp/v2/pages/42/revisions/73");
        AssertRequest(handler.Requests[2], HttpMethod.Delete, "https://example.com/wp-json/wp/v2/pages/42/revisions/73?force=true");
    }

    private static void AssertRequest(CapturedRequest request, HttpMethod method, string uri)
    {
        Assert.AreEqual(method, request.Method);
        Assert.AreEqual(uri, request.Uri?.OriginalString);
        Assert.AreEqual("Basic", request.AuthorizationScheme);
    }

    private sealed record CapturedRequest(HttpMethod Method, Uri? Uri, string? AuthorizationScheme);

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
