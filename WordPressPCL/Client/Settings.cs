using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        /// <returns>Site settings</returns>
        public Task<WordPressPCL.Models.Settings> GetSettings()
        {
            return _httpHelper.GetRequestAsync<WordPressPCL.Models.Settings>("settings", false, true);
        }
        
        /// <summary>
        /// Update site settings
        /// </summary>
        /// <param name="settings">Settings object</param>
        /// <returns>Updated settings</returns>
        public async Task<WordPressPCL.Models.Settings> UpdateSettingsAsync(WordPressPCL.Models.Settings settings)
        {
            using var postBody = new StringContent(JsonConvert.SerializeObject(settings), Encoding.UTF8, "application/json");
            var (setting, _) = await _httpHelper.PostRequestAsync<WordPressPCL.Models.Settings>("settings", postBody).ConfigureAwait(false);
            return setting;
        }
    }
}