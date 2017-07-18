using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCLTests.Utility;
using WordPressPCL;
using System.Threading.Tasks;
using WordPressPCL.Models;
using System.Net;
using System.Linq;

namespace WordPressPCLTests
{
    [TestClass]
    public class Basic_Tests
    {
        [TestMethod]
        public async Task BasicSetupTest()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var posts = await client.Posts.GetAll();
            Assert.IsNotNull(posts);
        }

        [TestMethod]
        public async Task GetFirstPostTest()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.Posts.GetAll();
            var post = await client.Posts.GetByID(posts.First().Id);
            Assert.IsTrue(posts.First().Id == post.Id);
            Assert.IsTrue(!String.IsNullOrEmpty(posts.First().Content.Rendered));
        }

        [TestMethod]
        public async Task GetStickyPosts()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.Posts.GetStickyPosts();

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
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.Posts.GetPostsByCategory(category);

            foreach (Post post in posts)
            {
                Assert.IsTrue(post.Categories.ToList().Contains(category));
            }
        }

        [TestMethod]
        public async Task GetPostsByTag()
        {
            // This TagID MUST exists at ApiCredentials.WordPressUri
            int tag = 12;
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.Posts.GetPostsByTag(tag);

            foreach (Post post in posts)
            {
                Assert.IsTrue(post.Tags.ToList().Contains(tag));
            }
        }

        [TestMethod]
        public async Task GetPostsByAuthor()
        {
            // This AuthorID MUST exists at ApiCredentials.WordPressUri
            int author = 2;
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.Posts.GetPostsByAuthor(author);

            foreach (Post post in posts)
            {
                Assert.IsTrue(post.Author == author);
            }
        }

        [TestMethod]
        public async Task GetPostsBySearch()
        {
            // This search term MUST be used at least once
            string search = "hello";
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.Posts.GetPostsBySearch(search);

            foreach (Post post in posts)
            {
                bool containsOnContentOrTitle = false;

                if (post.Content.Rendered.ToUpper().Contains(search.ToUpper()) || post.Title.Rendered.ToUpper().Contains(search.ToUpper()))
                {
                    containsOnContentOrTitle = true;
                }

                Assert.IsTrue(containsOnContentOrTitle);
            }
        }


        [TestMethod]
        public async Task JWTAuthTest()
        {
            // get JWT Authenticated client
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            //Assert.IsNotNull(client.JWToken);
            var IsValidToken = await client.IsValidJWToken();
            Assert.IsTrue(IsValidToken);
        }
    }
}
