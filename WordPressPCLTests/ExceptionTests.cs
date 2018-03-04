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
            Assert.IsNotNull(_clientAuth);
            // Create empty post
            try
            {
                var post = await _clientAuth.Posts.Create(new Post());
            }
            catch (WPException wpex)
            {
                Assert.IsNotNull(wpex.RequestData);
                Assert.AreEqual(wpex.RequestData.Name, "empty_content");
            }
        }

        [TestMethod]
        public async Task Exception_DeleteExceptionTest()
        {
            // Initialize
            Assert.IsNotNull(_clientAuth);
            // Delete nonexisted post
            try
            {
                var result = await _clientAuth.Posts.Delete(int.MaxValue);
            }
            catch (WPException wpex)
            {
                Assert.IsNotNull(wpex.RequestData);
                Assert.AreEqual(wpex.RequestData.Name, "rest_post_invalid_id");
            }
        }
    }
}