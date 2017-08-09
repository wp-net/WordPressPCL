using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Embedded Information
    /// </summary>
    public class Embedded
    {
        /// <summary>
        /// Post Author
        /// </summary>
        [JsonProperty("author")]
        public IEnumerable<User> Author { get; set; }

        /// <summary>
        /// Comments on the post
        /// </summary>
        [JsonProperty("replies")]
        public IEnumerable<IList<Comment>> Replies { get; set; }

        /// <summary>
        /// Featured images for the post
        /// </summary>
        [JsonProperty("wp:featuredmedia")]
        public IEnumerable<MediaItem> WpFeaturedmedia { get; set; }

        /// <summary>
        /// Terms for the post (categories, tags etc.)
        /// </summary>
        [JsonProperty("wp:term")]
        public IEnumerable<IEnumerable<Term>> WpTerm { get; set; }
        /// <summary>
        /// Parent page
        /// </summary>
        [JsonProperty("up")]
        public IEnumerable<Page> Up { get; set; }
    }
}
