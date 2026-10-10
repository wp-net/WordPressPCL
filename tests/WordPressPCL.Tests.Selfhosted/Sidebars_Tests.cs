using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Sidebars_Tests
{
    private const string SidebarJson = """
        {
          "id": "sidebar-1",
          "name": "Primary Sidebar",
          "description": "Main widget area",
          "class": "primary",
          "before_widget": "<section>",
          "after_widget": "</section>",
          "before_title": "<h2>",
          "after_title": "</h2>",
          "status": "active",
          "widgets": ["block-7", "search-2"],
          "_links": {
            "self": [{ "href": "https://example.com/wp-json/wp/v2/sidebars/sidebar-1" }]
          },
          "plugin_field": "preserved"
        }
        """;

    [TestMethod]
    public async Task ReadOperations_DefaultToPublicAccessAndAllowExplicitAuthentication()
    {
        RecordingHandler handler = new($"[{SidebarJson}]", SidebarJson, SidebarJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        List<Sidebar> sidebars = await client.Sidebars.GetAsync(embed: true);
        Sidebar sidebar = await client.Sidebars.GetByIdAsync(
            "Primary / Footer & More");
        Sidebar protectedSidebar = await client.Sidebars.GetByIdAsync(
            "sidebar-1",
            useAuth: true);

        Assert.HasCount(1, sidebars);
        Assert.AreEqual("sidebar-1", sidebar.Id);
        Assert.AreEqual("Primary Sidebar", sidebar.Name);
        Assert.AreEqual("active", sidebar.Status);
        List<string> widgets = sidebar.Widgets ?? [];
        Assert.HasCount(2, widgets);
        Assert.AreEqual("block-7", widgets[0]);
        Assert.AreEqual(
            "https://example.com/wp-json/wp/v2/sidebars/sidebar-1",
            sidebar.Links?.Self?[0].Href);
        Assert.AreEqual(
            "preserved",
            sidebar.AdditionalFields?["plugin_field"].GetString());
        Assert.AreEqual("sidebar-1", protectedSidebar.Id);
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/sidebars?_embed",
            null);
        AssertRequest(
            handler.Requests[1],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/sidebars/Primary%20%2F%20Footer%20%26%20More",
            null);
        AssertRequest(
            handler.Requests[2],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/sidebars/sidebar-1",
            "Basic");
    }

    [TestMethod]
    public async Task UpdateAsync_PostsOnlyTheOrderedWidgetAssignment()
    {
        RecordingHandler handler = new(SidebarJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Sidebar updated = await client.Sidebars.UpdateAsync(new Sidebar
        {
            Id = "Primary / Footer",
            Name = "Ignored read-only name",
            Status = "inactive",
            Widgets = ["search-2", "block-7"]
        });

        Assert.AreEqual("sidebar-1", updated.Id);
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Post,
            "https://example.com/wp-json/wp/v2/sidebars/Primary%20%2F%20Footer",
            "Basic");
        Assert.AreEqual("application/json", handler.Requests[0].ContentType);
        using JsonDocument body = JsonDocument.Parse(handler.Requests[0].Body!);
        Assert.AreEqual(1, body.RootElement.GetPropertyCount());
        JsonElement widgets = body.RootElement.GetProperty("widgets");
        Assert.AreEqual("search-2", widgets[0].GetString());
        Assert.AreEqual("block-7", widgets[1].GetString());
        Assert.IsFalse(body.RootElement.TryGetProperty("id", out _));
        Assert.IsFalse(body.RootElement.TryGetProperty("name", out _));
        Assert.IsFalse(body.RootElement.TryGetProperty("status", out _));
    }

    [TestMethod]
    public async Task RecordOperations_RejectMissingEntitiesAndIdsBeforeSending()
    {
        RecordingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Assert.ThrowsExactly<ArgumentNullException>(() =>
            client.Sidebars.GetByIdAsync(null!));
        Assert.ThrowsExactly<ArgumentException>(() =>
            client.Sidebars.GetByIdAsync(" "));
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(() =>
            client.Sidebars.UpdateAsync(null!));
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(() =>
            client.Sidebars.UpdateAsync(new Sidebar()));
        await Assert.ThrowsExactlyAsync<ArgumentException>(() =>
            client.Sidebars.UpdateAsync(new Sidebar { Id = " " }));
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
        HttpMethod method,
        string uri,
        string? authorizationScheme)
    {
        Assert.AreEqual(method, request.Method);
        Assert.AreEqual(uri, request.Uri?.OriginalString);
        Assert.AreEqual(authorizationScheme, request.AuthorizationScheme);
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
