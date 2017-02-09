using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL;
using WordPressPCLTest.Utility;
namespace WordPressPCLTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public async void TestMethod1()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var posts = await client.ListPosts();
            Assert.IsNotNull(posts);
        }


        [TestMethod]
        public async void BasicAuthTest()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            client.Username = ApiCredentials.Username;
            client.Password = ApiCredentials.Password;

            
        }
    }
}
