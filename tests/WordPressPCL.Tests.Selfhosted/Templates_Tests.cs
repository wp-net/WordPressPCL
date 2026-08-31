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
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Templates_Tests
{
    private const string TemplateJson = """
        {
          "id": "parent theme//front & page",
          "slug": "front-page",
          "theme": "parent theme",
          "type": "wp_template",
          "source": "custom",
          "origin": "theme",
          "content": {
            "raw": "<!-- wp:post-content /-->",
            "block_version": 3
          },
          "title": { "raw": "Front Page", "rendered": "Front Page" },
          "description": "Primary front page",
          "status": "publish",
          "wp_id": 41,
          "has_theme_file": true,
          "author": 7,
          "modified": "2026-08-31T20:15:00",
          "author_text": "Editor",
          "original_source": "theme",
          "date": "2026-08-30T10:00:00",
          "is_custom": false,
          "plugin": "site-editor",
          "future_field": "preserved"
        }
        """;

    [TestMethod]
    public async Task CollectionReads_UseSupportedFiltersAndDeserializeTemplateSchema()
    {
        RecordingHandler handler = new($"[{TemplateJson}]", $"[{TemplateJson}]");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        List<Template> templates = await client.Templates.GetAsync(
            embed: true,
            context: Context.Edit);
        List<Template> filtered = await client.Templates.QueryAsync(new TemplatesQueryBuilder
        {
            WpId = 41,
            PostType = "Book & Page",
            Context = Context.Edit,
            Embed = true
        });

        Assert.HasCount(1, templates);
        Assert.HasCount(1, filtered);
        Template template = filtered[0];
        Assert.AreEqual("parent theme//front & page", template.Id);
        Assert.AreEqual(3, template.Content?.BlockVersion);
        Assert.AreEqual("publish", template.Status?.ToString().ToLowerInvariant());
        Assert.AreEqual(new DateTime(2026, 8, 31, 20, 15, 0), template.Modified);
        Assert.AreEqual("theme", template.OriginalSource);
        Assert.IsFalse(template.IsCustom);
        Assert.AreEqual("preserved", template.AdditionalFields?["future_field"].GetString());
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/templates?context=edit&_embed");
        AssertRequest(
            handler.Requests[1],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/templates?context=edit&wp_id=41&post_type=Book+%26+Page&_embed=true");
    }

    [TestMethod]
    public async Task ItemAndFallbackReads_EncodePathAndQueryValues()
    {
        RecordingHandler handler = new(TemplateJson, TemplateJson, TemplateJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Template template = await client.Templates.GetByIdAsync(
            "parent theme//front & page",
            embed: true,
            context: Context.Edit);
        Template fallback = await client.Templates.GetFallbackAsync(
            "single & book",
            isCustom: true,
            templatePrefix: "single book");
        Template simpleFallback = await client.Templates.GetFallbackAsync("index");

        Assert.AreEqual("front-page", template.Slug);
        Assert.AreEqual("front-page", fallback.Slug);
        Assert.AreEqual("front-page", simpleFallback.Slug);
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/templates/parent%20theme//front%20%26%20page?context=edit&_embed");
        AssertRequest(
            handler.Requests[1],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/templates/lookup?slug=single+%26+book&is_custom=true&template_prefix=single+book");
        AssertRequest(
            handler.Requests[2],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/templates/lookup?slug=index");
    }

    [TestMethod]
    public void UpdateAsync_RequiresACompoundIdentifier()
    {
        RecordingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Assert.ThrowsExactly<ArgumentException>(() => client.Templates.UpdateAsync(new Template()));
    }

    [TestMethod]
    public async Task CrudOperations_UseCompoundRoutesAndSerializeWritableFields()
    {
        RecordingHandler handler = new(TemplateJson, TemplateJson, """{"deleted":true}""");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Template created = await client.Templates.CreateAsync(new Template
        {
            Slug = "front-page",
            Theme = "parent theme",
            Content = new TemplateContent("<!-- wp:post-content /-->"),
            Title = new Title("Front & Center"),
            Status = Status.Publish
        });
        Template updated = await client.Templates.UpdateAsync(new Template
        {
            Id = created.Id,
            Title = new Title("Updated")
        });
        bool deleted = await client.Templates.DeleteAsync(updated.Id!, force: true);

        Assert.AreEqual("parent theme//front & page", created.Id);
        Assert.AreEqual(created.Id, updated.Id);
        Assert.IsTrue(deleted);
        AssertRequest(handler.Requests[0], HttpMethod.Post, "https://example.com/wp-json/wp/v2/templates");
        AssertRequest(
            handler.Requests[1],
            HttpMethod.Post,
            "https://example.com/wp-json/wp/v2/templates/parent%20theme//front%20%26%20page");
        AssertRequest(
            handler.Requests[2],
            HttpMethod.Delete,
            "https://example.com/wp-json/wp/v2/templates/parent%20theme//front%20%26%20page?force=true");

        using JsonDocument createBody = JsonDocument.Parse(handler.Requests[0].Body!);
        Assert.AreEqual("front-page", createBody.RootElement.GetProperty("slug").GetString());
        Assert.AreEqual("Front & Center", createBody.RootElement.GetProperty("title").GetString());
        Assert.AreEqual(
            "<!-- wp:post-content /-->",
            createBody.RootElement.GetProperty("content").GetString());
        Assert.IsFalse(createBody.RootElement.TryGetProperty("id", out _));
        Assert.IsFalse(createBody.RootElement.TryGetProperty("description", out _));

        using JsonDocument updateBody = JsonDocument.Parse(handler.Requests[1].Body!);
        Assert.AreEqual("Updated", updateBody.RootElement.GetProperty("title").GetString());
        Assert.IsFalse(updateBody.RootElement.TryGetProperty("content", out _));
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
        if (method == HttpMethod.Post)
        {
            Assert.AreEqual("application/json", request.ContentType);
        }
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
