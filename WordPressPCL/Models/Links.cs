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
    }
}
