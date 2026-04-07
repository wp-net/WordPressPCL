using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Templates_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Templates_Get()
    {
        List<Template> templates = await _clientAuth.Templates.GetAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(templates);
        Assert.AreNotEqual(0, templates.Count);
    }

    [TestMethod]
    public async Task TemplateParts_Get()
    {
        List<TemplatePart> parts = await _clientAuth.TemplateParts.GetAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(parts);
        Assert.AreNotEqual(0, parts.Count);
    }

    public TestContext TestContext { get; set; } = null!;
}
