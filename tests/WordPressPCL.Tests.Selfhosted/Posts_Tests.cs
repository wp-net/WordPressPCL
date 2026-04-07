using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;
using WordPressPCL.Tests.Selfhosted.Utility;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Posts_Tests
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
    public async Task Posts_Create()
    {
        Post post = new()
        {
            Title = new Title("Title 1"),
            Content = new Content("Content PostCreate")
        };
        Post createdPost = await _clientAuth.Posts.CreateAsync(post, TestContext.CancellationToken);


        Assert.AreEqual(post.Content!.Raw, createdPost.Content!.Raw);
        Assert.Contains(post.Content.Rendered, createdPost.Content!.Rendered);
    }

    [TestMethod]
    public async Task Posts_Create_Scheduled()
    {
        string title = "Scheduled Title " + System.Guid.NewGuid().ToString();
        Post post = new()
        {
            Title = new Title(title),
            Content = new Content("Content PostCreateScheduled"),
            Date = DateTime.Now + TimeSpan.FromDays(5),
        };
        Post createdPost = await _clientAuth.Posts.CreateAsync(post, TestContext.CancellationToken);

        PostsQueryBuilder queryBuilder = new()
        {
            PerPage = 50,
            Page = 1,
            Order = Order.DESC,
            Statuses = new List<Status> { Status.Future, Status.Pending }
        };

        List<Post> postsTask = await _clientAuth.Posts.QueryAsync(queryBuilder, true, TestContext.CancellationToken);
        Assert.Contains(x => x.Title!.Rendered == title, postsTask);

        Assert.AreEqual(post.Content!.Raw, createdPost.Content!.Raw);
        Assert.Contains(post.Content.Rendered, createdPost.Content!.Rendered);
    }

    [TestMethod]
    public async Task Posts_Read()
    {
        List<Post> posts = await _clientAuth.Posts.QueryAsync(new PostsQueryBuilder(), cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(posts);
        Assert.AreNotEqual(0, posts.Count);

        List<Post> postsEdit = await _clientAuth.Posts.QueryAsync(new PostsQueryBuilder()
        {
            Context = Context.Edit,
            PerPage = 1,
            Page = 1
        }, true, TestContext.CancellationToken);
        Assert.HasCount(1, postsEdit);
        Assert.IsNotNull(postsEdit.FirstOrDefault());
        Assert.IsNotNull(postsEdit.FirstOrDefault()!.Content!.Raw);
    }

    [TestMethod]
    public async Task Posts_Get()
    {
        List<Post> posts = await _client.Posts.GetAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(posts);
        Assert.AreNotEqual(0, posts.Count);
    }

    [TestMethod]
    public async Task Posts_Count_Should_Equal_Number_Of_Posts()
    {
        // Create 100+ posts to test multi-page GetAll
        List<Post> postsCreate = Enumerable.Range(0, 110).Select(x =>
            new Post()
            {
                Title = new Title($"{System.Guid.NewGuid()} {x}"),
                Content = new Content("Content PostCreate")
            }
        ).ToList();
        foreach (Post post in postsCreate)
        {
            Post createdPost = await _clientAuth.Posts.CreateAsync(post, TestContext.CancellationToken);
        }

        List<Post> posts = await _client.Posts.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        int postsCount = await _client.Posts.GetCountAsync(TestContext.CancellationToken);
        Assert.AreEqual(posts.Count, postsCount);
    }

    [TestMethod]
    public async Task Posts_Read_Embedded()
    {
        List<Post> posts = await _client.Posts.QueryAsync(new PostsQueryBuilder()
        {
            PerPage = 10,
            Page = 1,
            Embed = true
        }, false, TestContext.CancellationToken);
        Assert.IsNotNull(posts);
    }

    [TestMethod]
    public async Task Posts_Update()
    {
        string testContent = $"Test {System.Guid.NewGuid()}";
        List<Post> posts = await _clientAuth.Posts.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotEmpty(posts);

        // edit first post and update it
        Post post = await _clientAuth.Posts.GetByIdAsync(posts.First().Id, cancellationToken: TestContext.CancellationToken);
        post.Content!.Raw = testContent;
        Post updatedPost = await _clientAuth.Posts.UpdateAsync(post, TestContext.CancellationToken);
        Assert.AreEqual(updatedPost.Content!.Raw, testContent);
        Assert.Contains(testContent, updatedPost.Content!.Rendered);
    }

    [TestMethod]
    public async Task Posts_Delete()
    {
        Post post = new()
        {
            Title = new Title("Title 1"),
            Content = new Content("Content PostCreate")
        };
        Post createdPost = await _clientAuth.Posts.CreateAsync(post, TestContext.CancellationToken);
        Assert.IsNotNull(createdPost);

        bool response = await _clientAuth.Posts.DeleteAsync(createdPost.Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsTrue(response);

        await Assert.ThrowsExactlyAsync<WPException>(async () =>
        {
            Post postById = await _clientAuth.Posts.GetByIdAsync(createdPost.Id, cancellationToken: TestContext.CancellationToken);
        });

        // Post should be available in trash
        PostsQueryBuilder queryBuilder = new()
        {
            Statuses = new List<Status> { Status.Trash },
            PerPage = 100
        };
        List<Post> posts = await _clientAuth.Posts.QueryAsync(queryBuilder, true, TestContext.CancellationToken);

        Post? deletedPost = posts.Where(x => x.Id == createdPost.Id).FirstOrDefault();
        Assert.IsNotNull(deletedPost);
    }

    [TestMethod]
    public async Task Posts_Query()
    {
        PostsQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 15,
            OrderBy = PostsOrderBy.Title,
            Order = Order.ASC,
            Statuses = new List<Status>() { Status.Publish },
            Embed = true
        };
        List<Post> queryresult = await _clientAuth.Posts.QueryAsync(queryBuilder, cancellationToken: TestContext.CancellationToken);
        Assert.AreEqual("?page=1&per_page=15&orderby=title&status=publish&order=asc&_embed=true&context=view", queryBuilder.BuildQuery());
        Assert.IsNotNull(queryresult);
        Assert.AreNotEqual(0, queryresult.Count);
    }

    [TestMethod]
    public async Task Posts_GetPaged_Returns_Metadata()
    {
        PagedResult<Post> paged = await _client.Posts.GetPagedAsync(page: 1, perPage: 5, cancellationToken: TestContext.CancellationToken);

        Assert.IsNotNull(paged);
        Assert.IsNotNull(paged.Items);
        Assert.IsLessThanOrEqualTo(5, paged.Items.Count);
        Assert.IsGreaterThan(0, paged.TotalCount, "TotalCount should be populated from X-WP-Total");
        Assert.IsGreaterThanOrEqualTo(1, paged.TotalPages, "TotalPages should be populated from X-WP-TotalPages");
    }

    [TestMethod]
    public async Task Posts_QueryPaged_Returns_Metadata()
    {
        PostsQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 3,
            Statuses = new List<Status> { Status.Publish }
        };
        PagedResult<Post> paged = await _client.Posts.QueryPagedAsync(queryBuilder, cancellationToken: TestContext.CancellationToken);

        Assert.IsNotNull(paged);
        Assert.IsNotNull(paged.Items);
        Assert.IsLessThanOrEqualTo(3, paged.Items.Count);
        Assert.IsGreaterThan(0, paged.TotalCount, "TotalCount should be populated from X-WP-Total");
        Assert.IsGreaterThanOrEqualTo(1, paged.TotalPages, "TotalPages should be populated from X-WP-TotalPages");
    }

    [TestMethod]
    public async Task Posts_GetPaged_TotalCount_Matches_GetCount()
    {
        PagedResult<Post> paged = await _client.Posts.GetPagedAsync(page: 1, perPage: 1, cancellationToken: TestContext.CancellationToken);
        int count = await _client.Posts.GetCountAsync(TestContext.CancellationToken);

        Assert.AreEqual(count, paged.TotalCount, "TotalCount from GetPagedAsync should match GetCountAsync");
    }

    [TestMethod]
    public async Task Posts_Meta_Write_And_Read()
    {
        // Arrange — create a post carrying a registered meta value
        string metaValue = $"pcl-meta-{System.Guid.NewGuid()}";
        Post post = new()
        {
            Title = new Title("Meta Test Post"),
            Content = new Content("Meta Test Content"),
            Meta = JsonSerializer.SerializeToElement(new Dictionary<string, object?>
            {
                ["wordpresspcl_test_meta"] = metaValue
            }),
        };

        // Act — create then fetch back (edit context to ensure meta is returned)
        Post createdPost = await _clientAuth.Posts.CreateAsync(post, TestContext.CancellationToken);
        Post fetchedPost = await _clientAuth.Posts.GetByIdAsync(createdPost.Id, cancellationToken: TestContext.CancellationToken);

        // Assert
        Assert.IsNotNull(fetchedPost.Meta, "Meta should not be null when a registered key was written");
        string? readValue = fetchedPost.Meta.Value.GetProperty("wordpresspcl_test_meta").GetString();
        Assert.AreEqual(metaValue, readValue, "Meta value read back should equal the value written");
    }

    [TestMethod]
    public async Task Posts_Meta_Update()
    {
        // Arrange — create a post, then update only the meta field
        Post post = new()
        {
            Title = new Title("Meta Update Test Post"),
            Content = new Content("Meta Update Test Content"),
            Meta = JsonSerializer.SerializeToElement(new Dictionary<string, object?>
            {
                ["wordpresspcl_test_meta"] = "initial-value"
            }),
        };

        Post createdPost = await _clientAuth.Posts.CreateAsync(post, TestContext.CancellationToken);

        // Act — update only the meta field
        string updatedMetaValue = $"updated-{System.Guid.NewGuid()}";
        Post updateRequest = new()
        {
            Id = createdPost.Id,
            Meta = JsonSerializer.SerializeToElement(new Dictionary<string, object?>
            {
                ["wordpresspcl_test_meta"] = updatedMetaValue
            }),
        };
        await _clientAuth.Posts.UpdateAsync(updateRequest, TestContext.CancellationToken);

        // Assert — fetch back and verify
        Post fetchedPost = await _clientAuth.Posts.GetByIdAsync(createdPost.Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(fetchedPost.Meta, "Meta should not be null after update");
        string? readValue = fetchedPost.Meta.Value.GetProperty("wordpresspcl_test_meta").GetString();
        Assert.AreEqual(updatedMetaValue, readValue, "Meta value should reflect the updated value");
    }

    public TestContext TestContext { get; set; } = null!;
}
