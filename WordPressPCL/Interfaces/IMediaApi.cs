using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Media REST API
    /// </summary>
    internal interface IMediaApi
    {
        /// <summary>
        /// Get media items with query
        /// </summary>
        [Get("/wp/v2/media{query}")]
        Task<HttpResponseMessage> GetMediaAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get media item by ID
        /// </summary>
        [Get("/wp/v2/media/{id}")]
        Task<HttpResponseMessage> GetMediaByIdAsync(int id, [Header("Authorization")] string authorization);

        /// <summary>
        /// Create media item
        /// </summary>
        [Post("/wp/v2/media")]
        Task<HttpResponseMessage> CreateMediaAsync([Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Update media item
        /// </summary>
        [Post("/wp/v2/media/{id}")]
        Task<HttpResponseMessage> UpdateMediaAsync(int id, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Delete media item
        /// </summary>
        [Delete("/wp/v2/media/{id}{query}")]
        Task<HttpResponseMessage> DeleteMediaAsync(int id, string query, [Header("Authorization")] string authorization);
    }
}
