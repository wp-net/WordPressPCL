using Newtonsoft.Json;
using System.ComponentModel;

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
        [JsonProperty("success")]
        public bool Success { get; set; }
         
        /// <summary>
        /// The response message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// The JWT Content
        /// </summary>
        [JsonProperty("data")]
        [DefaultValue(null)]
        public JWTData Data { get; set; }
    }
}
