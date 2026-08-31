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
public class GlobalStyles_Tests
{
    private const string StoredStylesJson = """
        {
          "id": 63,
          "title": { "raw": "Custom", "rendered": "Custom" },
          "settings": {
            "color": { "custom": false }
          },
          "styles": {
            "elements": { "link": { "color": { "text": "var:preset|color|blue" } } }
          },
          "future_field": "preserved"
        }
        """;

    private const string ThemeStylesJson = """
        {
          "settings": {
            "typography": { "fluid": true }
          },
          "styles": {
            "typography": { "fontSize": "1rem" }
          }
        }
        """;

    [TestMethod]
    public async Task ReadOperations_UseSupportedRoutesAndActualResponseShapes()
    {
        RecordingHandler handler = new(StoredStylesJson, ThemeStylesJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        GlobalStyles stored = await client.GlobalStyles.GetByIdAsync(63, embed: true);
        GlobalStyles theme = await client.GlobalStyles.GetThemeStylesAsync("vendor/theme & child");

        Assert.AreEqual(63, stored.Id);
        Assert.AreEqual("Custom", stored.Title?.Raw);
        Assert.IsFalse(stored.Settings?.GetProperty("color").GetProperty("custom").GetBoolean());
        Assert.AreEqual("preserved", stored.AdditionalFields?["future_field"].GetString());
        Assert.IsNull(theme.Id);
        Assert.IsNull(theme.Title);
        Assert.IsTrue(theme.Settings?.GetProperty("typography").GetProperty("fluid").GetBoolean());
        Assert.AreEqual(
            "1rem",
            theme.Styles?.GetProperty("typography").GetProperty("fontSize").GetString());
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/global-styles/63?_embed");
        AssertRequest(
            handler.Requests[1],
            HttpMethod.Get,
            "https://example.com/wp-json/wp/v2/global-styles/themes/vendor/theme%20%26%20child");
    }

    [TestMethod]
    public async Task RecordOperations_RequireAPositiveId()
    {
        RecordingHandler handler = new();
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => client.GlobalStyles.GetByIdAsync(0));
        await Assert.ThrowsExactlyAsync<ArgumentOutOfRangeException>(() =>
            client.GlobalStyles.UpdateAsync(-1, new GlobalStyles()));
    }

    [TestMethod]
    public async Task UpdateAsync_PostsOnlyProvidedGlobalStylesFields()
    {
        RecordingHandler handler = new(StoredStylesJson);
        using HttpClient httpClient = CreateHttpClient(handler);
        using WordPressClient client = CreateAuthenticatedClient(httpClient);
        using JsonDocument settings = JsonDocument.Parse("""{"color":{"custom":true}}""");
        using JsonDocument styles = JsonDocument.Parse("""{"spacing":{"blockGap":"1rem"}}""");

        GlobalStyles updated = await client.GlobalStyles.UpdateAsync(63, new GlobalStyles
        {
            Title = new Title("Updated"),
            Settings = settings.RootElement.Clone(),
            Styles = styles.RootElement.Clone()
        });

        Assert.AreEqual(63, updated.Id);
        AssertRequest(
            handler.Requests[0],
            HttpMethod.Post,
            "https://example.com/wp-json/wp/v2/global-styles/63");
        using JsonDocument body = JsonDocument.Parse(handler.Requests[0].Body!);
        Assert.AreEqual("Updated", body.RootElement.GetProperty("title").GetProperty("raw").GetString());
        Assert.IsTrue(body.RootElement.GetProperty("settings").GetProperty("color").GetProperty("custom").GetBoolean());
        Assert.AreEqual(
            "1rem",
            body.RootElement.GetProperty("styles").GetProperty("spacing").GetProperty("blockGap").GetString());
        Assert.IsFalse(body.RootElement.TryGetProperty("id", out _));
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
