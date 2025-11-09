using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for generic/custom WordPress REST API endpoints
    /// </summary>
    internal interface ICustomRequestApi
    {
        /// <summary>
        /// Generic GET request with dynamic path
        /// </summary>
        [Get("/{**route}")]
        Task<HttpResponseMessage> GetAsync([AliasAs("route")] string route, [Header("Authorization")] string authorization);

        /// <summary>
        /// Generic POST request with dynamic path
        /// </summary>
        [Post("/{**route}")]
        Task<HttpResponseMessage> PostAsync([AliasAs("route")] string route, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// Generic DELETE request with dynamic path
        /// </summary>
        [Delete("/{**route}")]
        Task<HttpResponseMessage> DeleteAsync([AliasAs("route")] string route, [Header("Authorization")] string authorization);

        /// <summary>
        /// Generic HEAD request with dynamic path
        /// </summary>
        [Head("/{**route}")]
        Task<HttpResponseMessage> HeadAsync([AliasAs("route")] string route, [Header("Authorization")] string authorization);
    }
}
