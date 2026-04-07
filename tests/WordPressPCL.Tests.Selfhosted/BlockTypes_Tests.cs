using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class BlockTypes_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task BlockTypes_Get()
    {
        List<BlockType> blockTypes = await _clientAuth.BlockTypes.GetAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(blockTypes);
        Assert.AreNotEqual(0, blockTypes.Count);
    }

    [TestMethod]
    public async Task BlockTypes_GetByName()
    {
        BlockType blockType = await _clientAuth.BlockTypes.GetByNameAsync("core", "paragraph", useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(blockType);
        Assert.AreEqual("core/paragraph", blockType.Name);
    }

    public TestContext TestContext { get; set; } = null!;
}
