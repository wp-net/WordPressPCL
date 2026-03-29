using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Themes_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Themes_GetActive()
    {
        List<Theme> themes = await _clientAuth.Themes.QueryAsync(new ThemesQueryBuilder { Status = ActivationStatus.Active }, useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(themes);
        Assert.AreNotEqual(0, themes.Count);

    }
    [TestMethod]
    public async Task Themes_Get()
    {
        List<Theme> themes = await _clientAuth.Themes.GetAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(themes);
        Assert.AreNotEqual(0, themes.Count);
        CollectionAssert.AllItemsAreUnique(themes.Select(tag => tag.Stylesheet).ToList());
    }

    [TestMethod]
    public async Task Themes_GetByIdAsync()
    {
        Theme theme = await _clientAuth.Themes.GetByIdAsync("twentytwentyfour", useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(theme);
    }

    public TestContext TestContext { get; set; } = null!;
}
