using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class PostTypes_Tests
{
    private static WordPressClient _client;
    private static WordPressClient _clientAuth;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _client = ClientHelper.GetWordPressClient();
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task PostTypes_Read()
    {
        List<PostType> posttypes = await _clientAuth.PostTypes.GetAllAsync();
        Assert.IsNotNull(posttypes);
        Assert.AreNotEqual(posttypes.Count, 0);
    }

    [TestMethod]
    public async Task PostTypes_Get()
    {
        List<PostType> posttypes = await _clientAuth.PostTypes.GetAsync();
        Assert.IsNotNull(posttypes);
        Assert.AreNotEqual(posttypes.Count, 0);
    }
}
