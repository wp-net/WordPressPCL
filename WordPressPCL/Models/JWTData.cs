using Newtonsoft.Json;
using System;

namespace WordPressPCL.Models
{
    class JWTData
    {
        [JsonProperty("token")]
        public String Token { get; set; }

        [JsonProperty("displayName")]
        public String DisplayName{ get; set; }

        [JsonProperty("email")]
        public String Email { get; set; }

        [JsonProperty("nicename")]
        public String NiceName { get; set; }
    }
}
