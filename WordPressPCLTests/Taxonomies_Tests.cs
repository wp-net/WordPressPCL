using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL.Utility;
using WordPressPCLTests.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class Taxonomies_Tests
    {
        [TestMethod]
        public async Task Taxonomies_Read()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var taxonomies = await client.Taxonomies.GetAll();
            Assert.IsNotNull(taxonomies);
            Assert.AreNotEqual(taxonomies.Count(), 0);
        }

        [TestMethod]
        public async Task Taxonomies_Get()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var taxonomies = await client.Taxonomies.Get();
            Assert.IsNotNull(taxonomies);
            Assert.AreNotEqual(taxonomies.Count(), 0);
        }

        [TestMethod]
        public async Task Taxonomies_Query()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var queryBuilder = new TaxonomiesQueryBuilder()
            {
                Type = "post"
            };
            var queryresult = await client.Taxonomies.Query(queryBuilder);
            Assert.AreEqual(queryBuilder.BuildQueryURL(), "?type=post");
            Assert.IsNotNull(queryresult);
            Assert.AreNotSame(queryresult.Count(), 0);
        }
    }
}