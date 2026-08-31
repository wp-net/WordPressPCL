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
public class Navigation_Tests
{
    private const string NavigationJson = """
        {
          "id": 29,
          "date": null,
          "date_gmt": null,
          "guid": { "rendered": "https://example.com/?post_type=wp_navigation&p=29" },
          "link": "https://example.com/?post_type=wp_navigation&p=29",
          "modified": "2026-08-31T20:15:00",
          "modified_gmt": "2026-08-31T18:15:00",
          "slug": "primary-navigation",
          "status": "publish",
          "type": "wp_navigation",
          "password": "",
          "title": { "raw": "Primary Navigation", "rendered": "Primary Navigation" },
          "content": {
            "raw": "<!-- wp:navigation-link {\"label\":\"Home\",\"url\":\"/\"} /-->",
            "rendered": "<a href=\"/\">Home</a>"
          },
          "meta": {},
          "template": ""
        }
        """;

    [TestMethod]
    public async Task QueryAsync_UsesNavigationRouteAndInheritedFilters()
    {
        RecordingHandler handler = new($"[{NavigationJson}]");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);
        NavigationQueryBuilder query = new()
        {
            Search = "Primary Menu",
            Exclude = [4, 8],
            PerPage = 10,
            Statuses = [Status.Publish],
            OrderBy = PostsOrderBy.Modified,
            Context = Context.Edit
        };

        List<Navigation> navigations = await client.Navigation.QueryAsync(query, useAuth: true);

        Assert.HasCount(1, navigations);
        Assert.AreEqual(29, navigations[0].Id);
        Assert.IsNull(navigations[0].Date);
        Assert.AreEqual("wp_navigation", navigations[0].Type);
        string? navigationContent = navigations[0].Content?.Raw;
        Assert.IsNotNull(navigationContent);
        Assert.Contains("navigation-link", navigationContent);
        Assert.AreEqual(
            "https://example.com/wp-json/wp/v2/navigation?per_page=10&search=Primary+Menu&exclude=4%2c8&orderby=modified&status=publish&order=desc&context=edit",
            handler.Requests[0].Uri?.OriginalString);
        Assert.AreEqual("Basic", handler.Requests[0].AuthorizationScheme);
    }

    [TestMethod]
    public async Task CrudAsync_UsesNavigationRoutesAndOmitsUnsetNullableFields()
    {
        RecordingHandler handler = new(NavigationJson, NavigationJson, """{"deleted":true}""");
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Navigation created = await client.Navigation.CreateAsync(new Navigation
        {
            Title = new Title("Primary Navigation"),
            Content = new Content("<!-- wp:navigation-link {\"label\":\"Home\",\"url\":\"/\"} /-->"),
            Status = Status.Publish
        });
        Navigation updated = await client.Navigation.UpdateAsync(new Navigation
        {
            Id = created.Id,
            Title = new Title("Header Navigation")
        });
        bool deleted = await client.Navigation.DeleteAsync(updated.Id);

        Assert.AreEqual(29, created.Id);
        Assert.AreEqual(29, updated.Id);
        Assert.IsTrue(deleted);
        AssertRequest(handler.Requests[0], HttpMethod.Post, "https://example.com/wp-json/wp/v2/navigation");
        AssertRequest(handler.Requests[1], HttpMethod.Post, "https://example.com/wp-json/wp/v2/navigation/29");
        AssertRequest(handler.Requests[2], HttpMethod.Delete, "https://example.com/wp-json/wp/v2/navigation/29?force=false");

        using JsonDocument createBody = JsonDocument.Parse(handler.Requests[0].Body!);
        Assert.AreEqual("publish", createBody.RootElement.GetProperty("status").GetString());
        string? rawContent = createBody.RootElement.GetProperty("content").GetProperty("raw").GetString();
        Assert.IsNotNull(rawContent);
        Assert.Contains("navigation-link", rawContent);
        Assert.IsFalse(createBody.RootElement.TryGetProperty("date", out _));
        Assert.IsFalse(createBody.RootElement.TryGetProperty("type", out _));

        using JsonDocument updateBody = JsonDocument.Parse(handler.Requests[1].Body!);
        Assert.AreEqual("Header Navigation", updateBody.RootElement.GetProperty("title").GetProperty("raw").GetString());
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
    }

    private sealed record CapturedRequest(
        HttpMethod Method,
        Uri? Uri,
        string? AuthorizationScheme,
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
                body));
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_responseBodies.Dequeue(), Encoding.UTF8, "application/json")
            };
        }
    }
}
