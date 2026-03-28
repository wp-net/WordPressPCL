using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("author")]
        public List<User>? Author { get; set; }

        /// <summary>
        /// Comments on the post
        /// </summary>
        [JsonPropertyName("replies")]
        public List<IList<Comment>>? Replies { get; set; }

        /// <summary>
        /// Featured images for the post
        /// </summary>
        [JsonPropertyName("wp:featuredmedia")]
        public List<MediaItem>? WpFeaturedmedia { get; set; }

        /// <summary>
        /// Terms for the post (categories, tags etc.)
        /// </summary>
        [JsonPropertyName("wp:term")]
        public List<List<Term>>? WpTerm { get; set; }
        /// <summary>
        /// Parent page
        /// </summary>
        [JsonPropertyName("up")]
        public List<Page>? Up { get; set; }
    }
}
