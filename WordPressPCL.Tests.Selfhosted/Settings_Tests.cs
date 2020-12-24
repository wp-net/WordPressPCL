using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Tests.Selfhosted.Utility;
using System.Linq;
using WordPressPCL;

namespace WordPressPCL.Tests.Selfhosted
{
    [TestClass]
    public class Settings_Tests
    {
        private static WordPressClient _clientAuth;

        [ClassInitialize]
        public static async Task Init(TestContext testContext)
        {
            _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
        }

        [TestMethod]
        public async Task Get_Settings_Test()
        {
            var settings = await _clientAuth.GetSettings();
            Assert.IsNotNull(settings);
            Assert.IsNotNull(settings.Title);
        }
    }
}
