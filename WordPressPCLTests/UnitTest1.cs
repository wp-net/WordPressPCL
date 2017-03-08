using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCLTests.Utility;
using WordPressPCL;
using System.Threading.Tasks;
using WordPressPCL.Models;

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
    }
}
