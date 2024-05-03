using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Client;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Plugins_Tests
{
    private static WordPressClient _clientAuth;
    private static string PluginId= "wordpress-seo";

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Plugins_Install_Activate_Deactivate_Delete()
    {
        var plugin = await _clientAuth.Plugins.InstallAsync(PluginId);
        Assert.IsNotNull(plugin);
        Assert.AreEqual(PluginId, plugin.Id);


        //Activate plugin
        var activePlugin = await _clientAuth.Plugins.ActivateAsync(plugin);
        Assert.AreEqual(activePlugin.Status, ActivationStatus.Active);
        Assert.AreEqual(activePlugin.Id, plugin.Id);


        //Deactivate plugin
        var deactivatedPlugin = await _clientAuth.Plugins.DeactivateAsync(plugin);
        Assert.AreEqual(deactivatedPlugin.Status, ActivationStatus.Inactive);
        Assert.AreEqual(deactivatedPlugin.Id, plugin.Id);

        //Delete plugin
        var response = await _clientAuth.Plugins.DeleteAsync(plugin);
        Assert.IsTrue(response);
        var plugins = await _clientAuth.Plugins.GetAllAsync(useAuth: true);
        var c = plugins.Where(x => x.Id == plugin.Id).ToList();
        Assert.AreEqual(c.Count, 0);
    }

    [TestMethod]
    public async Task Plugins_GetActive()
    {
        //Active plugin
        var plugins = await _clientAuth.Plugins.QueryAsync(new PluginsQueryBuilder { Status = ActivationStatus.Active }, useAuth:true);
        Assert.IsNotNull(plugins);
        Assert.AreNotEqual(plugins.Count, 0);

    }
    [TestMethod]
    public async Task Plugins_Search ()
    {
        //Active plugin
        var plugins = await _clientAuth.Plugins.QueryAsync(new PluginsQueryBuilder { Search="jwt" }, useAuth:true);
        Assert.IsNotNull(plugins);
        Assert.AreNotEqual(plugins.Count, 0);

    }

    [TestMethod]
    public async Task Plugins_Get()
    {
        var plugins = await _clientAuth.Plugins.GetAsync (useAuth: true);
        Assert.IsNotNull(plugins);
        Assert.AreNotEqual(plugins.Count, 0);
        CollectionAssert.AllItemsAreUnique(plugins.Select(tag => tag.Id).ToList());
    }

    [TestMethod]
    public async Task Plugins_GetByID()
    {
        var plugin = await _clientAuth.Plugins.GetByIDAsync("jwt-auth/jwt-auth", useAuth: true);
        Assert.IsNotNull(plugin);
    }

}
