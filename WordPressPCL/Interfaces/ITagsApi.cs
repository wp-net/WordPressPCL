using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Tags REST API
    /// </summary>
    internal interface ITagsApi
    {
        /// <summary>
        /// Get tags with query
        /// </summary>
        [Get("/wp/v2/tags{query}")]
        Task<HttpResponseMessage> GetTagsAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get tag by ID
        /// </summary>
        [Get("/wp/v2/tags/{id}")]
        Task<HttpResponseMessage> GetTagByIdAsync(int id, [Header("Authorization")] string authorization);

        /// <summary>
        /// Create tag
        /// </summary>
        [Post("/wp/v2/tags")]
        Task<HttpResponseMessage> CreateTagAsync([Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Update tag
        /// </summary>
        [Post("/wp/v2/tags/{id}")]
        Task<HttpResponseMessage> UpdateTagAsync(int id, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Delete tag
        /// </summary>
        [Delete("/wp/v2/tags/{id}{query}")]
        Task<HttpResponseMessage> DeleteTagAsync(int id, string query, [Header("Authorization")] string authorization);
    }
}
