using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class WidgetTypes_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task WidgetTypes_Get()
    {
        List<WidgetType> types = await _clientAuth.WidgetTypes.GetAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(types);
        Assert.AreNotEqual(0, types.Count);
    }

    [TestMethod]
    public async Task WidgetTypes_GetById()
    {
        WidgetType widgetType = await _clientAuth.WidgetTypes.GetByIdAsync("block", useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(widgetType);
        Assert.AreEqual("block", widgetType.Id);
    }

    public TestContext TestContext { get; set; } = null!;
}
