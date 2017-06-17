using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCLTests.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class Settings_Tests
    {
        [TestMethod]
        public async Task Get_Settings_Test()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var settings = await client.GetSettings();
            Assert.IsNotNull(settings);
            Assert.IsNotNull(settings.Title);
        }
    }
}
