using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Categories REST API
    /// </summary>
    internal interface ICategoriesApi
    {
        /// <summary>
        /// Get categories with query
        /// </summary>
        [Get("/wp/v2/categories{query}")]
        Task<HttpResponseMessage> GetCategoriesAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get category by ID
        /// </summary>
        [Get("/wp/v2/categories/{id}")]
        Task<HttpResponseMessage> GetCategoryByIdAsync(int id, [Header("Authorization")] string authorization);

        /// <summary>
        /// Create category
        /// </summary>
        [Post("/wp/v2/categories")]
        Task<HttpResponseMessage> CreateCategoryAsync([Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Update category
        /// </summary>
        [Post("/wp/v2/categories/{id}")]
        Task<HttpResponseMessage> UpdateCategoryAsync(int id, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Delete category
        /// </summary>
        [Delete("/wp/v2/categories/{id}{query}")]
        Task<HttpResponseMessage> DeleteCategoryAsync(int id, string query, [Header("Authorization")] string authorization);
    }
}
