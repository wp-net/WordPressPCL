using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Tests.Selfhosted.Utility;
using System.Threading.Tasks;
using WordPressPCL.Models;
using System.Linq;
using System.Collections.Generic;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Basic_Tests
{
    private static WordPressClient _client = null!;
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _client = ClientHelper.GetWordPressClient();
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task BasicSetupTest()
    {
        // Initialize
        Assert.IsNotNull(_client);
        // Posts
        List<Post> posts = await _client.Posts.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(posts);

        // Test Auth Client
        List<Post> postsAuth = await _clientAuth.Posts.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(postsAuth);
    }

    [TestMethod]
    public async Task GetFirstPostTest()
    {
        // Initialize
        List<Post> posts = await _client.Posts.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Post post = await _client.Posts.GetByIdAsync(posts.First().Id, cancellationToken: TestContext.CancellationToken);
        Assert.AreEqual(post.Id, posts.First().Id);
        Assert.IsTrue(!string.IsNullOrEmpty(posts.First().Content!.Rendered));
    }

    [TestMethod]
    public async Task GetStickyPosts()
    {
        // Initialize
        List<Post> posts = await _client.Posts.GetStickyPostsAsync(cancellationToken: TestContext.CancellationToken);

        foreach (Post post in posts)
        {
            Assert.IsTrue(post.Sticky);
        }
    }

    [TestMethod]
    public async Task GetPostsByCategory()
    {
        // This CategoryID MUST exists at ApiCredentials.WordPressUri
        int category = 1;
        // Initialize
        List<Post> posts = await _client.Posts.GetPostsByCategoryAsync(category, cancellationToken: TestContext.CancellationToken);

        foreach (Post post in posts)
        {
            Assert.Contains(category, post.Categories!.ToList());
        }
    }

    [TestMethod]
    public async Task GetPostsByTag()
    {
        // This TagID MUST exists at ApiCredentials.WordPressUri
        int tag = 12;
        // Initialize
        List<Post> posts = await _client.Posts.GetPostsByTagAsync(tag, cancellationToken: TestContext.CancellationToken);

        foreach (Post post in posts)
        {
            Assert.Contains(tag, post.Tags!.ToList());
        }
    }

    [TestMethod]
    public async Task GetPostsByAuthor()
    {
        // This AuthorID MUST exists at ApiCredentials.WordPressUri
        int author = 2;
        // Initialize
        List<Post> posts = await _client.Posts.GetPostsByAuthorAsync(author, cancellationToken: TestContext.CancellationToken);

        foreach (Post post in posts)
        {
            Assert.AreEqual(author, post.Author);
        }
    }

    [TestMethod]
    public async Task GetPostsBySearch()
    {
        // This search term MUST be used at least once
        string search = "hello";
        // Initialize
        List<Post> posts = await _client.Posts.GetPostsBySearchAsync(search, cancellationToken: TestContext.CancellationToken);

        foreach (Post post in posts)
        {
            bool containsOnContentOrTitle = false;

            if (post.Content!.Rendered!.ToUpper().Contains(search.ToUpper()) || post.Title!.Rendered!.ToUpper().Contains(search.ToUpper()))
            {
                containsOnContentOrTitle = true;
            }

            Assert.IsTrue(containsOnContentOrTitle);
        }
    }

    [TestMethod]
    public async Task Authorize()
    {
        bool validToken = await _clientAuth.Auth.IsValidJWTokenAsync(TestContext.CancellationToken);
        Assert.IsTrue(validToken);
    }

    public TestContext TestContext { get; set; }
}
