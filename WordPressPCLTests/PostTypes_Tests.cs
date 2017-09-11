using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCLTests.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class PostTypes_Tests
    {
        [TestMethod]
        public async Task PostTypes_Read()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var posttypes = await client.PostTypes.GetAll();
            Assert.IsNotNull(posttypes);
            Assert.AreNotEqual(posttypes.Count(), 0);
        }

        [TestMethod]
        public async Task PostTypes_Get()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var posttypes = await client.PostTypes.Get();
            Assert.IsNotNull(posttypes);
            Assert.AreNotEqual(posttypes.Count(), 0);
        }
    }
}