
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Response class for the JWT Plugin
    /// </summary>
    public class JWTResponse
    {
        /// <summary>
        /// Indicates if the call was successful
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
         
        /// <summary>
        /// The response message
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// The JWT Content
        /// </summary>
        [JsonPropertyName("data")]
        [DefaultValue(null)]
        public JWTData? Data { get; set; }
    }
}
