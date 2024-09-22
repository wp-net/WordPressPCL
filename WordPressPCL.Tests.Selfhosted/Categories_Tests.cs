using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Categories_Tests
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
    public async Task Categories_Create()
    {
        Random random = new();
        string name = $"TestCategory {random.Next(0, 10000)}";
        Category category = await _clientAuth.Categories.CreateAsync(new Category()
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
        List<Category> categories = await _client.Categories.GetAllAsync();
        Assert.IsNotNull(categories);
        Assert.AreNotEqual(categories.Count, 0);
        CollectionAssert.AllItemsAreUnique(categories.Select(tag => tag.Id).ToList());
    }

    [TestMethod]
    public async Task Categories_Get()
    {
        List<Category> categories = await _client.Categories.GetAsync();
        Assert.IsNotNull(categories);
        Assert.AreNotEqual(categories.Count, 0);
        CollectionAssert.AllItemsAreUnique(categories.Select(tag => tag.Id).ToList());
    }

    [TestMethod]
    public async Task Categories_Update()
    {
        List<Category> categories = await _clientAuth.Categories.GetAllAsync();
        Category category = categories.First();
        Random random = new();
        string name = $"UpdatedCategory {random.Next(0, 10000)}";
        category.Name = name;
        Category updatedCategory = await _clientAuth.Categories.UpdateAsync(category);
        Assert.AreEqual(updatedCategory.Name, name);
        Assert.AreEqual(updatedCategory.Id, category.Id);
    }

    [TestMethod]
    public async Task Categories_Delete()
    {
        Random random = new();
        string name = $"TestCategory {random.Next(0, 10000)}";
        Category category = await _clientAuth.Categories.CreateAsync(new Category()
        {
            Name = name,
            Description = "Test"
        });

        if (category == null)
        {
            Assert.Inconclusive();
        }
        bool response = await _clientAuth.Categories.DeleteAsync(category.Id);
        Assert.IsTrue(response);
        List<Category> categories = await _clientAuth.Categories.GetAllAsync();
        List<Category> c = categories.Where(x => x.Id == category.Id).ToList();
        Assert.AreEqual(c.Count, 0);
    }

    [TestMethod]
    public async Task Categories_Query()
    {
        CategoriesQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 15,
            OrderBy = TermsOrderBy.Id,
            Order = Order.DESC,
        };
        List<Category> queryresult = await _clientAuth.Categories.QueryAsync(queryBuilder);
        Assert.AreEqual("?page=1&per_page=15&orderby=id&order=desc&context=view", queryBuilder.BuildQuery());
        Assert.IsNotNull(queryresult);
        Assert.AreNotSame(queryresult.Count, 0);
    }
}
