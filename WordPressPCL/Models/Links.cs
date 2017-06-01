using Newtonsoft.Json;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
    public class Links
    {
        [JsonProperty("self")]
        public IList<Self> Self { get; set; }

        [JsonProperty("collection")]
        public IList<Collection> Collection { get; set; }

        [JsonProperty("about")]
        public IList<About> About { get; set; }

        [JsonProperty("wp:post_type")]
        public IList<WpPostType> WpPostType { get; set; }

        [JsonProperty("curies")]
        public IList<Cury> Curies { get; set; }
    }

    public class Self : HrefBase { }

    public class Collection : HrefBase {}

    public class About : HrefBase { }

    public class Author : HrefBase
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

    public class Reply : HrefBase
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }


    public class Cury : HrefBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }
    }

    public class WpPostType : HrefBase { }

    public class HttpsApiWOrgAttachment : HrefBase { }

    public class HttpsApiWOrgTerm : HrefBase
    {
        public string Taxonomy { get; set; }

        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

    public class HttpsApiWOrgMeta : HrefBase
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }
}
