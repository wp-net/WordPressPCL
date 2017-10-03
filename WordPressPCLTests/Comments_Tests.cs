using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCLTests.Utility;
using WordPressPCL;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using System.Net;
using System.Linq;
using Newtonsoft.Json;

namespace WordPressPCLTests
{
    [TestClass]
    public class Comments_Tests
    {
        [TestMethod]
        public async Task Comments_Create()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var IsValidToken = await client.IsValidJWToken();
            Assert.IsTrue(IsValidToken);

            var posts = await client.Posts.GetAll();
            var postId = posts.First().Id;

            var me = await client.Users.GetCurrentUser();

            // Create random content to prevent duplicate commment errors
            var random = new Random();
            var content = $"TestComment {random.Next(0, 10000)}";
            var comment = new Comment()
            {
                Content = new Content(content),
                PostId = postId,
                AuthorId = me.Id,
                AuthorEmail = "test@test.com",
                AuthorName = me.Name
            };
            var resultComment = await client.Comments.Create(comment);
            Assert.IsNotNull(resultComment);

            // Posting same comment twice should fail
            var secondResultComment = await client.Comments.Create(comment);
            Assert.IsNull(secondResultComment);
        }
        [TestMethod]
        public async Task Comments_Read()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var comments = await client.Comments.GetAll();

            if (comments.Count() == 0)
            {
                Assert.Inconclusive("no comments to test");
            }

            foreach (var comment in comments)
            {
                // test Date parsing was successfull
                Assert.IsNotNull(comment.Date);
                Assert.AreNotEqual(DateTime.Now, comment.Date);
                Assert.AreNotEqual(DateTime.MaxValue, comment.Date);
                Assert.AreNotEqual(DateTime.MinValue, comment.Date);

                Assert.IsNotNull(comment.DateGmt);
                Assert.AreNotEqual(DateTime.Now, comment.DateGmt);
                Assert.AreNotEqual(DateTime.MaxValue, comment.DateGmt);
                Assert.AreNotEqual(DateTime.MinValue, comment.DateGmt);
            }
        }

        [TestMethod]
        public async Task Comments_Get()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var comments = await client.Comments.Get();

            if (comments.Count() == 0)
            {
                Assert.Inconclusive("no comments to test");
            }
            Assert.IsNotNull(comments);

        }

        [TestMethod]
        public async Task Comments_Update()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var me = await client.Users.GetCurrentUser();
            var queryBuilder = new CommentsQueryBuilder()
            {
                Authors = new int[] { me.Id }
            };
            var comments = await client.Comments.Query(queryBuilder, true);
            var comment = comments.FirstOrDefault();
            if (comment == null)
            {
                Assert.Inconclusive();
            }
            var random = new Random();
            var title = $"TestComment {random.Next(0, 10000)}";
            comment.Content.Raw = title;
            var commentUpdated = await client.Comments.Update(comment);
            Assert.AreEqual(commentUpdated.Content.Raw, title);
        }
        [TestMethod]
        public async Task Comments_Delete()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            
            var posts = await client.Posts.GetAll();
            var postId = posts.First().Id;

            var me = await client.Users.GetCurrentUser();

            // Create random content to prevent duplicate commment errors
            var random = new Random();
            var comment = new Comment()
            {
                Content = new Content($"Testcomment {random.Next(0, 10000)}"),
                PostId = postId,
                AuthorId = me.Id,
                AuthorEmail = "test@test.com",
                AuthorName = me.Name
            };
            var resultComment = await client.Comments.Create(comment);

            var response = await client.Comments.Delete(resultComment.Id);
            Assert.IsTrue(response.IsSuccessStatusCode);

        }
        [TestMethod]
        public async Task Comments_Query()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var queryBuilder = new CommentsQueryBuilder()
            {
                Page = 1,
                PerPage = 15,
                OrderBy = CommentsOrderBy.Id,
                Order = Order.DESC,
            };
            var queryresult = await client.Comments.Query(queryBuilder);
            Assert.AreEqual(queryBuilder.BuildQueryURL(), "?page=1&per_page=15&orderby=id&order=desc");
            Assert.IsNotNull(queryresult);
            Assert.AreNotSame(queryresult.Count(), 0);
        }

    }

}