using System;
using System.Collections.Generic;
using System.Linq;
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
    private static WordPressClient _client;
    private static WordPressClient _clientAuth;

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
        Post createdPost = await _clientAuth.Posts.CreateAsync(post);


        Assert.AreEqual(post.Content.Raw, createdPost.Content.Raw);
        Assert.IsTrue(createdPost.Content.Rendered.Contains(post.Content.Rendered));
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
        Post createdPost = await _clientAuth.Posts.CreateAsync(post);

        PostsQueryBuilder queryBuilder = new()
        {
            PerPage = 50,
            Page = 1,
            Order = Order.DESC,
            Statuses = new List<Status> { Status.Future, Status.Pending }
        };

        List<Post> postsTask = await _clientAuth.Posts.QueryAsync(queryBuilder, true);
        Assert.IsTrue(postsTask.Any(x => x.Title.Rendered == title));

        Assert.AreEqual(post.Content.Raw, createdPost.Content.Raw);
        Assert.IsTrue(createdPost.Content.Rendered.Contains(post.Content.Rendered));
    }

    [TestMethod]
    public async Task Posts_Read()
    {
        List<Post> posts = await _clientAuth.Posts.QueryAsync(new PostsQueryBuilder());
        Assert.IsNotNull(posts);
        Assert.AreNotEqual(posts.Count, 0);

        List<Post> postsEdit = await _clientAuth.Posts.QueryAsync(new PostsQueryBuilder()
        {
            Context = Context.Edit,
            PerPage = 1,
            Page = 1
        }, true);
        Assert.AreEqual(1, postsEdit.Count);
        Assert.IsNotNull(postsEdit.FirstOrDefault());
        Assert.IsNotNull(postsEdit.FirstOrDefault().Content.Raw);
    }

    [TestMethod]
    public async Task Posts_Get()
    {
        List<Post> posts = await _client.Posts.GetAsync();
        Assert.IsNotNull(posts);
        Assert.AreNotEqual(posts.Count, 0);
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
            Post createdPost = await _clientAuth.Posts.CreateAsync(post);
        }

        List<Post> posts = await _client.Posts.GetAllAsync();
        int postsCount = await _client.Posts.GetCountAsync();
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
        }, false);
        Assert.IsNotNull(posts);
    }

    [TestMethod]
    public async Task Posts_Update()
    {
        string testContent = $"Test {System.Guid.NewGuid()}";
        List<Post> posts = await _clientAuth.Posts.GetAllAsync();
        Assert.IsTrue(posts.Count > 0);

        // edit first post and update it
        Post post = await _clientAuth.Posts.GetByIDAsync(posts.First().Id);
        post.Content.Raw = testContent;
        Post updatedPost = await _clientAuth.Posts.UpdateAsync(post);
        Assert.AreEqual(updatedPost.Content.Raw, testContent);
        Assert.IsTrue(updatedPost.Content.Rendered.Contains(testContent));
    }

    [TestMethod]
    public async Task Posts_Delete()
    {
        Post post = new()
        {
            Title = new Title("Title 1"),
            Content = new Content("Content PostCreate")
        };
        Post createdPost = await _clientAuth.Posts.CreateAsync(post);
        Assert.IsNotNull(createdPost);

        bool resonse = await _clientAuth.Posts.DeleteAsync(createdPost.Id);
        Assert.IsTrue(resonse);

        await Assert.ThrowsExceptionAsync<WPException>(async () =>
        {
            Post postById = await _clientAuth.Posts.GetByIDAsync(createdPost.Id);
        });

        // Post should be available in trash
        PostsQueryBuilder queryBuilder = new()
        {
            Statuses = new List<Status> { Status.Trash },
            PerPage = 100
        };
        List<Post> posts = await _clientAuth.Posts.QueryAsync(queryBuilder, true);

        Post deletedPost = posts.Where(x => x.Id == createdPost.Id).FirstOrDefault();
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
        List<Post> queryresult = await _clientAuth.Posts.QueryAsync(queryBuilder);
        Assert.AreEqual("?page=1&per_page=15&orderby=title&status=publish&order=asc&_embed=true&context=view", queryBuilder.BuildQuery());
        Assert.IsNotNull(queryresult);
        Assert.AreNotSame(queryresult.Count, 0);
    }
}
