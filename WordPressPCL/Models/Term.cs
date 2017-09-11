using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    /// <summary>
    /// This is the base class for all terms, like categories and tags
    /// </summary>
    public class Term : Base
    {
        /// <summary>
        /// URL of the term.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, embed, edit
        /// </remarks>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// HTML title for the term.
        /// </summary>
        /// <remarks>Context: view, embed, edit</remarks>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// An alphanumeric identifier for the term unique to its type.
        /// </summary>
        /// <remarks>Context: view, embed, edit</remarks>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Type attribution for the term.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, embed, edit
        /// One of: category, post_tag, nav_menu, link_category, post_format
        /// </remarks>
        [JsonProperty("taxonomy")]
        public TermTaxonomy Taxonomy { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

        /// <summary>
        /// parameterless constructor
        /// </summary>
        public Term()
        {
        }
    }
}