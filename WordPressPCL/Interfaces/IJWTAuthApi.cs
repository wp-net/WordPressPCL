using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for JWT Authentication endpoints
    /// </summary>
    internal interface IJWTAuthApi
    {
        /// <summary>
        /// Request JWT token
        /// </summary>
        [Post("/jwt-auth/v1/token")]
        Task<HttpResponseMessage> RequestTokenAsync([Body] HttpContent content);

        /// <summary>
        /// Validate JWT token
        /// </summary>
        [Post("/jwt-auth/v1/token/validate")]
        Task<HttpResponseMessage> ValidateTokenAsync([Header("Authorization")] string authorization);
    }
}
