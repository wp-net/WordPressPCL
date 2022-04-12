using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class ExceptionTests
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
    public async Task Exception_JWTAuthExceptionTest()
    {
        // Initialize
        Assert.IsNotNull(_client);
        // Get settings without auth

        await Assert.ThrowsExceptionAsync<WPException>(async () =>
        {
            var settings = await _client.Settings.GetSettingsAsync();
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
        
        await Assert.ThrowsExceptionAsync<WPException>(async () => 
        {
            await _client.Auth.RequestJWTokenAsync(dummyUser, dummyPassword);
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

        await Assert.ThrowsExceptionAsync<WPException>(async () => {
            await _client.Auth.IsValidJWTokenAsync();
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
            var post = await _clientAuth.Posts.CreateAsync(new Post());
        }
        catch (WPException wpex)
        {
            Assert.IsNotNull(wpex.RequestData);
            Assert.AreEqual(wpex.RequestData.Name, "empty_content");
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
            var result = await _clientAuth.Posts.DeleteAsync(int.MaxValue);
        }
        catch (WPException wpex)
        {
            Assert.IsNotNull(wpex.RequestData);
            Assert.AreEqual(wpex.RequestData.Name, "rest_post_invalid_id");
        }
    }
}
