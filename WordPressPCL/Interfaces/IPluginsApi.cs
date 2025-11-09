using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Plugins REST API
    /// </summary>
    internal interface IPluginsApi
    {
        /// <summary>
        /// Get plugins with query
        /// </summary>
        [Get("/wp/v2/plugins{query}")]
        Task<HttpResponseMessage> GetPluginsAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get plugin by slug
        /// </summary>
        [Get("/wp/v2/plugins/{plugin}")]
        Task<HttpResponseMessage> GetPluginAsync(string plugin, [Header("Authorization")] string authorization);

        /// <summary>
        /// Create/Install plugin
        /// </summary>
        [Post("/wp/v2/plugins")]
        Task<HttpResponseMessage> CreatePluginAsync([Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Update plugin
        /// </summary>
        [Post("/wp/v2/plugins/{plugin}")]
        Task<HttpResponseMessage> UpdatePluginAsync(string plugin, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Delete plugin
        /// </summary>
        [Delete("/wp/v2/plugins/{plugin}")]
        Task<HttpResponseMessage> DeletePluginAsync(string plugin, [Header("Authorization")] string authorization);
    }
}
