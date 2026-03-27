using System.Text.Json.Serialization;


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
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// User Display Name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// User Email
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// User Nice Names
        /// </summary>
        [JsonPropertyName("nicename")]
        public string? NiceName { get; set; }
    }
}
