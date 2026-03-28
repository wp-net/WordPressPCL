using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net.Http;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted;

/// <summary>
/// Unit tests verifying that guard clauses fire correctly when null
/// (or empty) values are passed to public entry points.  These tests
/// do not require a running WordPress server.
/// </summary>
[TestClass]
public class NullHandling_Tests
{
    // -----------------------------------------------------------------------
    // WordPressClient constructors
    // -----------------------------------------------------------------------

    [TestMethod]
    public void WordPressClient_StringUri_NullThrows()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new WordPressClient((string)null!));
    }

    [TestMethod]
    public void WordPressClient_UriOverload_NullThrows()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new WordPressClient((Uri)null!));
    }

    [TestMethod]
    public void WordPressClient_HttpClientOverload_NullHttpClientThrows()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new WordPressClient((HttpClient)null!));
    }

    [TestMethod]
    public void WordPressClient_HttpClientOverload_NoBaseAddressAndNoUriThrows()
    {
        // HttpClient has no BaseAddress and no uri is provided → must throw
        var httpClient = new HttpClient(); // no BaseAddress
        Assert.ThrowsExactly<ArgumentNullException>(() => new WordPressClient(httpClient));
    }

    [TestMethod]
    public void WordPressClient_HttpClientOverload_WithBaseAddress_Succeeds()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("http://example.com/wp-json/") };
        var client = new WordPressClient(httpClient);
        Assert.IsNotNull(client);
        Assert.AreEqual(new Uri("http://example.com/wp-json/"), client.WordPressUri);
    }

    [TestMethod]
    public void WordPressClient_HttpClientOverload_WithExplicitUri_Succeeds()
    {
        var httpClient = new HttpClient();
        var client = new WordPressClient(httpClient, uri: new Uri("http://example.com/wp-json/"));
        Assert.IsNotNull(client);
        Assert.AreEqual(new Uri("http://example.com/wp-json/"), client.WordPressUri);
    }

    // -----------------------------------------------------------------------
    // Auth.UseBasicAuth guard clauses
    // -----------------------------------------------------------------------

    [TestMethod]
    public void UseBasicAuth_NullUsername_Throws()
    {
        var client = new WordPressClient("http://example.com/wp-json/");
        Assert.ThrowsExactly<ArgumentNullException>(() =>
            client.Auth.UseBasicAuth(null!, "password"));
    }

    [TestMethod]
    public void UseBasicAuth_NullPassword_Throws()
    {
        var client = new WordPressClient("http://example.com/wp-json/");
        Assert.ThrowsExactly<ArgumentNullException>(() =>
            client.Auth.UseBasicAuth("user", null!));
    }

    // -----------------------------------------------------------------------
    // Auth.RequestJWTokenAsync guard clauses
    // -----------------------------------------------------------------------

    [TestMethod]
    public async System.Threading.Tasks.Task RequestJWTokenAsync_NullUsername_Throws()
    {
        var client = new WordPressClient("http://example.com/wp-json/");
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
            await client.Auth.RequestJWTokenAsync(null!, "password", TestContext.CancellationToken));
    }

    [TestMethod]
    public async System.Threading.Tasks.Task RequestJWTokenAsync_NullPassword_Throws()
    {
        var client = new WordPressClient("http://example.com/wp-json/");
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
            await client.Auth.RequestJWTokenAsync("user", null!, TestContext.CancellationToken));
    }

    // -----------------------------------------------------------------------
    // Media.CreateAsync guard clauses (stream overload)
    // -----------------------------------------------------------------------

    [TestMethod]
    public async System.Threading.Tasks.Task Media_CreateAsync_Stream_NullFilename_Throws()
    {
        var client = new WordPressClient("http://example.com/wp-json/");
        using var stream = new MemoryStream();
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
            await client.Media.CreateAsync(stream, null!, cancellationToken: TestContext.CancellationToken));
    }

    [TestMethod]
    public async System.Threading.Tasks.Task Media_CreateAsync_Stream_EmptyFilename_Throws()
    {
        var client = new WordPressClient("http://example.com/wp-json/");
        using var stream = new MemoryStream();
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
            await client.Media.CreateAsync(stream, string.Empty, cancellationToken: TestContext.CancellationToken));
    }

    // -----------------------------------------------------------------------
    // Media.CreateAsync guard clauses (file path overload)
    // -----------------------------------------------------------------------

    [TestMethod]
    public async System.Threading.Tasks.Task Media_CreateAsync_Path_NullPath_Throws()
    {
        var client = new WordPressClient("http://example.com/wp-json/");
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
            await client.Media.CreateAsync((string)null!, "file.jpg", cancellationToken: TestContext.CancellationToken));
    }

    [TestMethod]
    public async System.Threading.Tasks.Task Media_CreateAsync_Path_NullFilename_Throws()
    {
        var client = new WordPressClient("http://example.com/wp-json/");
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
            await client.Media.CreateAsync("/tmp/file.jpg", null!, cancellationToken: TestContext.CancellationToken));
    }

    // -----------------------------------------------------------------------
    // Model constructors — verify they accept null-like states gracefully
    // -----------------------------------------------------------------------

    [TestMethod]
    public void Post_DefaultConstructor_AllPropertiesAreNull()
    {
        var post = new Post();
        Assert.IsNull(post.Slug);
        Assert.IsNull(post.Title);
        Assert.IsNull(post.Content);
        Assert.IsNull(post.Links);
    }

    [TestMethod]
    public void User_ParameterlessConstructor_AllPropertiesAreNull()
    {
        var user = new User();
        Assert.IsNull(user.UserName);
        Assert.IsNull(user.Email);
        Assert.IsNull(user.Password);
    }

    [TestMethod]
    public void User_RequiredParamsConstructor_SetsPropertiesCorrectly()
    {
        var user = new User("name", "email@test.com", "pass");
        Assert.AreEqual("name", user.UserName);
        Assert.AreEqual("email@test.com", user.Email);
        Assert.AreEqual("pass", user.Password);
    }

    [TestMethod]
    public void Comment_DefaultConstructor_ContentIsNull()
    {
        var comment = new Comment();
        Assert.IsNull(comment.Content);
    }

    [TestMethod]
    public void Comment_RequiredParamsConstructor_SetsContentCorrectly()
    {
        var comment = new Comment(42, "Hello world");
        Assert.AreEqual(42, comment.PostId);
        Assert.AreEqual("Hello world", comment.Content!.Raw);
    }

    public TestContext TestContext { get; set; }
}
