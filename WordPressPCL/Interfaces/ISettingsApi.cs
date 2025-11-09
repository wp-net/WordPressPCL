using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Settings REST API
    /// </summary>
    internal interface ISettingsApi
    {
        /// <summary>
        /// Get settings
        /// </summary>
        [Get("/wp/v2/settings")]
        Task<HttpResponseMessage> GetSettingsAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Update settings
        /// </summary>
        [Post("/wp/v2/settings")]
        Task<HttpResponseMessage> UpdateSettingsAsync([Body] HttpContent content, [Header("Authorization")] string authorization);
    }
}
