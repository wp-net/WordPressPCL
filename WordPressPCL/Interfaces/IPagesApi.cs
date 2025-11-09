using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Pages REST API
    /// </summary>
    internal interface IPagesApi
    {
        /// <summary>
        /// Get pages
        /// </summary>
        [Get("/wp/v2/pages")]
        Task<HttpResponseMessage> GetPagesAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Get pages with query
        /// </summary>
        [Get("/wp/v2/pages{query}")]
        Task<HttpResponseMessage> GetPagesAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get page by ID
        /// </summary>
        [Get("/wp/v2/pages/{id}")]
        Task<HttpResponseMessage> GetPageByIdAsync(int id, [Header("Authorization")] string authorization);

        /// <summary>
        /// Create page
        /// </summary>
        [Post("/wp/v2/pages")]
        Task<HttpResponseMessage> CreatePageAsync([Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Update page
        /// </summary>
        [Post("/wp/v2/pages/{id}")]
        Task<HttpResponseMessage> UpdatePageAsync(int id, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Delete page
        /// </summary>
        [Delete("/wp/v2/pages/{id}{query}")]
        Task<HttpResponseMessage> DeletePageAsync(int id, string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get page count (HEAD request)
        /// </summary>
        [Head("/wp/v2/pages")]
        Task<HttpResponseMessage> GetPageCountAsync([Header("Authorization")] string authorization);
    }
}
