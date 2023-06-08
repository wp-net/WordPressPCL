using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
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
        /// <returns></returns>
        public async Task<Plugin> InstallAsync(Plugin Plugin)
        {

            using var postBody = new StringContent(JsonConvert.SerializeObject(new { slug = Plugin.Id }), Encoding.UTF8, "application/json");
            var (plugin, _) = await _httpHelper.PostRequestAsync<Models.Plugin>("plugins", postBody).ConfigureAwait(false);
            return plugin;

        }

        /// <summary>
        /// Installs a plugin based on the Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Plugin> InstallAsync(string Id)
        {

            using var postBody = new StringContent(JsonConvert.SerializeObject(new { slug=Id }), Encoding.UTF8, "application/json");
            var (plugin, _) = await _httpHelper.PostRequestAsync<Models.Plugin>("plugins", postBody).ConfigureAwait(false);
            return plugin;
        }

        /// <summary>
        /// Activates an existing plugin
        /// </summary>
        /// <param name="Plugin"></param>
        /// <returns></returns>
        public async Task<Plugin> ActivateAsync(Plugin Plugin)
        {
            using var postBody = new StringContent(JsonConvert.SerializeObject(new { status = "active" }), Encoding.UTF8, "application/json");
            var (plugin, _) = await _httpHelper.PostRequestAsync<Models.Plugin>($"plugins/{Plugin.PluginFile}", postBody).ConfigureAwait(false);
            return plugin;
        }

        /// <summary>
        /// Deactivates an existing plugin
        /// </summary>
        /// <param name="Plugin"></param>
        /// <returns></returns>
        public async Task<Plugin> DeactivateAsync(Plugin Plugin)
        {

            using var postBody = new StringContent(JsonConvert.SerializeObject(new { status = "inactive" }), Encoding.UTF8, "application/json");
            var (plugin, _) = await _httpHelper.PostRequestAsync<Models.Plugin>($"plugins/{Plugin.PluginFile}", postBody).ConfigureAwait(false);
            return plugin;
        }

        /// <summary>
        /// Get plugins by search term
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="embed">include embed info</param>
        /// <returns>List of posts</returns>
        public Task<IEnumerable<Plugin>> GetPluginsBySearchAsync(string searchTerm, bool embed = false)
        {
            // default values
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return HttpHelper.GetRequestAsync<IEnumerable<Plugin>>(_methodPath.SetQueryParam("search", searchTerm), embed,true);
        }

        /// <summary>
        /// Get plugins by search term
        /// </summary>
        /// <param name="activationStatus">active or inactive</param>
        /// <param name="embed">include embed info</param>
        /// <returns>List of posts</returns>
        public Task<IEnumerable<Plugin>> GetPluginsByActivationStatusAsync(ActivationStatus activationStatus, bool embed = false)
        {
            // default values
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return HttpHelper.GetRequestAsync<IEnumerable<Plugin>>(_methodPath.SetQueryParam("status", activationStatus.ToString()), embed, true);
        }


        /// <summary>
        /// Deletes an existing plugin
        /// </summary>
        /// <param name="Plugin"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Plugin Plugin)
        {
#pragma warning disable CA1507 // Use nameof to express symbol names
            return HttpHelper.DeleteRequestAsync($"{_methodPath}/{Plugin.PluginFile}");
#pragma warning restore CA1507 // Use nameof to express symbol names
        }

        #endregion Custom
    }
}