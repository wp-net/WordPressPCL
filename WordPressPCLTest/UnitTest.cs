using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL;

namespace WordPressPCLTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public async void TestMethod1()
        {
            // Initialize
            var client = new WordPressClient("http://demo.wp-api.org/wp-json/wp/v2/");
            Assert.IsNotNull(client);
            // Posts
            var posts = await client.ListPosts();
            Assert.IsNotNull(posts);
        }
    }
}
