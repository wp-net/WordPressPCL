using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading.Tasks;
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
    }
}
