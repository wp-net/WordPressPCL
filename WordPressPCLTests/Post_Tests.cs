using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using WordPressPCLTests.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class Post_Tests
    {
        [TestMethod]
        public async Task Update_Post_TestAsync()
        {
            var testContent = "Test" + new Random().Next() ;
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var posts = await client.ListPosts();
            Assert.IsTrue(posts.Count > 0);

            // edit first post and update it
            var post = await client.GetPost(posts[0].Id);
            post.Content.Raw = testContent;
            var updatedPost = await client.UpdatePost(post);
            Assert.AreEqual(updatedPost.Content.Raw, testContent);
            Assert.IsTrue(updatedPost.Content.Rendered.Contains(testContent));
        }

        [TestMethod]
        public async Task Create_Post_Test()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var post = new Post()
            {
                Title = new Title()
                {
                    Raw = "New Title"
                },
                Content = new Content()
                {
                    Raw = "Test Raw Content"
                },
                Date = DateTime.Now,
                DateGmt = DateTime.UtcNow
            };
            var post2 = new PostCreate()
            {
                Title = "Title 1",
                Content = "Content PostCreate"
            };
            var createdPost = await client.CreatePost(post2);


            Assert.AreEqual(post2.Content, createdPost.Content.Raw);
            Assert.IsTrue(createdPost.Content.Rendered.Contains(post2.Content));
        }





    }
}
