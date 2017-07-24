using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCLTests.Utility;
using WordPressPCL;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using System.Net;
using System.Linq;
using Newtonsoft.Json;

namespace WordPressPCLTests
{
    [TestClass]
    public class Tag_Tests
    {
        [TestMethod]
        public async Task Tags_Create()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var tag = await client.Tags.Create(new Tag()
            {
                Name = "Test",
                Description = "Test"
            });
            Assert.IsNotNull(tag);
            Assert.AreEqual("Test", tag.Name);
        }
        [TestMethod]
        public async Task Tags_Read()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var tags = await client.Tags.GetAll();
            Assert.IsNotNull(tags);
            Assert.AreNotEqual(tags.Count(), 0);
            CollectionAssert.AllItemsAreUnique(tags.Select(tag => tag.Id).ToList());
        }
        [TestMethod]
        public async Task Tags_Update()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public async Task Tags_Delete()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public async Task Tags_Query()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var queryBuilder = new TagsQueryBuilder()
            {
                Page = 1,
                PerPage = 15,
                OrderBy = TermsOrderBy.Id,
                Order = Order.DESC,
            };
            var queryresult = await client.Tags.Query(queryBuilder);
            Assert.AreEqual(queryBuilder.BuildQueryURL(), "?page=1&per_page=15&orderby=id&order=desc");
            Assert.IsNotNull(queryresult);
            Assert.AreNotSame(queryresult.Count(), 0);
        }

    }

}