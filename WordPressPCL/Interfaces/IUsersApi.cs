using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Users REST API
    /// </summary>
    internal interface IUsersApi
    {
        /// <summary>
        /// Get users with query
        /// </summary>
        [Get("/wp/v2/users{query}")]
        Task<HttpResponseMessage> GetUsersAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get user by ID
        /// </summary>
        [Get("/wp/v2/users/{id}")]
        Task<HttpResponseMessage> GetUserByIdAsync(int id, [Header("Authorization")] string authorization);

        /// <summary>
        /// Create user
        /// </summary>
        [Post("/wp/v2/users")]
        Task<HttpResponseMessage> CreateUserAsync([Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Update user
        /// </summary>
        [Post("/wp/v2/users/{id}")]
        Task<HttpResponseMessage> UpdateUserAsync(int id, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Delete user
        /// </summary>
        [Delete("/wp/v2/users/{id}{query}")]
        Task<HttpResponseMessage> DeleteUserAsync(int id, string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get current user
        /// </summary>
        [Get("/wp/v2/users/me")]
        Task<HttpResponseMessage> GetCurrentUserAsync([Header("Authorization")] string authorization);
    }
}
