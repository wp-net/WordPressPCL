using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Model for unsuccessful request
    /// </summary>
    public class BadRequest
    {
        /// <summary>
        /// Error type
        /// </summary>
        [JsonProperty("code")]
        public string Name { get; set; }

        /// <summary>
        /// Error description
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Additional info
        /// </summary>
        [JsonProperty("data")]
        public dynamic Data { get; set; }
    }
}