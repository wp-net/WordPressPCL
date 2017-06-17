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
            var posts = await client.ListPosts();
            Assert.IsNotNull(posts);
        }

        [TestMethod]
        public async Task GetFirstPostTest()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.ListPosts();
            var post = await client.GetPost(posts[0].Id);
            Assert.IsTrue(posts[0].Id == post.Id);
            Assert.IsTrue(!String.IsNullOrEmpty(posts[0].Content.Rendered));
        }

        [TestMethod]
        public async Task GetStickyPosts()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.ListStickyPosts();

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
            var posts = await client.ListPostsByCategory(category);

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
            var posts = await client.ListPostsByTag(tag);

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
            var posts = await client.ListPostsByAuthor(author);

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
            var posts = await client.ListPostsBySearch(search);

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
        public async Task GetComments()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var comments = await client.ListComments();

            if(comments.Count == 0)
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
        public async Task JWTAuthTest()
        {
            // get JWT Authenticated client
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            Assert.IsNotNull(client.JWToken);
            var IsValidToken = await client.IsValidJWToken();
            Assert.IsTrue(IsValidToken);
        }

        [TestMethod]
        public async Task CreateAndDeleteComment()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var IsValidToken = await client.IsValidJWToken();
            Assert.IsTrue(IsValidToken);

            var posts = await client.ListPosts();
            var postId = posts[0].Id;

            var me = await client.GetCurrentUser();

            var comment = new CommentCreate()
            {
                Content = "Testcomment",
                PostId = postId,
                AuthorId = me.Id,
                AuthorEmail = "test@test.com",
                AuthorName = me.Name
            };
            var resultComment = await client.CreateComment(comment, postId);
            Assert.IsNotNull(resultComment);

            // Posting same comment twice should fail
            var secondResultComment = await client.CreateComment(comment, postId);
            Assert.IsNull(secondResultComment);


            var del = await client.DeleteComment(resultComment.Id);
            Assert.IsTrue(del.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task CreateAndDeletePostTest()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var IsValidToken = await client.IsValidJWToken();
            Assert.IsTrue(IsValidToken);
            var newpost = new PostCreate()
            {
                Content = "Testcontent"
            };
            var resultPost = await client.CreatePost(newpost);
            Assert.IsNotNull(resultPost.Id);

            var del = await client.DeletePost(resultPost.Id);
            Assert.IsTrue(del.IsSuccessStatusCode);
        }
    }
}
