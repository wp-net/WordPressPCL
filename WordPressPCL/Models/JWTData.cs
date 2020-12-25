using Newtonsoft.Json;
using System;

namespace WordPressPCL.Models
{
    public class JWTData
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("nicename")]
        public string NiceName { get; set; }
    }
}
