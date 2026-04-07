using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Sidebars_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Sidebars_Get()
    {
        List<Sidebar> sidebars = await _clientAuth.Sidebars.GetAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(sidebars);
    }

    public TestContext TestContext { get; set; } = null!;
}
