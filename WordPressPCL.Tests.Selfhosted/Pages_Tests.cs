using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;
using System.Linq;
using WordPressPCL.Utility;
using WordPressPCL.Models.Exceptions;
using System.Collections.Generic;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Pages_Tests
{
    private static WordPressClient _client = null!;
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _client = ClientHelper.GetWordPressClient();
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Pages_Create()
    {
        Page page = new()
        {
            Title = new Title("Title 1"),
            Content = new Content("Content PostCreate")
        };
        Page createdPage = await _clientAuth.Pages.CreateAsync(page, TestContext.CancellationToken);

        Assert.AreEqual(page.Content!.Raw, createdPage.Content!.Raw);
        Assert.Contains(page.Content.Rendered, createdPage.Content.Rendered);
    }

    [TestMethod]
    public async Task Pages_Read()
    {
        List<Page> pages = await _client.Pages.QueryAsync(new PagesQueryBuilder(), cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(pages);
        Assert.AreNotEqual(0, pages.Count);
    }

    [TestMethod]
    public async Task Pages_Get()
    {
        List<Page> pages = await _client.Pages.GetAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(pages);
        Assert.AreNotEqual(0, pages.Count);
    }

    [TestMethod]
    public async Task Pages_Update()
    {
        string testContent = $"Test {System.Guid.NewGuid()}";
        List<Page> pages = await _client.Pages.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotEmpty(pages);

        Page page = pages.First();
        page.Content!.Raw = testContent;
        Page updatedPage = await _clientAuth.Pages.UpdateAsync(page, TestContext.CancellationToken);
        Assert.AreEqual(testContent, updatedPage.Content!.Raw);
    }


    [TestMethod]
    public async Task Pages_Delete()
    {
        Page page = new()
        {
            Title = new Title("Title 1"),
            Content = new Content("Content PostCreate")
        };
        Page createdPage = await _clientAuth.Pages.CreateAsync(page, TestContext.CancellationToken);
        Assert.IsNotNull(createdPage);

        bool response = await _clientAuth.Pages.DeleteAsync(createdPage.Id);
        Assert.IsTrue(response);
        await Assert.ThrowsExactlyAsync<WPException>(async () =>
        {
            Page pageById = await _client.Pages.GetByIdAsync(createdPage.Id, cancellationToken: TestContext.CancellationToken);
        });
    }

    [TestMethod]
    public async Task Pages_Query()
    {
        PagesQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 15,
            OrderBy = PagesOrderBy.Title,
            Order = Order.ASC,
            Statuses = new List<Status> { Status.Publish },
            Embed = true
        };
        List<Page> queryresult = await _client.Pages.QueryAsync(queryBuilder, cancellationToken: TestContext.CancellationToken);
        Assert.AreEqual("?page=1&per_page=15&orderby=title&status=publish&order=asc&_embed=true&context=view", queryBuilder.BuildQuery());
        Assert.IsNotNull(queryresult);
        Assert.AreNotEqual(0, queryresult.Count);
    }

    public TestContext TestContext { get; set; }
}
