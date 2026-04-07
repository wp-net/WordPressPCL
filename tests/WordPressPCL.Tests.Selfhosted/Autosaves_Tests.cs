using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Autosaves_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task PostAutosaves_Get()
    {
        // Create a post to have an autosave target
        Post post = new()
        {
            Title = new Title("Autosave Test Post"),
            Content = new Content("Initial content"),
            Status = Status.Draft,
        };
        Post created = await _clientAuth.Posts.CreateAsync(post, cancellationToken: TestContext.CancellationToken);

        // Retrieve autosaves (may be empty for a fresh post)
        List<PostRevision> autosaves = await _clientAuth.Posts.Autosaves(created.Id).GetAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(autosaves);

        // Cleanup
        await _clientAuth.Posts.DeleteAsync(created.Id, force: true, cancellationToken: TestContext.CancellationToken);
    }

    [TestMethod]
    public async Task PageAutosaves_Get()
    {
        // Create a page to have an autosave target
        Page page = new()
        {
            Title = new Title("Autosave Test Page"),
            Content = new Content("Initial content"),
            Status = Status.Draft,
        };
        Page created = await _clientAuth.Pages.CreateAsync(page, cancellationToken: TestContext.CancellationToken);

        // Retrieve autosaves (may be empty for a fresh page)
        List<PostRevision> autosaves = await _clientAuth.Pages.Autosaves(created.Id).GetAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(autosaves);

        // Cleanup
        await _clientAuth.Pages.DeleteAsync(created.Id, force: true, cancellationToken: TestContext.CancellationToken);
    }

    public TestContext TestContext { get; set; } = null!;
}
