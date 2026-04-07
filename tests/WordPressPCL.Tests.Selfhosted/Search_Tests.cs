using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Search_Tests
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
    public async Task Search_ByTerm()
    {
        List<SearchResult> results = await _client.Search.SearchAsync("hello", cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(results);
    }

    [TestMethod]
    public async Task Search_QueryBuilder()
    {
        SearchQueryBuilder qb = new SearchQueryBuilder
        {
            Search = "hello",
            Type = "post",
        };
        List<SearchResult> results = await _client.Search.QueryAsync(qb, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(results);
    }

    public TestContext TestContext { get; set; } = null!;
}
