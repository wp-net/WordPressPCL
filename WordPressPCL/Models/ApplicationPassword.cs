using Newtonsoft.Json;
using System;

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
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// App ID
        /// </summary>
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// App Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Created Timestamp
        /// </summary>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Last Used
        /// </summary>
        [JsonProperty("last_used")]
        public object LastUsed { get; set; }

        /// <summary>
        /// Last IP
        /// </summary>
        [JsonProperty("last_ip")]
        public object LastIp { get; set; }

        /// <summary>
        /// Application Password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Links
        /// </summary>
        [JsonProperty("_links")]
        public Links Links { get; set; }
    }
}
