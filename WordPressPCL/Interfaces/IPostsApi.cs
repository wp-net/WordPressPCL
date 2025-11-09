using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Posts REST API
    /// </summary>
    internal interface IPostsApi
    {
        /// <summary>
        /// Get posts
        /// </summary>
        [Get("/wp/v2/posts")]
        Task<HttpResponseMessage> GetPostsAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Get posts with query
        /// </summary>
        [Get("/wp/v2/posts{query}")]
        Task<HttpResponseMessage> GetPostsAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get post by ID
        /// </summary>
        [Get("/wp/v2/posts/{id}")]
        Task<HttpResponseMessage> GetPostByIdAsync(int id, [Header("Authorization")] string authorization);

        /// <summary>
        /// Create post
        /// </summary>
        [Post("/wp/v2/posts")]
        Task<HttpResponseMessage> CreatePostAsync([Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Update post
        /// </summary>
        [Post("/wp/v2/posts/{id}")]
        Task<HttpResponseMessage> UpdatePostAsync(int id, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Delete post
        /// </summary>
        [Delete("/wp/v2/posts/{id}")]
        Task<HttpResponseMessage> DeletePostAsync(int id, [Header("Authorization")] string authorization);

        /// <summary>
        /// Delete post with query parameters
        /// </summary>
        [Delete("/wp/v2/posts/{id}{query}")]
        Task<HttpResponseMessage> DeletePostAsync(int id, string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get post count (HEAD request)
        /// </summary>
        [Head("/wp/v2/posts")]
        Task<HttpResponseMessage> GetPostCountAsync([Header("Authorization")] string authorization);
    }
}
