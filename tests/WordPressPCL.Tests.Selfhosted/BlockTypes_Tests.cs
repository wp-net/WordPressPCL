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
public class BlockTypes_Tests
{
    private const string BlockTypeJson = """
        {
          "api_version": 3,
          "name": "core/paragraph",
          "title": "Paragraph",
          "description": "Start with the basic building block of all narrative.",
          "icon": null,
          "attributes": { "content": { "type": "string" } },
          "provides_context": { "core/queryId": "queryId" },
          "uses_context": [ "postId" ],
          "selectors": { "root": { "selector": ".wp-block-paragraph" } },
          "supports": { "anchor": true },
          "category": "text",
          "is_dynamic": false,
          "editor_script_handles": [ "wp-block-paragraph" ],
          "view_script_module_ids": [ "@wordpress/interactivity" ],
          "style_handles": [ "wp-block-paragraph" ],
          "view_style_handles": [ "wp-block-paragraph-view" ],
          "styles": [ { "name": "plain", "label": "Plain" } ],
          "variations": [],
          "textdomain": "default",
          "parent": null,
          "ancestor": [ "core/group" ],
          "allowed_blocks": [ "core/text" ],
          "keywords": [ "text" ],
          "example": { "attributes": { "content": "Example" } },
          "block_hooks": { "core/post-content": "last_child" },
          "plugin_field": "preserved"
        }
        """;

    [TestMethod]
    public async Task QueryAsync_EncodesFiltersAuthenticatesAndDeserializesSchema()
    {
        RecordingHandler handler = new($"[{BlockTypeJson}]");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);
        BlockTypesQueryBuilder query = new()
        {
            Namespace = "Core & custom",
            Context = Context.Edit,
            Embed = true
        };

        List<BlockType> blockTypes = await client.BlockTypes.QueryAsync(query);

        Assert.HasCount(1, blockTypes);
        BlockType blockType = blockTypes[0];
        Assert.AreEqual("core/paragraph", blockType.Name);
        Assert.AreEqual(3, blockType.ApiVersion);
        Assert.AreEqual("string", blockType.Attributes?["content"].GetProperty("type").GetString());
        Assert.AreEqual("queryId", blockType.ProvidesContext?.GetProperty("core/queryId").GetString());
        Assert.AreEqual("postId", blockType.UsesContext?[0]);
        Assert.IsTrue(blockType.Supports?["anchor"].GetBoolean());
        Assert.AreEqual("wp-block-paragraph", blockType.EditorScriptHandles?[0]);
        Assert.AreEqual("@wordpress/interactivity", blockType.ViewScriptModuleIds?[0]);
        Assert.AreEqual("wp-block-paragraph-view", blockType.ViewStyleHandles?[0]);
        Assert.AreEqual("core/group", blockType.Ancestor?[0]);
        Assert.AreEqual("core/text", blockType.AllowedBlocks?[0]);
        Assert.AreEqual("last_child", blockType.BlockHooks?["core/post-content"]);
        Assert.AreEqual("preserved", blockType.AdditionalFields?["plugin_field"].GetString());
        Assert.AreEqual(
            "https://example.com/wp-json/wp/v2/block-types?context=edit&namespace=Core+%26+custom&_embed=true",
            handler.Requests[0].Uri?.OriginalString);
        Assert.AreEqual("Basic", handler.Requests[0].AuthorizationScheme);
    }

    [TestMethod]
    public async Task NamespaceAndNameReads_UseDocumentedRoutesAndContext()
    {
        RecordingHandler handler = new($"[{BlockTypeJson}]", BlockTypeJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        List<BlockType> namespaceTypes = await client.BlockTypes.GetByNamespaceAsync("core", Context.Edit);
        BlockType blockType = await client.BlockTypes.GetByNameAsync("core", "paragraph", Context.Edit);

        Assert.HasCount(1, namespaceTypes);
        Assert.AreEqual("core/paragraph", blockType.Name);
        Assert.AreEqual(
            "https://example.com/wp-json/wp/v2/block-types/core?context=edit",
            handler.Requests[0].Uri?.OriginalString);
        Assert.AreEqual(
            "https://example.com/wp-json/wp/v2/block-types/core/paragraph?context=edit",
            handler.Requests[1].Uri?.OriginalString);
        Assert.AreEqual("Basic", handler.Requests[0].AuthorizationScheme);
        Assert.AreEqual("Basic", handler.Requests[1].AuthorizationScheme);
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

    private sealed record CapturedRequest(Uri? Uri, string? AuthorizationScheme);

    private sealed class RecordingHandler(params string[] responseBodies) : HttpMessageHandler
    {
        private readonly Queue<string> _responseBodies = new(responseBodies);

        public List<CapturedRequest> Requests { get; } = [];

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add(new CapturedRequest(request.RequestUri, request.Headers.Authorization?.Scheme));
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_responseBodies.Dequeue(), Encoding.UTF8, "application/json")
            });
        }
    }
}
