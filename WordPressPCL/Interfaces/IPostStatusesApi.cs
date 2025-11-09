using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Post Statuses REST API
    /// </summary>
    internal interface IPostStatusesApi
    {
        /// <summary>
        /// Get post statuses
        /// </summary>
        [Get("/wp/v2/statuses")]
        Task<HttpResponseMessage> GetPostStatusesAsync([Header("Authorization")] string authorization);

        /// <summary>
        /// Get post status
        /// </summary>
        [Get("/wp/v2/statuses/{status}")]
        Task<HttpResponseMessage> GetPostStatusAsync(string status, [Header("Authorization")] string authorization);
    }
}
