using System.Collections.Generic;
using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Links helper class
    /// </summary>
    public class Links
    {
        /// <summary>
        /// Self link
        /// </summary>
        [JsonProperty("self")]
        public List<Self> Self { get; set; }

        /// <summary>
        /// Collection of links
        /// <see cref="Models.Collection"/>
        /// </summary>
        [JsonProperty("collection")]
        public List<Collection> Collection { get; set; }

        /// <summary>
        /// About info
        /// <see cref="Models.About"/>
        /// </summary>
        [JsonProperty("about")]
        public List<About> About { get; set; }

        /// <summary>
        /// WordPress post Type
        /// </summary>
        [JsonProperty("wp:post_type")]
        public List<WpPostType> WpPostType { get; set; }

        /// <summary>
        /// Curries
        /// </summary>
        [JsonProperty("curies")]
        public List<Cury> Curies { get; set; }

        /// <summary>
        /// Author
        /// </summary>
        [JsonProperty("author")]
        public List<Author> Author { get; set; }

        /// <summary>
        /// Replies
        /// </summary>
        [JsonProperty("replies")]
        public List<Reply> Replies { get; set; }

        /// <summary>
        /// Versions
        /// </summary>
        [JsonProperty("version-history")]
        public List<VersionHistory> Versions { get; set; }

        /// <summary>
        /// Attachment
        /// </summary>
        [JsonProperty("wp:attachment")]
        public List<HttpsApiWOrgAttachment> Attachment { get; set; }

        /// <summary>
        /// Featured media
        /// </summary>
        [JsonProperty("wp:featuredmedia")]
        public List<HttpsApiWOrgFeaturedmedia> FeaturedMedia { get; set; }

        /// <summary>
        /// Featured media
        /// </summary>
        [JsonProperty("wp:term")]
        public List<HttpsApiWOrgTerm> Term { get; set; }
    }
}
