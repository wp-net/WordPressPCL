using Newtonsoft.Json;
using System;

namespace WordPressPCL.Models
{
    public class ApplicationPassword
    {

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("last_used")]
        public object LastUsed { get; set; }

        [JsonProperty("last_ip")]
        public object LastIp { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }
}
