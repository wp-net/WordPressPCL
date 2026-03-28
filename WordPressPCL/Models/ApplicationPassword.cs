
using System;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Application Password
    /// </summary>
    public class ApplicationPassword
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        [JsonPropertyName("uuid")]
        public string? Uuid { get; set; }

        /// <summary>
        /// App ID
        /// </summary>
        [JsonPropertyName("app_id")]
        public string? AppId { get; set; }

        /// <summary>
        /// App Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Created Timestamp
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Last Used
        /// </summary>
        [JsonPropertyName("last_used")]
        public object? LastUsed { get; set; }

        /// <summary>
        /// Last IP
        /// </summary>
        [JsonPropertyName("last_ip")]
        public object? LastIp { get; set; }

        /// <summary>
        /// Application Password
        /// </summary>
        [JsonPropertyName("password")]
        public string? Password { get; set; }

        /// <summary>
        /// Links
        /// </summary>
        [JsonPropertyName("_links")]
        public Links? Links { get; set; }
    }
}
