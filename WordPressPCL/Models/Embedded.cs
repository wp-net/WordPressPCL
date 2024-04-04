using System.Collections.Generic;
using Newtonsoft.Json;

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
        public List<User> Author { get; set; }

        /// <summary>
        /// Comments on the post
        /// </summary>
        [JsonProperty("replies")]
        public List<IList<Comment>> Replies { get; set; }

        /// <summary>
        /// Featured images for the post
        /// </summary>
        [JsonProperty("wp:featuredmedia")]
        public List<MediaItem> WpFeaturedmedia { get; set; }

        /// <summary>
        /// Terms for the post (categories, tags etc.)
        /// </summary>
        [JsonProperty("wp:term")]
        public List<List<Term>> WpTerm { get; set; }
        /// <summary>
        /// Parent page
        /// </summary>
        [JsonProperty("up")]
        public List<Page> Up { get; set; }
    }
}
