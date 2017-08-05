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
    public class Categories_Tests
    {
        [TestMethod]
        public async Task Categories_Create()
        {
            Random random = new Random();
            var name = $"TestCategory {random.Next(0, 10000)}";
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var category = await client.Categories.Create(new Category()
            {
                Name = name,
                Description = "Test"
            });
            Assert.IsNotNull(category);
            Assert.AreEqual(name, category.Name);
            Assert.AreEqual("Test", category.Description);
        }
        [TestMethod]
        public async Task Categories_Read()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            var categories = await client.Categories.GetAll();
            Assert.IsNotNull(categories);
            Assert.AreNotEqual(categories.Count(), 0);
            CollectionAssert.AllItemsAreUnique(categories.Select(tag => tag.Id).ToList());
        }
        [TestMethod]
        public async Task Categories_Update()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var categories = await client.Categories.GetAll();
            var category = categories.First();
            Random random = new Random();
            var name = $"UpdatedCategory {random.Next(0, 10000)}";
            category.Name = name;
            var updatedCategory = await client.Categories.Update(category);
            Assert.AreEqual(updatedCategory.Name, name);
            Assert.AreEqual(updatedCategory.Id, category.Id);
        }
        [TestMethod]
        public async Task Categories_Delete()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public async Task Categories_Query()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var queryBuilder = new CategoriesQueryBuilder()
            {
                Page = 1,
                PerPage = 15,
                OrderBy = TermsOrderBy.Id,
                Order = Order.DESC,
            };
            var queryresult = await client.Categories.Query(queryBuilder);
            Assert.AreEqual(queryBuilder.BuildQueryURL(), "?page=1&per_page=15&orderby=id&order=desc");
            Assert.IsNotNull(queryresult);
            Assert.AreNotSame(queryresult.Count(), 0);
        }

    }

}