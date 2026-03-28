using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;
using WordPressPCL.Tests.Selfhosted.Utility;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Plugins_Tests
{
    private static WordPressClient _clientAuth = null!;
    private static string PluginId = "wordpress-seo";

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Plugins_Install_Activate_Deactivate_Delete()
    {
        Plugin plugin;
        try
        {
            plugin = await _clientAuth.Plugins.InstallAsync(PluginId, TestContext.CancellationToken);
        }
        catch (WPException ex) when (ex.Message.Contains("WordPress.org", StringComparison.OrdinalIgnoreCase))
        {
            Assert.Inconclusive("WordPress.org is not reachable from the test environment, so plugin installation cannot be verified.");
            return;
        }

        Assert.IsNotNull(plugin);
        Assert.AreEqual(PluginId, plugin.Id);


        //Activate plugin
        Plugin activePlugin = await _clientAuth.Plugins.ActivateAsync(plugin, TestContext.CancellationToken);
        Assert.AreEqual(ActivationStatus.Active, activePlugin.Status);
        Assert.AreEqual(plugin.Id, activePlugin.Id);


        //Deactivate plugin
        Plugin deactivatedPlugin = await _clientAuth.Plugins.DeactivateAsync(plugin, TestContext.CancellationToken);
        Assert.AreEqual(ActivationStatus.Inactive, deactivatedPlugin.Status);
        Assert.AreEqual(plugin.Id, deactivatedPlugin.Id);

        //Delete plugin
        bool response = await _clientAuth.Plugins.DeleteAsync(plugin, cancellationToken: TestContext.CancellationToken);
        Assert.IsTrue(response);
        List<Plugin> plugins = await _clientAuth.Plugins.GetAllAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        List<Plugin> c = plugins.Where(x => x.Id == plugin.Id).ToList();
        Assert.IsEmpty(c);
    }

    [TestMethod]
    public async Task Plugins_GetActive()
    {
        //Active plugin
        List<Plugin> plugins = await _clientAuth.Plugins.QueryAsync(new PluginsQueryBuilder { Status = ActivationStatus.Active }, useAuth: true, TestContext.CancellationToken);
        Assert.IsNotNull(plugins);
        Assert.AreNotEqual(0, plugins.Count);

    }
    [TestMethod]
    public async Task Plugins_Search()
    {
        //Active plugin
        List<Plugin> plugins = await _clientAuth.Plugins.QueryAsync(new PluginsQueryBuilder { Search = "jwt" }, useAuth: true, TestContext.CancellationToken);
        Assert.IsNotNull(plugins);
        Assert.AreNotEqual(0, plugins.Count);

    }

    [TestMethod]
    public async Task Plugins_Get()
    {
        List<Plugin> plugins = await _clientAuth.Plugins.GetAsync(useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(plugins);
        Assert.AreNotEqual(0, plugins.Count);
        CollectionAssert.AllItemsAreUnique(plugins.Select(tag => tag.Id).ToList());
    }

    [TestMethod]
    public async Task Plugins_GetByIdAsync()
    {
        Plugin plugin = await _clientAuth.Plugins.GetByIdAsync("jwt-auth/jwt-auth", useAuth: true, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(plugin);
    }

    public TestContext TestContext { get; set; }
}
