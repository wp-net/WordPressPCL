using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Settings_Tests
{
    private static WordPressClient _clientAuth;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Get_Settings_Test()
    {
        var settings = await _clientAuth.Settings.GetSettingsAsync();
        Assert.IsNotNull(settings);
        Assert.IsNotNull(settings.Title);
    }
}
