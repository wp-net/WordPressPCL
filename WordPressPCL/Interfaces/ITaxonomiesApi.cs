using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Taxonomies REST API
    /// </summary>
    internal interface ITaxonomiesApi
    {
        /// <summary>
        /// Get taxonomies with query
        /// </summary>
        [Get("/wp/v2/taxonomies{query}")]
        Task<HttpResponseMessage> GetTaxonomiesAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get taxonomy by type
        /// </summary>
        [Get("/wp/v2/taxonomies/{type}")]
        Task<HttpResponseMessage> GetTaxonomyAsync(string type, [Header("Authorization")] string authorization);
    }
}
