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

    public class Self
    {
        public string Href { get; set; }
    }

    public class Collection
    {
        public string Href { get; set; }
    }

    public class About
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class Author
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class Reply
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }


    public class Cury
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }
    }

    public class WpPostType
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class HttpsApiWOrgAttachment
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class HttpsApiWOrgTerm
    {
        public string Taxonomy { get; set; }

        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }

    public class HttpsApiWOrgMeta
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
