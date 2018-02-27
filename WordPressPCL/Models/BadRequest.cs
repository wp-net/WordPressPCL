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
        public BadRequestData Data { get; set; }
    }

    /// <summary>
    /// Helper class
    /// </summary>
    public class BadRequestData
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
    }
}