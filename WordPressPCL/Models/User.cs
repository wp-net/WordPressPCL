using Newtonsoft.Json;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public AvatarUrls avatar_urls { get; set; }
        public string slug { get; set; }
        public Links _links { get; set; }
    }

    public class AvatarUrls
    {
        [JsonProperty("24")]
        public string size24 { get; set; }
        [JsonProperty("48")]
        public string size48 { get; set; }
        [JsonProperty("96")]
        public string size96 { get; set; }
    }

}
