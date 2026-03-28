using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class ExceptionTests
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
    public async Task Exception_JWTAuthExceptionTest()
    {
        // Initialize
        Assert.IsNotNull(_client);
        // Get settings without auth

        await Assert.ThrowsExactlyAsync<WPException>(async () =>
        {
            Settings settings = await _client.Settings.GetSettingsAsync(TestContext.CancellationToken);
        });
    }

    [TestMethod]
    public async Task Exception_RequestJWTokenForBasicAuth()
    {
        //Initialize
        Assert.IsNotNull(_client);
        const string dummyUser = "dummy";
        const string dummyPassword = "dummy";

        _client.Auth.UseBasicAuth(dummyUser, dummyPassword);

        await Assert.ThrowsExactlyAsync<WPException>(async () =>
        {
            await _client.Auth.RequestJWTokenAsync(dummyUser, dummyPassword, TestContext.CancellationToken);
        });
    }

    [TestMethod]
    public async Task Exception_IsValidJWTokenForBasicAuth()
    {
        //Initialize
        Assert.IsNotNull(_client);
        const string dummyUser = "dummy";
        const string dummyPassword = "dummy";

        _client.Auth.UseBasicAuth(dummyUser, dummyPassword);

        await Assert.ThrowsExactlyAsync<WPException>(async () =>
        {
            await _client.Auth.IsValidJWTokenAsync(TestContext.CancellationToken);
        });
    }

    [TestMethod]
    public async Task Exception_PostCreateExceptionTest()
    {
        // Initialize
        Assert.IsNotNull(_clientAuth);
        // Create empty post
        try
        {
            Post post = await _clientAuth.Posts.CreateAsync(new Post(), TestContext.CancellationToken);
        }
        catch (WPException wpex)
        {
            Assert.IsNotNull(wpex.RequestData);
            Assert.AreEqual("empty_content", wpex.RequestData.Name);
        }
    }

    [TestMethod]
    public async Task Exception_DeleteExceptionTest()
    {
        // Initialize
        Assert.IsNotNull(_clientAuth);
        // Delete nonexisted post
        try
        {
            bool result = await _clientAuth.Posts.DeleteAsync(int.MaxValue, cancellationToken: TestContext.CancellationToken);
        }
        catch (WPException wpex)
        {
            Assert.IsNotNull(wpex.RequestData);
            Assert.AreEqual("rest_post_invalid_id", wpex.RequestData.Name);
        }
    }

    public TestContext TestContext { get; set; }
}
