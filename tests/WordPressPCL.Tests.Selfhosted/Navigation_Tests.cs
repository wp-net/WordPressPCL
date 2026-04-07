using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Navigation_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Navigation_GetAll()
    {
        List<Navigation> navigations = await _clientAuth.Navigation.GetAllAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(navigations);
    }

    [TestMethod]
    public async Task Navigation_CreateUpdateDelete()
    {
        // Create
        Navigation nav = new Navigation
        {
            Title = new Title("Test Navigation"),
            Status = Status.Publish,
        };
        Navigation created = await _clientAuth.Navigation.CreateAsync(nav, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(created);
        Assert.AreNotEqual(0, created.Id);

        // Update
        created.Title = new Title("Updated Navigation");
        Navigation updated = await _clientAuth.Navigation.UpdateAsync(created, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(updated);

        // Delete
        bool deleted = await _clientAuth.Navigation.DeleteAsync(created.Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsTrue(deleted);
    }

    public TestContext TestContext { get; set; } = null!;
}
