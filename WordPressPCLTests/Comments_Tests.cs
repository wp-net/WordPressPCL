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

            var comment = new Comment()
            {
                Content = new Content("Testcomment"),
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
        public async Task Comments_Update()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public async Task Comments_Delete()
        {
            Assert.Inconclusive();
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