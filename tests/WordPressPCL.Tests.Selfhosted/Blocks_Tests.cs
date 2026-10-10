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
public class Blocks_Tests
{
    private const string BlockJson = """
        {
          "id": 17,
          "date": null,
          "date_gmt": null,
          "guid": {
            "raw": "https://example.com/?post_type=wp_block&p=17",
            "rendered": "https://example.com/?post_type=wp_block&p=17"
          },
          "link": "https://example.com/?post_type=wp_block&p=17",
          "modified": "2026-08-31T20:15:00",
          "modified_gmt": "2026-08-31T18:15:00",
          "slug": "hero-banner",
          "status": "publish",
          "type": "wp_block",
          "password": "",
          "title": { "raw": "Hero Banner" },
          "content": {
            "raw": "<!-- wp:paragraph --><p>Hello</p><!-- /wp:paragraph -->"
          },
          "meta": {},
          "template": ""
        }
        """;

    [TestMethod]
    public async Task QueryAsync_PreservesSearchAndEncodesCollectionFilters()
    {
        RecordingHandler handler = new($"[{BlockJson}]");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);
        BlocksQueryBuilder query = new()
        {
            Page = 2,
            PerPage = 25,
            Search = "Hero & Banner",
            ModifiedAfter = new DateTime(2026, 8, 30, 12, 30, 0),
            Include = [17, 3],
            OrderBy = PostsOrderBy.IncludeSlugs,
            Slugs = ["Hero", "Footer"],
            Statuses = [Status.Publish, Status.Draft],
            Order = Order.ASC,
            Embed = true,
            Context = Context.Edit
        };

        List<Block> blocks = await client.Blocks.QueryAsync(query);

        Assert.HasCount(1, blocks);
        Assert.AreEqual(17, blocks[0].Id);
        Assert.IsNull(blocks[0].Date);
        Assert.AreEqual("wp_block", blocks[0].Type);
        Assert.AreEqual("https://example.com/?post_type=wp_block&p=17", blocks[0].Guid?.Raw);
        Assert.AreEqual("https://example.com/?post_type=wp_block&p=17", blocks[0].Guid?.Rendered);
        string? rawContent = blocks[0].Content?.Raw;
        Assert.IsNotNull(rawContent);
        Assert.Contains("wp:paragraph", rawContent);
        Assert.IsNull(blocks[0].Content?.Rendered);
        Assert.AreEqual(
            "https://example.com/wp-json/wp/v2/blocks?page=2&per_page=25&search=Hero+%26+Banner&modified_after=2026-08-30T12%3a30%3a00&include=17%2c3&orderby=include_slugs&slug=Hero%2cFooter&status=publish%2cdraft&order=asc&_embed=true&context=edit",
            handler.Requests[0].Uri?.OriginalString);
        Assert.AreEqual("Basic", handler.Requests[0].AuthorizationScheme);
    }

    [TestMethod]
    public async Task CrudAsync_UsesBlockRoutesAuthenticationAndSerializerOptions()
    {
        RecordingHandler handler = new(BlockJson, BlockJson, """{"deleted":true}""");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);
        client.JsonSerializerOptions.WriteIndented = true;

        Block created = await client.Blocks.CreateAsync(new Block
        {
            Title = new Title("Reusable & Featured"),
            Content = new Content("<!-- wp:paragraph --><p>Hello</p><!-- /wp:paragraph -->"),
            Status = Status.Publish
        });
        Block updated = await client.Blocks.UpdateAsync(new Block
        {
            Id = created.Id,
            Title = new Title("Updated")
        });
        bool deleted = await client.Blocks.DeleteAsync(updated.Id);

        Assert.AreEqual(17, created.Id);
        Assert.AreEqual(17, updated.Id);
        Assert.IsTrue(deleted);
        AssertRequest(handler.Requests[0], HttpMethod.Post, "https://example.com/wp-json/wp/v2/blocks");
        AssertRequest(handler.Requests[1], HttpMethod.Post, "https://example.com/wp-json/wp/v2/blocks/17");
        AssertRequest(handler.Requests[2], HttpMethod.Delete, "https://example.com/wp-json/wp/v2/blocks/17?force=false");
        Assert.Contains("\n", handler.Requests[0].Body!);

        using JsonDocument createBody = JsonDocument.Parse(handler.Requests[0].Body!);
        Assert.AreEqual("Reusable & Featured", createBody.RootElement.GetProperty("title").GetProperty("raw").GetString());
        Assert.AreEqual("publish", createBody.RootElement.GetProperty("status").GetString());
        Assert.IsFalse(createBody.RootElement.TryGetProperty("date", out _));
        Assert.IsFalse(createBody.RootElement.TryGetProperty("link", out _));

        using JsonDocument updateBody = JsonDocument.Parse(handler.Requests[1].Body!);
        Assert.AreEqual(17, updateBody.RootElement.GetProperty("id").GetInt32());
        Assert.IsFalse(updateBody.RootElement.TryGetProperty("status", out _));
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
