using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCLTests.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class PostStatuses_Tests
    {
        [TestMethod]
        public async Task PostStatuses_Read()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var poststatuses = await client.PostStatuses.GetAll();
            Assert.IsNotNull(poststatuses);
            Assert.AreNotEqual(poststatuses.Count(), 0);
        }

        [TestMethod]
        public async Task PostStatuses_Get()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var poststatuses = await client.PostStatuses.Get();
            Assert.IsNotNull(poststatuses);
            Assert.AreNotEqual(poststatuses.Count(), 0);
        }
    }
}