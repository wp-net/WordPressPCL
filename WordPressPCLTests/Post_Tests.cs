using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using WordPressPCLTests.Utility;
using System.Linq;

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
            var post2 = new Post()
            {
                Title = new Title("Title 1"),
                Content = new Content("Content PostCreate")
            };
            var createdPost = await client.Posts.Create(post2);


            Assert.AreEqual(post2.Content.Raw, createdPost.Content.Raw);
            Assert.IsTrue(createdPost.Content.Rendered.Contains(post2.Content.Rendered));
        }





    }
}
