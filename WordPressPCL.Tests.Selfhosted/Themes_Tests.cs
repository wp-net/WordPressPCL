using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Themes_Tests
{
    private static WordPressClient _clientAuth;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Themes_GetActive()
    {
        List<Theme> themes = await _clientAuth.Themes.QueryAsync(new ThemesQueryBuilder { Status = ActivationStatus.Active }, useAuth:true);
        Assert.IsNotNull(themes);
        Assert.AreNotEqual(themes.Count, 0);

    }
    [TestMethod]
    public async Task Themes_Get()
    {
        List<Theme> themes = await _clientAuth.Themes.GetAsync (useAuth: true);
        Assert.IsNotNull(themes);
        Assert.AreNotEqual(themes.Count, 0);
        CollectionAssert.AllItemsAreUnique(themes.Select(tag => tag.Stylesheet).ToList());
    }

    [TestMethod]
    public async Task Themes_GetByID()
    {
        Theme theme = await _clientAuth.Themes.GetByIDAsync("twentytwentythree", useAuth: true);
        Assert.IsNotNull(theme);
    }

}
