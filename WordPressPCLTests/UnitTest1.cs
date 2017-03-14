using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCLTests.Utility;
using WordPressPCL;
using System.Threading.Tasks;
using WordPressPCL.Models;
using System.Net;

namespace WordPressPCLTests
{
    [TestClass]
    public class UnitTest1
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
        }


        [TestMethod]
        public async Task JWTAuthTest()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            client.Username = ApiCredentials.Username;
            client.Password = ApiCredentials.Password;
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken();
            Assert.IsNotNull(client.JWToken);
            var IsValidToken = await client.IsValidJWToken();
            Assert.IsTrue(IsValidToken);
        }


        [TestMethod]
        public async Task CreateAndDeleteComment()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            client.Username = ApiCredentials.Username;
            client.Password = ApiCredentials.Password;
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken();
            var IsValidToken = await client.IsValidJWToken();
            Assert.IsTrue(IsValidToken);

            var posts = await client.ListPosts();
            var postId = posts[0].Id;

            var me = await client.GetCurrentUser();

            var comment = new CommentCreate()
            {
                Content = "Testcomment",
                PostId = postId,
                AuthorId = me.id
            };
            var resultComment = await client.CreateComment(comment, postId);
            Assert.IsNotNull(resultComment);
        }


        [TestMethod]
        public async Task CreateAndDeletePostTest()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            client.Username = ApiCredentials.Username;
            client.Password = ApiCredentials.Password;
            client.AuthMethod = AuthMethod.JWT;
            await client.RequestJWToken();
            var IsValidToken = await client.IsValidJWToken();
            Assert.IsTrue(IsValidToken);
            var newpost = new PostCreate()
            {
                Content = "Testcontent"
            };
            var resultPost = await client.CreatePost(newpost);
            Assert.IsNotNull(resultPost.Id);

            //var del = await client.DeletePost(resultPost.Id);
            //Assert.IsTrue(del == HttpStatusCode.OK);


            }
        }
}
