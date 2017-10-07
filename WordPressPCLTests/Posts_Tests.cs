using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCLTests.Utility;
using System.Linq;
using WordPressPCL;
using WordPressPCL.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class Posts_Tests
    {
        [TestMethod]
        public async Task Posts_Create()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var post = new Post()
            {
                Title = new Title("Title 1"),
                Content = new Content("Content PostCreate")
            };
            var createdPost = await client.Posts.Create(post);


            Assert.AreEqual(post.Content.Raw, createdPost.Content.Raw);
            Assert.IsTrue(createdPost.Content.Rendered.Contains(post.Content.Rendered));
        }

        [TestMethod]
        public async Task Posts_Read()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var posts = await client.Posts.Query(new PostsQueryBuilder());
            Assert.IsNotNull(posts);
            Assert.AreNotEqual(posts.Count(), 0);

            var postsEdit = await client.Posts.Query(new PostsQueryBuilder()
            { 
                Context = Context.Edit,
                PerPage = 1,
                Page = 1
            }, true);
            Assert.AreEqual(1, postsEdit.Count());
            Assert.IsNotNull(postsEdit.FirstOrDefault());
            Assert.IsNotNull(postsEdit.FirstOrDefault().Content.Raw);
        }

        [TestMethod]
        public async Task Posts_Get()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var posts = await client.Posts.Get();
            Assert.IsNotNull(posts);
            Assert.AreNotEqual(posts.Count(), 0);
        }

        [TestMethod]
        public async Task Posts_Update()
        {
            var testContent = "Test" + new Random().Next() ;
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var posts = await client.Posts.GetAll();
            Assert.IsTrue(posts.Count() > 0);

            // edit first post and update it
            var post = await client.Posts.GetByID(posts.First().Id);
            post.Content.Raw = testContent;
            var updatedPost = await client.Posts.Update(post);
            Assert.AreEqual(updatedPost.Content.Raw, testContent);
            Assert.IsTrue(updatedPost.Content.Rendered.Contains(testContent));
        }

        [TestMethod]
        public async Task Posts_Delete()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var post = new Post()
            {
                Title = new Title("Title 1"),
                Content = new Content("Content PostCreate")
            };
            var createdPost = await client.Posts.Create(post);
            Assert.IsNotNull(createdPost);

            var resonse = await client.Posts.Delete(createdPost.Id);
            Assert.IsTrue(resonse.IsSuccessStatusCode);

            var postById = await client.Posts.GetByID(createdPost.Id);
            Assert.IsNull(postById);
        }

        [TestMethod]
        public async Task Posts_Query()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var queryBuilder = new PostsQueryBuilder()
            {
                Page = 1,
                PerPage = 15,
                OrderBy = PostsOrderBy.Title,
                Order = Order.DESC,
                Statuses = new Status[] { Status.Publish },
                Embed = true
            };
            var queryresult = await client.Posts.Query(queryBuilder);
            Assert.AreEqual(queryBuilder.BuildQueryURL(), "?page=1&per_page=15&orderby=title&status=publish&order=desc&_embed=true");
            Assert.IsNotNull(queryresult);
            Assert.AreNotSame(queryresult.Count(), 0);
        }
    }
}
