using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Refit interface for WordPress Themes REST API
    /// </summary>
    internal interface IThemesApi
    {
        /// <summary>
        /// Get themes with query
        /// </summary>
        [Get("/wp/v2/themes{query}")]
        Task<HttpResponseMessage> GetThemesAsync(string query, [Header("Authorization")] string authorization);

        /// <summary>
        /// Get theme by stylesheet
        /// </summary>
        [Get("/wp/v2/themes/{stylesheet}")]
        Task<HttpResponseMessage> GetThemeAsync(string stylesheet, [Header("Authorization")] string authorization);

        /// <summary>
        /// Update theme
        /// </summary>
        [Post("/wp/v2/themes/{stylesheet}")]
        Task<HttpResponseMessage> UpdateThemeAsync(string stylesheet, [Body] HttpContent content, [Header("Authorization")] string authorization);
    }
}
