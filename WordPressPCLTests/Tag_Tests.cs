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

            var random = new Random();
            var tagname = $"Test {random.Next(0, 1000)}";
            var tag = await client.Tags.Create(new Tag()
            {
                Name = tagname,
                Description = "Test Description"
            });
            Assert.IsNotNull(tag);
            Assert.AreEqual(tagname, tag.Name);
            Assert.AreEqual("Test Description", tag.Description);
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
        public async Task Tags_Get()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var tags = await client.Tags.Get();
            Assert.IsNotNull(tags);
            Assert.AreNotEqual(tags.Count(), 0);
            CollectionAssert.AllItemsAreUnique(tags.Select(tag => tag.Id).ToList());
        }

        [TestMethod]
        public async Task Tags_Update()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var tags = await client.Tags.GetAll();
            var tag = tags.FirstOrDefault();
            if(tag == null)
            {
                Assert.Inconclusive();
            }
            var random = new Random();
            var tagname = $"Testname {random.Next(0, 1000)}";
            var tagdesc = "Test Description";
            tag.Name = tagname;
            tag.Description = tagdesc;
            var tagUpdated = await client.Tags.Update(tag);
            Assert.AreEqual(tagname, tagUpdated.Name);
            Assert.AreEqual(tagdesc, tagUpdated.Description);
        }
        [TestMethod]
        public async Task Tags_Delete()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var tags = await client.Tags.GetAll();
            var tag = tags.FirstOrDefault();
            if (tag == null)
            {
                Assert.Inconclusive();
            }
            var tagId = tag.Id;
            var response = await client.Tags.Delete(tagId);
            Assert.IsTrue(response.IsSuccessStatusCode);
            tags = await client.Tags.GetAll();
            var tagsWithId = tags.Where(x => x.Id == tagId).ToList();
            Assert.AreEqual(tagsWithId.Count, 0);
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