using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Post Types REST API
    /// </summary>
    internal interface IPostTypesApi
    {
        /// <summary>
        /// Get post types with query
        /// </summary>
        [Get("/wp/v2/types{query}")]
        Task<HttpResponseMessage> GetPostTypesAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get post type
        /// </summary>
        [Get("/wp/v2/types/{type}")]
        Task<HttpResponseMessage> GetPostTypeAsync(string type, [Header("Authorization")] string authorization);
    }
}
