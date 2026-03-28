using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with Plugins endpoint WP REST API
/// Date: 26 May 2023
/// Creator: Gregory Liénard
/// </summary>
public class Plugins : CRUDOperation<Plugin, PluginsQueryBuilder>
{
    #region Init

    private const string _methodPath = "plugins";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Plugins(HttpHelper HttpHelper) : base(HttpHelper, _methodPath)
    {
    }

    #endregion Init

    #region Custom

    /// <summary>
    /// Installs a plugin based on the Plugin
    /// </summary>
    /// <param name="Plugin"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<Plugin> InstallAsync(Plugin Plugin, CancellationToken cancellationToken = default)
    {

        using StringContent postBody = new StringContent(JsonSerializer.Serialize(new { slug = Plugin.Id }), Encoding.UTF8, "application/json");
        (Plugin plugin, HttpResponseMessage _) = await HttpHelper.PostRequestAsync<Plugin>("plugins", postBody, cancellationToken: cancellationToken).ConfigureAwait(false);
        return plugin;

    }

    /// <summary>
    /// Installs a plugin based on the Id
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<Plugin> InstallAsync(string Id, CancellationToken cancellationToken = default)
    {

        using StringContent postBody = new StringContent(JsonSerializer.Serialize(new { slug = Id }), Encoding.UTF8, "application/json");
        (Plugin plugin, HttpResponseMessage _) = await HttpHelper.PostRequestAsync<Plugin>("plugins", postBody, cancellationToken: cancellationToken).ConfigureAwait(false);
        return plugin;
    }

    /// <summary>
    /// Activates an existing plugin
    /// </summary>
    /// <param name="Plugin"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<Plugin> ActivateAsync(Plugin Plugin, CancellationToken cancellationToken = default)
    {
        using StringContent postBody = new StringContent(JsonSerializer.Serialize(new { status = "active" }), Encoding.UTF8, "application/json");
        (Plugin plugin, HttpResponseMessage _) = await HttpHelper.PostRequestAsync<Plugin>($"plugins/{Plugin.PluginFile}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false);
        return plugin;
    }

    /// <summary>
    /// Deactivates an existing plugin
    /// </summary>
    /// <param name="Plugin"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<Plugin> DeactivateAsync(Plugin Plugin, CancellationToken cancellationToken = default)
    {

        using StringContent postBody = new(JsonSerializer.Serialize(new { status = "inactive" }), Encoding.UTF8, "application/json");
        (Plugin plugin, HttpResponseMessage _) = await HttpHelper.PostRequestAsync<Plugin>($"plugins/{Plugin.PluginFile}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false);
        return plugin;
    }

    /// <summary>
    /// Get plugins by search term
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <param name="embed">include embed info</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of posts</returns>
    public Task<List<Plugin>> GetPluginsBySearchAsync(string searchTerm, bool embed = false, CancellationToken cancellationToken = default)
    {
        // default values
        // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
        return HttpHelper.GetRequestAsync<List<Plugin>>(_methodPath.SetQueryParam("search", searchTerm)!, embed, true, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get plugins by activation status
    /// </summary>
    /// <param name="activationStatus">active or inactive</param>
    /// <param name="embed">include embed info</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of posts</returns>
    public Task<List<Plugin>> GetPluginsByActivationStatusAsync(ActivationStatus activationStatus, bool embed = false, CancellationToken cancellationToken = default)
    {
        // default values
        // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
        return HttpHelper.GetRequestAsync<List<Plugin>>(_methodPath.SetQueryParam("status", activationStatus.ToString().ToLower())!, embed, true, cancellationToken: cancellationToken);
    }


    /// <summary>
    /// Deletes an existing plugin
    /// </summary>
    /// <param name="Plugin"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public Task<bool> DeleteAsync(Plugin Plugin, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA1507 // Use nameof to express symbol names
        return HttpHelper.DeleteRequestAsync($"{_methodPath}/{Plugin.PluginFile}", cancellationToken: cancellationToken);
#pragma warning restore CA1507 // Use nameof to express symbol names
    }

    #endregion Custom
}
