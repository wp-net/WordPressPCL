using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class HttpClient_Tests
{
    [TestMethod]
    public async Task CustomHttpClient_WithBaseAddress()
    {
        // Initialize
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(ApiCredentials.WordPressUri)
        };

        await CustomHttpClientBase(httpClient, new WordPressClient(httpClient));
    }

    [TestMethod]
    public async Task CustomHttpClient_WithoutBaseAddress()
    {
        // Initialize
        var httpClient = new HttpClient();
        var wordPressClient = new WordPressClient(httpClient, uri: new Uri(ApiCredentials.WordPressUri));
        await CustomHttpClientBase(httpClient, wordPressClient);
    }

    private async Task CustomHttpClientBase(HttpClient httpClient, WordPressClient client)
    {
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0");
        httpClient.DefaultRequestHeaders.Add("Referer", "https://github.com/wp-net/WordPressPCL");

        var posts = await client.Posts.GetAllAsync();
        var post = await client.Posts.GetByIDAsync(posts.First().Id);
        Assert.IsTrue(posts.First().Id == post.Id);
        Assert.IsTrue(!string.IsNullOrEmpty(posts.First().Content.Rendered));

        await client.Auth.RequestJWTokenAsync(ApiCredentials.Username, ApiCredentials.Password);
        var validToken = await client.Auth.IsValidJWTokenAsync();
        Assert.IsTrue(validToken);
    }
}