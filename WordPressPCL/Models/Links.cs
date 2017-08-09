using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
        public IEnumerable<Self> Self { get; set; }
        /// <summary>
        /// Collection of links
        /// <see cref="WordPressPCL.Models.Collection"/>
        /// </summary>
        [JsonProperty("collection")]
        public IEnumerable<Collection> Collection { get; set; }
        /// <summary>
        /// About info
        /// <see cref="WordPressPCL.Models.About"/>
        /// </summary>
        [JsonProperty("about")]
        public IEnumerable<About> About { get; set; }
        /// <summary>
        /// WordPress post Type
        /// </summary>
        [JsonProperty("wp:post_type")]
        public IEnumerable<WpPostType> WpPostType { get; set; }
        /// <summary>
        /// Curries
        /// </summary>
        [JsonProperty("curies")]
        public IEnumerable<Cury> Curies { get; set; }
        /// <summary>
        /// Author
        /// </summary>
        [JsonProperty("author")]
        public IEnumerable<Author> Author { get; set; }
        /// <summary>
        /// Replies
        /// </summary>
        [JsonProperty("replies")]
        public IEnumerable<Reply> Replies { get; set; }
        /// <summary>
        /// Versions
        /// </summary>
        [JsonProperty("version-history")]
        public IEnumerable<VersionHistory> Versions { get; set; }
        /// <summary>
        /// Attachment
        /// </summary>
        [JsonProperty("wp:attachment")]
        public IEnumerable<HttpsApiWOrgAttachment> Attachment { get; set; }
        /// <summary>
        /// Featured media
        /// </summary>
        [JsonProperty("wp:featuredmedia")]
        public IEnumerable<HttpsApiWOrgFeaturedmedia> FeaturedMedia { get; set; }
        /// <summary>
        /// Featured media
        /// </summary>
        [JsonProperty("wp:term")]
        public IEnumerable<HttpsApiWOrgTerm> Term { get; set; }
    }
}
