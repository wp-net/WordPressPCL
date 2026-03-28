using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WordPressPCL.Utility;
using WordPressPCL.Tests.Selfhosted.Utility;
using WordPressPCL.Models;
using System.Collections.Generic;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Taxonomies_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Taxonomies_Read()
    {
        List<Taxonomy> taxonomies = await _clientAuth.Taxonomies.GetAllAsync();
        Assert.IsNotNull(taxonomies);
        Assert.AreNotEqual(0, taxonomies.Count);
    }

    [TestMethod]
    public async Task Taxonomies_Get()
    {
        List<Taxonomy> taxonomies = await _clientAuth.Taxonomies.GetAsync();
        Assert.IsNotNull(taxonomies);
        Assert.AreNotEqual(0, taxonomies.Count);
    }

    [TestMethod]
    public async Task Taxonomies_Query()
    {
        TaxonomiesQueryBuilder queryBuilder = new()
        {
            Type = "post"
        };
        List<Taxonomy> queryresult = await _clientAuth.Taxonomies.QueryAsync(queryBuilder);
        Assert.AreEqual("?type=post&order=desc&context=view", queryBuilder.BuildQuery());
        Assert.IsNotNull(queryresult);
        Assert.AreNotEqual(0, queryresult.Count);
    }
}
