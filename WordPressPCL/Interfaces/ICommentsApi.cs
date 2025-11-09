using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Comments REST API
    /// </summary>
    internal interface ICommentsApi
    {
        /// <summary>
        /// Get comments
        /// </summary>
        [Get("/wp/v2/comments")]
        Task<HttpResponseMessage> GetCommentsAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Get comments with query
        /// </summary>
        [Get("/wp/v2/comments{query}")]
        Task<HttpResponseMessage> GetCommentsAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get comment by ID
        /// </summary>
        [Get("/wp/v2/comments/{id}")]
        Task<HttpResponseMessage> GetCommentByIdAsync(int id, [Header("Authorization")] string authorization);

        /// <summary>
        /// Create comment
        /// </summary>
        [Post("/wp/v2/comments")]
        Task<HttpResponseMessage> CreateCommentAsync([Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Update comment
        /// </summary>
        [Post("/wp/v2/comments/{id}")]
        Task<HttpResponseMessage> UpdateCommentAsync(int id, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Delete comment
        /// </summary>
        [Delete("/wp/v2/comments/{id}{query}")]
        Task<HttpResponseMessage> DeleteCommentAsync(int id, string query, [Header("Authorization")] string authorization);
    }
}
