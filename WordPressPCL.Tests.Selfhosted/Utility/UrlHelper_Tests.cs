using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted.Utility
{
    [TestClass]
    public class UrlHelper_Tests
    {


        [TestMethod]
        public void SetQueryParam_Test()
        {
            string test = "test";
            var result = test.SetQueryParam("param", "value");

            Assert.AreEqual("test", test);
            Assert.AreEqual("test?param=value", result);

        }
    }
}