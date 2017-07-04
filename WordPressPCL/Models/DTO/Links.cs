using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    public class Links
    {
        [JsonProperty("self")]
        public IEnumerable<Self> Self { get; set; }

        [JsonProperty("collection")]
        public IEnumerable<Collection> Collection { get; set; }

        [JsonProperty("about")]
        public IEnumerable<About> About { get; set; }

        [JsonProperty("wp:post_type")]
        public IEnumerable<WpPostType> WpPostType { get; set; }

        [JsonProperty("curies")]
        public IEnumerable<Cury> Curies { get; set; }
    }
}
