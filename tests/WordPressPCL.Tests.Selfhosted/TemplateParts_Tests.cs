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
public class TemplateParts_Tests
{
    private const string TemplatePartJson = """
        {
          "id": "child theme//header & utility",
          "slug": "header-utility",
          "theme": "child theme",
          "type": "wp_template_part",
          "source": "custom",
          "origin": "theme",
          "content": {
            "raw": "<!-- wp:group /-->",
            "block_version": 3
          },
          "title": { "raw": "Utility Header", "rendered": "Utility Header" },
          "description": "Header utilities",
          "status": "publish",
          "wp_id": 52,
          "has_theme_file": false,
          "author": 9,
          "area": "header",
          "future_field": 12
        }
        """;

    [TestMethod]
    public async Task CollectionReads_UseTemplatePartFiltersAndDeserializeArea()
    {
        RecordingHandler handler = new($"[{TemplatePartJson}]", $"[{TemplatePartJson}]");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        List<TemplatePart> parts = await client.TemplateParts.GetAsync();
        List<TemplatePart> filtered = await client.TemplateParts.QueryAsync(new TemplatePartsQueryBuilder
        {
            WpId = 52,
            Area = "Header & Footer",
            PostType = "wp_template_part",
            Context = Context.Edit,
            Embed = true
        });

        Assert.HasCount(1, parts);
        Assert.HasCount(1, filtered);
        Assert.AreEqual("header", filtered[0].Area);
        Assert.AreEqual(3, filtered[0].Content?.BlockVersion);
        Assert.AreEqual(12, filtered[0].AdditionalFields?["future_field"].GetInt32());
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/template-parts");
        AssertRequest(
            handler.Requests[1],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/template-parts?context=edit&wp_id=52&post_type=wp_template_part&area=Header+%26+Footer&_embed=true");
    }

    [TestMethod]
    public async Task GetByIdAsync_PreservesCompoundSeparatorsAndEncodesSegments()
    {
        RecordingHandler handler = new(TemplatePartJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        TemplatePart part = await client.TemplateParts.GetByIdAsync(
            "child theme//header & utility",
            context: Context.Edit);

        Assert.AreEqual("header-utility", part.Slug);
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/template-parts/child%20theme//header%20%26%20utility?context=edit");
    }

    [TestMethod]
    public void UpdateAsync_RequiresACompoundIdentifier()
    {
        RecordingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Assert.ThrowsExactly<ArgumentException>(() => client.TemplateParts.UpdateAsync(new TemplatePart()));
    }

    [TestMethod]
    public async Task CrudOperations_UseTemplatePartRoutesAndSerializeArea()
    {
        RecordingHandler handler = new(TemplatePartJson, TemplatePartJson, """{"deleted":true}""");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        TemplatePart created = await client.TemplateParts.CreateAsync(new TemplatePart
        {
            Slug = "header-utility",
            Theme = "child theme",
            Area = "header",
            Content = new TemplateContent("<!-- wp:group /-->"),
            Title = new Title("Utility Header")
        });
        TemplatePart updated = await client.TemplateParts.UpdateAsync(new TemplatePart
        {
            Id = created.Id,
            Area = "footer"
        });
        bool deleted = await client.TemplateParts.DeleteAsync(updated.Id!);

        Assert.AreEqual(created.Id, updated.Id);
        Assert.IsTrue(deleted);
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Post,
            "https://example.com/wp-json/wp/v2/template-parts");
        AssertRequest(
            handler.Requests[1],
            HttpMethod.Post,
            "https://example.com/wp-json/wp/v2/template-parts/child%20theme//header%20%26%20utility");
        AssertRequest(
            handler.Requests[2],
            HttpMethod.Delete,
            "https://example.com/wp-json/wp/v2/template-parts/child%20theme//header%20%26%20utility?force=false");

        using JsonDocument createBody = JsonDocument.Parse(handler.Requests[0].Body!);
        Assert.AreEqual("header", createBody.RootElement.GetProperty("area").GetString());
        Assert.AreEqual("header-utility", createBody.RootElement.GetProperty("slug").GetString());
        Assert.AreEqual("Utility Header", createBody.RootElement.GetProperty("title").GetString());
        Assert.AreEqual("<!-- wp:group /-->", createBody.RootElement.GetProperty("content").GetString());
        Assert.IsFalse(createBody.RootElement.TryGetProperty("wp_id", out _));

        using JsonDocument updateBody = JsonDocument.Parse(handler.Requests[1].Body!);
        Assert.AreEqual("footer", updateBody.RootElement.GetProperty("area").GetString());
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
