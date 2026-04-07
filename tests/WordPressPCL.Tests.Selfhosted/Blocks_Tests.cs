using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Blocks_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Blocks_GetAll()
    {
        List<Block> blocks = await _clientAuth.Blocks.GetAllAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(blocks);
    }

    [TestMethod]
    public async Task Blocks_CreateUpdateDelete()
    {
        // Create
        Block block = new Block
        {
            Title = new Title("Test Reusable Block"),
            Content = new Content("<!-- wp:paragraph --><p>Hello World</p><!-- /wp:paragraph -->"),
            Status = Status.Publish,
        };
        Block created = await _clientAuth.Blocks.CreateAsync(block, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(created);
        Assert.AreNotEqual(0, created.Id);

        // Update
        created.Title = new Title("Updated Reusable Block");
        Block updated = await _clientAuth.Blocks.UpdateAsync(created, cancellationToken: TestContext.CancellationToken);
        Assert.AreEqual("Updated Reusable Block", updated.Title!.Raw);

        // Delete
        bool deleted = await _clientAuth.Blocks.DeleteAsync(created.Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsTrue(deleted);
    }

    public TestContext TestContext { get; set; } = null!;
}
