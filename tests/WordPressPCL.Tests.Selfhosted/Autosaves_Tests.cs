using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Autosaves_Tests
{
    private const string AutosaveJson = """
        {
          "id": 71,
          "parent": 42,
          "author": 3,
          "title": { "raw": "Autosaved title", "rendered": "Autosaved title" },
          "content": { "raw": "Autosaved content", "rendered": "Autosaved content" }
        }
        """;

    [TestMethod]
    public async Task PostAutosaves_ListGetAndCreate_UseAuthenticatedPostRoutesAndSerializerOptions()
    {
        RecordingHandler handler = new($"[{AutosaveJson}]", AutosaveJson, AutosaveJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);
        client.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        client.JsonSerializerOptions.WriteIndented = true;

        List<PostRevision> autosaves = await client.Posts.Autosaves(42).GetAsync(embed: true);
        PostRevision autosave = await client.Posts.Autosaves(42).GetByIdAsync(71);
        PostRevision created = await client.Posts.Autosaves(42).CreateAsync(new Post
        {
            Title = new Title(string.Empty),
            Content = new Content("Draft content")
        });

        Assert.HasCount(1, autosaves);
        Assert.AreEqual(71, autosaves[0].Id);
        Assert.AreEqual(42, autosaves[0].Parent);
        Assert.AreEqual("Autosaved content", autosave.Content?.Raw);
        Assert.AreEqual(71, created.Id);
        AssertRequest(handler.Requests[0], HttpMethod.Get, "https://example.com/wp-json/wp/v2/posts/42/autosaves?_embed");
        AssertRequest(handler.Requests[1], HttpMethod.Get, "https://example.com/wp-json/wp/v2/posts/42/autosaves/71");
        AssertRequest(handler.Requests[2], HttpMethod.Post, "https://example.com/wp-json/wp/v2/posts/42/autosaves");
        Assert.AreEqual("application/json", handler.Requests[2].ContentType);
        Assert.Contains("\n", handler.Requests[2].Body!);

        using JsonDocument requestBody = JsonDocument.Parse(handler.Requests[2].Body!);
        Assert.AreEqual(string.Empty, requestBody.RootElement.GetProperty("title").GetString());
        Assert.AreEqual("Draft content", requestBody.RootElement.GetProperty("content").GetString());
        Assert.IsFalse(requestBody.RootElement.TryGetProperty("status", out _));
        Assert.IsFalse(requestBody.RootElement.TryGetProperty("password", out _));
    }

    [TestMethod]
    public async Task PageAutosaves_ListGetAndCreate_UseAuthenticatedPageRoutesAndSerializerOptions()
    {
        RecordingHandler handler = new($"[{AutosaveJson}]", AutosaveJson, AutosaveJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);
        client.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        List<PostRevision> autosaves = await client.Pages.Autosaves(42).GetAsync(embed: true);
        PostRevision autosave = await client.Pages.Autosaves(42).GetByIdAsync(71);
        PostRevision created = await client.Pages.Autosaves(42).CreateAsync(new Page
        {
            Title = new Title("Draft title"),
            Content = new Content("Draft content"),
            Status = Status.Draft
        });

        Assert.HasCount(1, autosaves);
        Assert.AreEqual(71, autosaves[0].Id);
        Assert.AreEqual(42, autosaves[0].Parent);
        Assert.AreEqual("Autosaved title", autosave.Title?.Raw);
        Assert.AreEqual(71, created.Id);
        AssertRequest(handler.Requests[0], HttpMethod.Get, "https://example.com/wp-json/wp/v2/pages/42/autosaves?_embed");
        AssertRequest(handler.Requests[1], HttpMethod.Get, "https://example.com/wp-json/wp/v2/pages/42/autosaves/71");
        AssertRequest(handler.Requests[2], HttpMethod.Post, "https://example.com/wp-json/wp/v2/pages/42/autosaves");
        Assert.AreEqual("application/json", handler.Requests[2].ContentType);

        using JsonDocument requestBody = JsonDocument.Parse(handler.Requests[2].Body!);
        Assert.AreEqual("Draft title", requestBody.RootElement.GetProperty("title").GetString());
        Assert.IsFalse(requestBody.RootElement.TryGetProperty("status", out _));
        Assert.IsFalse(requestBody.RootElement.TryGetProperty("menu_order", out _));
        Assert.IsFalse(requestBody.RootElement.TryGetProperty("slug", out _));
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

    private static void AssertRequest(CapturedRequest request, HttpMethod method, string uri)
    {
        Assert.AreEqual(method, request.Method);
        Assert.AreEqual(uri, request.Uri?.OriginalString);
        Assert.AreEqual("Basic", request.AuthorizationScheme);
    }

    private sealed record CapturedRequest(
        HttpMethod Method,
        Uri? Uri,
        string? AuthorizationScheme,
        string? ContentType,
        string? Body);

    private sealed class RecordingHandler(params string[] responseBodies) : HttpMessageHandler
    {
        private readonly Queue<string> _responseBodies = new(responseBodies);

        public List<CapturedRequest> Requests { get; } = [];

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string? body = request.Content is null
                ? null
                : await request.Content.ReadAsStringAsync(cancellationToken);
            Requests.Add(new CapturedRequest(
                request.Method,
                request.RequestUri,
                request.Headers.Authorization?.Scheme,
                request.Content?.Headers.ContentType?.MediaType,
                body));

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_responseBodies.Dequeue(), Encoding.UTF8, "application/json")
            };
        }
    }
}
