using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    /// <summary>
    /// JWT Data
    /// </summary>
    public class JWTData
    {
        /// <summary>
        /// JWT Token
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// User Display Name
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// User Email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// User Nice Names
        /// </summary>
        [JsonProperty("nicename")]
        public string NiceName { get; set; }
    }
}
