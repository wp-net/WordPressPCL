using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Tests.Hosted.Utility;
using System.Threading.Tasks;
using WordPressPCL.Models;
using System.Linq;

namespace WordPressPCL.Hosted;

[TestClass]
public class Basic_Tests
{
    private static WordPressClient _client = null!;

    [ClassInitialize]
    public static void Init(TestContext testContext)
    {
        _client = ClientHelper.GetWordPressClient();
    }

    [TestMethod]
    public async Task Hosted_BasicSetupTest()
    {
        // Initialize
        Assert.IsNotNull(_client);
        // Posts
        var posts = await _client.Posts.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(posts);
        Assert.AreNotEqual(0, posts!.Count);
    }

    [TestMethod]
    public async Task Hosted_GetFirstPostTest()
    {
        // Initialize
        var posts = await _client.Posts.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        var post = await _client.Posts.GetByIdAsync(posts.First().Id, cancellationToken: TestContext.CancellationToken);
        Assert.AreEqual(post.Id, posts.First().Id);
        Assert.IsTrue(!String.IsNullOrEmpty(posts.First().Content!.Rendered));
    }

    [TestMethod]
    public async Task Hosted_GetStickyPosts()
    {
        // Initialize
        var posts = await _client.Posts.GetStickyPostsAsync(cancellationToken: TestContext.CancellationToken);

        foreach (Post post in posts)
        {
            Assert.IsTrue(post.Sticky);
        }
    }

    [TestMethod]
    public async Task Hosted_GetPostsByCategory()
    {
        // This CategoryID MUST exists at ApiCredentials.WordPressUri
        int category = 1;
        // Initialize
        var posts = await _client.Posts.GetPostsByCategoryAsync(category, cancellationToken: TestContext.CancellationToken);

        foreach (Post post in posts)
        {
            Assert.Contains(category, post.Categories!.ToList());
        }
    }

    [TestMethod]
    public async Task Hosted_GetPostsByTag()
    {
        var tags = await _client.Tags.GetAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsTrue(tags.Any(), "No tags returned from the API; cannot test posts by tag.");
        int tagId = tags.First().Id;

        // Initialize
        var posts = await _client.Posts.GetPostsByTagAsync(tagId, cancellationToken: TestContext.CancellationToken);
        Assert.AreNotEqual(0, posts.Count);
        foreach (Post post in posts)
        {
            Assert.Contains(tagId, post.Tags!.ToList());
        }
    }

    [TestMethod]
    public async Task Hosted_GetPostsByAuthor()
    {
        // This AuthorID MUST exists at ApiCredentials.WordPressUri
        int author = 3722200;
        // Initialize
        var posts = await _client.Posts.GetPostsByAuthorAsync(author, cancellationToken: TestContext.CancellationToken);
        Assert.AreNotEqual(0, posts.Count);
        foreach (Post post in posts)
        {
            Assert.AreEqual(author, post.Author);
        }
    }

    [TestMethod]
    public async Task Hosted_GetPostsBySearch()
    {
        // This search term MUST be used at least once
        string search = "hello";
        // Initialize
        var posts = await _client.Posts.GetPostsBySearchAsync(search, cancellationToken: TestContext.CancellationToken);
        Assert.AreNotEqual(0, posts.Count);
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

    public TestContext TestContext { get; set; }
}
