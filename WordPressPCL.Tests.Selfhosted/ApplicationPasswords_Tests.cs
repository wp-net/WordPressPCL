using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted
{
    [TestClass]
    public class ApplicationPasswords_Tests
    {
        private static WordPressClient _client;
        private static WordPressClient _clientAuth;

        [ClassInitialize]
        public static async Task Init(TestContext testContext)
        {
            _client = ClientHelper.GetWordPressClient();
            _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
        }

        [TestMethod]
        public async Task Application_Passwords_Create()
        {
            var password = await _clientAuth.Users.CreateApplicationPassword("TestApp1");
            Debug.WriteLine(password.AppId);

            Assert.IsNotNull(password.Password);
        }

        [TestMethod]
        public async Task Read()
        {
            await _clientAuth.Users.CreateApplicationPassword("TestApp1");
            var passwords = await _clientAuth.Users.GetApplicationPasswords();

            Assert.IsNotNull(passwords);
            Assert.AreNotEqual(0, passwords.Count);
        }

        [TestMethod]
        public async Task Application_Password_Auth()
        {
            var appPassword = await _clientAuth.Users.CreateApplicationPassword("TestApp1");
            var appPasswordClient = new WordPressClient(ApiCredentials.WordPressUri)
            {
                AuthMethod = AuthMethod.ApplicationPassword,
                UserName = ApiCredentials.Username
            };
            appPasswordClient.SetApplicationPassword(appPassword.Password);

            var post = new Post()
            {
                Title = new Title("Title 1"),
                Content = new Content("Content PostCreate")
            };
            var postCreated = await appPasswordClient.Posts.Create(post);
            Assert.IsNotNull(postCreated);
            Assert.AreEqual("Title 1", postCreated.Title.Raw);
        }
    }
}
