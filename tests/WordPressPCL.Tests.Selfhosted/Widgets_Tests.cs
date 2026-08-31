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
public class Widgets_Tests
{
    private const string WidgetJson = """
        {
          "id": "block-7",
          "id_base": "block",
          "sidebar": "sidebar-1",
          "rendered": "<section>Welcome</section>",
          "rendered_form": "<form>Widget settings</form>",
          "instance": {
            "encoded": "YToxOntzOjc6ImNvbnRlbnQiO3M6Nzoid2VsY29tZSI7fQ==",
            "hash": "signed-hash",
            "raw": { "content": "Welcome" }
          },
          "_links": {
            "self": [{ "href": "https://example.com/wp-json/wp/v2/widgets/block-7" }]
          },
          "plugin_field": true
        }
        """;

    [TestMethod]
    public async Task ReadOperations_UseRoutesQueriesEncodingAndAuthenticationChoice()
    {
        RecordingHandler handler = new(
            $"[{WidgetJson}]",
            WidgetJson,
            $"[{WidgetJson}]",
            $"[{WidgetJson}]");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        List<Widget> all = await client.Widgets.GetAsync(embed: true);
        Widget widget = await client.Widgets.GetByIdAsync(
            "block/7 & draft",
            useAuth: false);
        List<Widget> sidebarWidgets = await client.Widgets.GetBySidebarAsync(
            "Header & Footer",
            embed: true);
        List<Widget> queried = await client.Widgets.QueryAsync(new WidgetsQueryBuilder
        {
            Sidebar = "Header & Footer/Primary",
            Context = Context.Edit,
            Embed = true,
            Order = Order.ASC
        });

        Assert.HasCount(1, all);
        Assert.AreEqual("block-7", widget.Id);
        Assert.AreEqual("block", widget.IdBase);
        Assert.AreEqual("sidebar-1", widget.Sidebar);
        Assert.AreEqual("signed-hash", widget.Instance?.Hash);
        Assert.AreEqual("Welcome", widget.Instance?.Raw?.GetProperty("content").GetString());
        Assert.AreEqual(
            "https://example.com/wp-json/wp/v2/widgets/block-7",
            widget.Links?.Self?[0].Href);
        Assert.IsTrue(widget.AdditionalFields?["plugin_field"].GetBoolean());
        Assert.HasCount(1, sidebarWidgets);
        Assert.HasCount(1, queried);
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/widgets?_embed",
            "Basic");
        AssertRequest(
            handler.Requests[1],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/widgets/block%2F7%20%26%20draft",
            null);
        AssertRequest(
            handler.Requests[2],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/widgets?sidebar=Header%20%26%20Footer&_embed",
            "Basic");
        AssertRequest(
            handler.Requests[3],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/widgets?sidebar=Header%20%26%20Footer%2FPrimary&context=edit&_embed=true",
            "Basic");
    }

    [TestMethod]
    public async Task CrudOperations_UseWritablePayloadsServerIdsForceAndAuthentication()
    {
        RecordingHandler handler = new(
            WidgetJson,
            WidgetJson,
            """{"id":"block-7","sidebar":"wp_inactive_widgets"}""",
            """{"deleted":true,"previous":{"id":"block-7"}}""");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);
        using JsonDocument raw = JsonDocument.Parse("""{"content":"Welcome"}""");

        Widget created = await client.Widgets.CreateAsync(new Widget
        {
            Id = "caller-id-must-be-ignored",
            IdBase = "block",
            Sidebar = "sidebar-1",
            Rendered = "ignored",
            RenderedForm = "ignored",
            Instance = new WidgetInstance { Raw = raw.RootElement.Clone() }
        });
        Widget updated = await client.Widgets.UpdateAsync(new Widget
        {
            Id = created.Id,
            IdBase = "ignored-on-update",
            Sidebar = "Footer & Secondary",
            FormData = "widget-legacy%5B2%5D%5Btitle%5D=Updated"
        });
        bool deactivated = await client.Widgets.DeleteAsync("block-7");
        bool deleted = await client.Widgets.DeleteAsync("block/7 & old", force: true);

        Assert.AreEqual("block-7", created.Id);
        Assert.AreEqual("block-7", updated.Id);
        Assert.IsTrue(deactivated);
        Assert.IsTrue(deleted);
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Post,
            "https://example.com/wp-json/wp/v2/widgets",
            "Basic");
        AssertRequest(
            handler.Requests[1],
            HttpMethod.Post,
            "https://example.com/wp-json/wp/v2/widgets/block-7",
            "Basic");
        AssertRequest(
            handler.Requests[2],
            HttpMethod.Delete,
            "https://example.com/wp-json/wp/v2/widgets/block-7?force=false",
            "Basic");
        AssertRequest(
            handler.Requests[3],
            HttpMethod.Delete,
            "https://example.com/wp-json/wp/v2/widgets/block%2F7%20%26%20old?force=true",
            "Basic");
        Assert.AreEqual("application/json", handler.Requests[0].ContentType);
        Assert.AreEqual("application/json", handler.Requests[1].ContentType);

        using JsonDocument createBody = JsonDocument.Parse(handler.Requests[0].Body!);
        Assert.AreEqual("block", createBody.RootElement.GetProperty("id_base").GetString());
        Assert.AreEqual("sidebar-1", createBody.RootElement.GetProperty("sidebar").GetString());
        Assert.AreEqual(
            "Welcome",
            createBody.RootElement
                .GetProperty("instance")
                .GetProperty("raw")
                .GetProperty("content")
                .GetString());
        Assert.IsFalse(createBody.RootElement.TryGetProperty("id", out _));
        Assert.IsFalse(createBody.RootElement.TryGetProperty("rendered", out _));
        Assert.IsFalse(createBody.RootElement.TryGetProperty("rendered_form", out _));

        using JsonDocument updateBody = JsonDocument.Parse(handler.Requests[1].Body!);
        Assert.AreEqual(
            "Footer & Secondary",
            updateBody.RootElement.GetProperty("sidebar").GetString());
        Assert.AreEqual(
            "widget-legacy%5B2%5D%5Btitle%5D=Updated",
            updateBody.RootElement.GetProperty("form_data").GetString());
        Assert.IsFalse(updateBody.RootElement.TryGetProperty("id", out _));
        Assert.IsFalse(updateBody.RootElement.TryGetProperty("id_base", out _));
    }

    [TestMethod]
    public async Task Operations_RejectMissingInputsBeforeSending()
    {
        RecordingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Assert.ThrowsExactly<ArgumentNullException>(() =>
            client.Widgets.GetByIdAsync(null!));
        Assert.ThrowsExactly<ArgumentException>(() =>
            client.Widgets.GetByIdAsync(" "));
        Assert.ThrowsExactly<ArgumentNullException>(() =>
            client.Widgets.GetBySidebarAsync(null!));
        Assert.ThrowsExactly<ArgumentException>(() =>
            client.Widgets.GetBySidebarAsync(" "));
        Assert.ThrowsExactly<ArgumentNullException>(() =>
            client.Widgets.QueryAsync(null!));
        Assert.ThrowsExactly<ArgumentException>(() =>
            new WidgetsQueryBuilder { Sidebar = " " }.BuildQuery());
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(() =>
            client.Widgets.CreateAsync(null!));
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(() =>
            client.Widgets.CreateAsync(new Widget()));
        await Assert.ThrowsExactlyAsync<ArgumentException>(() =>
            client.Widgets.CreateAsync(new Widget { IdBase = " " }));
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(() =>
            client.Widgets.CreateAsync(new Widget { IdBase = "block" }));
        await Assert.ThrowsExactlyAsync<ArgumentException>(() =>
            client.Widgets.CreateAsync(new Widget { IdBase = "block", Sidebar = " " }));
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(() =>
            client.Widgets.UpdateAsync(null!));
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(() =>
            client.Widgets.UpdateAsync(new Widget()));
        await Assert.ThrowsExactlyAsync<ArgumentException>(() =>
            client.Widgets.UpdateAsync(new Widget { Id = " " }));
        Assert.ThrowsExactly<ArgumentNullException>(() =>
            client.Widgets.DeleteAsync(null!));
        Assert.ThrowsExactly<ArgumentException>(() =>
            client.Widgets.DeleteAsync(" "));
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
