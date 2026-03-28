using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Utility;

namespace WordPressPCL.Client {
    /// <summary>
    /// Client class for interaction with Settings endpoints of WP REST API
    /// </summary>
    public class Settings {

        private readonly HttpHelper _httpHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public Settings(HttpHelper httpHelper) {
            _httpHelper = httpHelper;
        }

        /// <summary>
        /// Get site settings
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Site settings</returns>
        public Task<Models.Settings> GetSettingsAsync(CancellationToken cancellationToken = default)
        {
            return _httpHelper.GetRequestAsync<Models.Settings>("settings", false, true, cancellationToken: cancellationToken);
        }
        
        /// <summary>
        /// Update site settings
        /// </summary>
        /// <param name="settings">Settings object</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated settings</returns>
        public async Task<Models.Settings> UpdateSettingsAsync(Models.Settings settings, CancellationToken cancellationToken = default)
        {
            using var postBody = new StringContent(JsonSerializer.Serialize(settings, _httpHelper.JsonSerializerOptions), Encoding.UTF8, "application/json");
            var (setting, _) = await _httpHelper.PostRequestAsync<Models.Settings>("settings", postBody, cancellationToken: cancellationToken).ConfigureAwait(false);
            return setting;
        }
    }
}