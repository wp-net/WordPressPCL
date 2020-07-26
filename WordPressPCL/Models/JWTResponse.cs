using Newtonsoft.Json;
using System.ComponentModel;

namespace WordPressPCL.Models
{
    class JWTResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        [DefaultValue(null)]
        public JWTData Data { get; set; }
    }
}
