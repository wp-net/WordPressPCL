using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Hosted;

[TestClass]
public class WordPressClient_DI_Tests
{
    [TestMethod]
    public async Task Disposing_WordPressClient_DoesNotDispose_ExternalHttpClient()
    {
        RecordingHandler handler = new();
        HttpClient httpClient = new(handler)
        {
            BaseAddress = new Uri("https://example.com/wp-json/")
        };

        using (WordPressClient wordPressClient = new(httpClient))
        {
            Assert.IsFalse(wordPressClient.OwnsHttpClient);
        }

        using WordPressClient reusedClient = new(httpClient);
        List<Post> posts = await reusedClient.Posts.GetAllAsync();

        Assert.AreEqual(0, posts.Count);
        Assert.AreEqual("https://example.com/wp-json/wp/v2/posts?per_page=100&page=1", handler.LastRequestUri?.ToString());
    }

    [TestMethod]
    public async Task Disposing_WordPressClient_Disposes_OwnedHttpClient()
    {
        using WordPressClient wordPressClient = new(new Uri("https://example.com/wp-json/"));

        Assert.IsTrue(wordPressClient.OwnsHttpClient);

        wordPressClient.Dispose();

        try
        {
            await wordPressClient.Posts.GetAllAsync();
            Assert.Fail("Expected ObjectDisposedException after disposing an owned HttpClient.");
        }
        catch (ObjectDisposedException)
        {
        }
    }

    [TestMethod]
    public async Task AddWordPressClient_Registers_TypedClient_WithConfiguredBaseAddressAndAuth()
    {
        RecordingHandler handler = new();
        ServiceCollection services = new();

        services
            .AddWordPressClient(
                (serviceProvider, httpClient) =>
                {
                    httpClient.BaseAddress = new Uri("https://example.com/wp-json/");
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "WordPressPCL.Tests");
                },
                (serviceProvider, client) => client.Auth.UseBasicAuth("demo-user", "demo-password"))
            .ConfigurePrimaryHttpMessageHandler(() => handler);

        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        using WordPressClient client = serviceProvider.GetRequiredService<WordPressClient>();

        Assert.IsFalse(client.OwnsHttpClient);
        Assert.AreEqual(new Uri("https://example.com/wp-json/"), client.WordPressUri);

        List<Post> posts = await client.Posts.GetAllAsync(useAuth: true);

        Assert.AreEqual(0, posts.Count);
        Assert.AreEqual("https://example.com/wp-json/wp/v2/posts?per_page=100&page=1", handler.LastRequestUri?.ToString());
        Assert.AreEqual("Basic", handler.LastAuthorizationScheme);
        Assert.AreEqual(Convert.ToBase64String(Encoding.ASCII.GetBytes("demo-user:demo-password")), handler.LastAuthorizationParameter);
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public Uri? LastRequestUri { get; private set; }

        public string? LastAuthorizationScheme { get; private set; }

        public string? LastAuthorizationParameter { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequestUri = request.RequestUri;
            LastAuthorizationScheme = request.Headers.Authorization?.Scheme;
            LastAuthorizationParameter = request.Headers.Authorization?.Parameter;

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[]", Encoding.UTF8, "application/json")
            });
        }
    }
}
