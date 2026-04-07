using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class PageRevisions_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task PageRevisions_Read()
    {
        int id = await CreatePageWithRevision();
        Client.PageRevisions revisionsClient = _clientAuth.Pages.Revisions(id);
        List<PostRevision> revisions = await revisionsClient.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.AreNotEqual(0, revisions.Count);
    }

    [TestMethod]
    public async Task PageRevisions_GetById()
    {
        int id = await CreatePageWithRevision();
        Client.PageRevisions revisionsClient = _clientAuth.Pages.Revisions(id);
        List<PostRevision> revisions = await revisionsClient.GetAsync(cancellationToken: TestContext.CancellationToken);
        Assert.AreNotEqual(0, revisions.Count);
        PostRevision revision = await revisionsClient.GetByIdAsync(revisions.First().Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(revision);
        Assert.AreEqual(revisions.First().Id, revision.Id);
    }

    private static async Task<int> CreatePageWithRevision()
    {
        Page page = new()
        {
            Title = new Title("Revision Test Page"),
            Content = new Content("Original content"),
        };
        Page created = await _clientAuth.Pages.CreateAsync(page);
        created.Content!.Raw = "Updated content";
        Page updated = await _clientAuth.Pages.UpdateAsync(created);
        return updated.Id;
    }

    public TestContext TestContext { get; set; } = null!;
}
