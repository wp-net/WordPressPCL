using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted
{
    [TestClass]
    public class ExceptionTests
    {
        private static WordPressClient _client;
        private static WordPressClient _clientAuth;

        [ClassInitialize]
        public static async Task Init(TestContext testContext)
        {
            _client = ClientHelper.GetWordPressClient();
            _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient();
        }

        [TestMethod]
        public async Task Exception_JWTAuthExceptionTest()
        {
            // Initialize
            Assert.IsNotNull(_client);
            // Get settings without auth
            try
            {
                var settings = await _client.GetSettings();
            }
            catch (WPException wpex)
            {
                Assert.IsNotNull(wpex.RequestData);
                Assert.AreEqual(wpex.RequestData.Name, "jwt_auth_bad_auth_header");
            }
        }

        [TestMethod]
        public async Task Exception_PostCreateExceptionTest()
        {
            // Initialize
            Assert.IsNotNull(_client);
            // Create empty post
            try
            {
                var post = await _client.Posts.Create(new Post());
            }
            catch (WPException wpex)
            {
                Assert.IsNotNull(wpex.RequestData);
                Assert.AreEqual(wpex.RequestData.Name, "jwt_auth_bad_auth_header");
            }
        }
    }
}