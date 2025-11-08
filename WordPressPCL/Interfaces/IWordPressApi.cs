using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress REST API
    /// </summary>
    internal interface IWordPressApi
    {
        /// <summary>
        /// GET request to specified route with dynamic path
        /// </summary>
        [Get("/{**route}")]
        Task<HttpResponseMessage> GetAsync([AliasAs("route")] string route, [Header("Authorization")] string authorization);

        /// <summary>
        /// POST request to specified route with dynamic path
        /// </summary>
        [Post("/{**route}")]
        Task<HttpResponseMessage> PostAsync([AliasAs("route")] string route, [Body] HttpContent content, [Header("Authorization")] string authorization);

        /// <summary>
        /// DELETE request to specified route with dynamic path
        /// </summary>
        [Delete("/{**route}")]
        Task<HttpResponseMessage> DeleteAsync([AliasAs("route")] string route, [Header("Authorization")] string authorization);

        /// <summary>
        /// HEAD request to specified route with dynamic path
        /// </summary>
        [Head("/{**route}")]
        Task<HttpResponseMessage> HeadAsync([AliasAs("route")] string route, [Header("Authorization")] string authorization);
    }
}
