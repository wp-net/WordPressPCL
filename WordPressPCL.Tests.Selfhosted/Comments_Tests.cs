using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Tests.Selfhosted.Utility;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using System.Linq;
using WordPressPCL.Models.Exceptions;
using System.Collections.Generic;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Comments_Tests
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
    public async Task Comments_Create()
    {
        List<Post> posts = await _clientAuth.Posts.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        int postId = posts.First().Id;

        User me = await _clientAuth.Users.GetCurrentUserAsync(TestContext.CancellationToken);

        // Create random content to prevent duplicate commment errors
        string content = $"TestComment {System.Guid.NewGuid()}";
        Comment comment = new()
        {
            Content = new Content(content),
            PostId = postId,
            AuthorId = me.Id,
            AuthorEmail = "test@test.com",
            AuthorName = me.Name
        };
        Comment resultComment = await _clientAuth.Comments.CreateAsync(comment, TestContext.CancellationToken);
        Assert.IsNotNull(resultComment);

        // Posting same comment twice should fail
        await Assert.ThrowsExactlyAsync<WPException>(async () =>
        {
            Comment secondResultComment = await _clientAuth.Comments.CreateAsync(comment, TestContext.CancellationToken);
        });
    }
    [TestMethod]
    public async Task Comments_Read()
    {
        List<Comment> comments = await _client.Comments.GetAllAsync(cancellationToken: TestContext.CancellationToken);

        if (!comments.Any())
        {
            Assert.Inconclusive("no comments to test");
        }

        foreach (Comment comment in comments)
        {
            // test Date parsing was successfull
            Assert.AreNotEqual(DateTime.Now, comment.Date);
            Assert.AreNotEqual(DateTime.MaxValue, comment.Date);
            Assert.AreNotEqual(DateTime.MinValue, comment.Date);

            Assert.AreNotEqual(DateTime.Now, comment.DateGmt);
            Assert.AreNotEqual(DateTime.MaxValue, comment.DateGmt);
            Assert.AreNotEqual(DateTime.MinValue, comment.DateGmt);
        }
    }

    [TestMethod]
    public async Task Comments_Get()
    {
        List<Comment> comments = await _client.Comments.GetAsync(cancellationToken: TestContext.CancellationToken);

        if (!comments.Any())
        {
            Assert.Inconclusive("no comments to test");
        }
        Assert.IsNotNull(comments);

    }

    [TestMethod]
    public async Task Comments_Update()
    {
        User me = await _clientAuth.Users.GetCurrentUserAsync(TestContext.CancellationToken);
        CommentsQueryBuilder queryBuilder = new()
        {
            Authors = new List<int> { me.Id }
        };
        List<Comment> comments = await _clientAuth.Comments.QueryAsync(queryBuilder, true, TestContext.CancellationToken);
        Comment? comment = comments.FirstOrDefault();
        if (comment == null)
        {
            Assert.Inconclusive();
        }
        string title = $"TestComment {System.Guid.NewGuid()}";
        comment.Content!.Raw = title;
        Comment commentUpdated = await _clientAuth.Comments.UpdateAsync(comment, TestContext.CancellationToken);
        Assert.AreEqual(title, commentUpdated.Content!.Raw);
    }

    [TestMethod]
    public async Task Comments_Delete()
    {
        List<Post> posts = await _clientAuth.Posts.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        int postId = posts.First().Id;

        User me = await _clientAuth.Users.GetCurrentUserAsync(TestContext.CancellationToken);

        // Create random content to prevent duplicate commment errors
        Comment comment = new()
        {
            Content = new Content($"Testcomment {System.Guid.NewGuid()}"),
            PostId = postId,
            AuthorId = me.Id,
            AuthorEmail = "test@test.com",
            AuthorName = me.Name
        };
        Comment resultComment = await _clientAuth.Comments.CreateAsync(comment, TestContext.CancellationToken);

        bool response = await _clientAuth.Comments.DeleteAsync(resultComment.Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsTrue(response);

    }

    [TestMethod]
    public async Task Comments_Query()
    {
        CommentsQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 15,
            OrderBy = CommentsOrderBy.Id,
            Order = Order.DESC,
        };
        List<Comment> queryresult = await _clientAuth.Comments.QueryAsync(queryBuilder, cancellationToken: TestContext.CancellationToken);
        Assert.AreEqual("?page=1&per_page=15&orderby=id&order=desc&context=view", queryBuilder.BuildQuery());
        Assert.IsNotNull(queryresult);
        Assert.AreNotEqual(0, queryresult.Count);
    }

    // TODO: Can't create pending comment from Admin Account
    //[TestMethod]
    public static async Task Comments_Query_Pending()
    {
        // Create new pending comment
        List<Post> posts = await _clientAuth.Posts.GetAllAsync();
        int postId = posts.First().Id;
        User me = await _clientAuth.Users.GetCurrentUserAsync();

        // Create random content to prevent duplicate commment errors
        string content = $"TestComment {System.Guid.NewGuid()}";
        Comment comment = new()
        {
            Content = new Content(content),
            PostId = postId,
            AuthorId = me.Id,
            AuthorEmail = "test@test.com",
            AuthorName = me.Name,
            Status = CommentStatus.Pending
        };
        Comment resultComment = await _clientAuth.Comments.CreateAsync(comment);
        Assert.IsNotNull(resultComment);
        Assert.AreEqual(CommentStatus.Pending, resultComment.Status);

        // this test needs a pending comment added manually for now.
        CommentsQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 15,
            OrderBy = CommentsOrderBy.Id,
            Order = Order.DESC,
            Statuses = new List<CommentStatus> { CommentStatus.Pending }
        };
        List<Comment> queryresult = await _clientAuth.Comments.QueryAsync(queryBuilder, true);
        string querystring = "page=1&per_page=15&orderby=id&status=hold";
        Assert.AreEqual(querystring, queryBuilder.BuildQuery());
        Assert.IsNotNull(queryresult);
        Assert.AreNotEqual(0, queryresult.Count);

        // Delete Pending comment
        await _clientAuth.Comments.DeleteAsync(resultComment.Id);
    }

    [TestMethod]
    public async Task Comments_GetAllForPost()
    {
        User me = await _clientAuth.Users.GetCurrentUserAsync(TestContext.CancellationToken);

        // create test post and add comments
        Post post = new()
        {
            Title = new Title("Title 1"),
            Content = new Content("Content PostCreate")
        };
        Post createdPost = await _clientAuth.Posts.CreateAsync(post, TestContext.CancellationToken);
        Assert.IsNotNull(createdPost);

        for (int i = 0; i < 30; i++)
        {
            // Create random content to prevent duplicate commment errors
            string content = $"TestComment {System.Guid.NewGuid()}";
            Comment comment = new()
            {
                Content = new Content(content),
                PostId = createdPost.Id,
                AuthorId = me.Id,
                AuthorEmail = "test@test.com",
                AuthorName = me.Name
            };
            Comment resultComment = await _clientAuth.Comments.CreateAsync(comment, TestContext.CancellationToken);
            Assert.IsNotNull(resultComment);
        }

        // shoud work without auth
        WordPressClient nonauthclient = ClientHelper.GetWordPressClient();
        List<Comment> comments = await nonauthclient.Comments.GetCommentsForPostAsync(createdPost.Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsLessThanOrEqualTo(10, comments.Count);

        List<Comment> allComments = await nonauthclient.Comments.GetAllCommentsForPostAsync(createdPost.Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsGreaterThan(20, allComments.Count);

        // cleanup
        bool result = await _clientAuth.Posts.DeleteAsync(createdPost.Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsTrue(result);
    }

    public TestContext TestContext { get; set; }
}

