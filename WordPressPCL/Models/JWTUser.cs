using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    class JWTUser
    {
        [JsonProperty("token")]
        public String Token { get; set; }

        [JsonProperty("user_display_name")]
        public String DisplayName{ get; set; }

        [JsonProperty("user_email")]
        public String Email { get; set; }

        [JsonProperty("user_nicename")]
        public String NiceName { get; set; }
    }
}
