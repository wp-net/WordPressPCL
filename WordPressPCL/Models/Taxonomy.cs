using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Type represents Taxonomy Entity of WP REST API
    /// </summary>
    public class Taxonomy
    {
        /// <summary>
        /// All capabilities used by the taxonomy.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("capabilities")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IDictionary<string, bool>? Capabilities { get; set; }

        /// <summary>
        /// A human-readable description of the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Whether or not the taxonomy should have children.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("hierarchical")]
        public bool Hierarchical { get; set; }

        /// <summary>
        /// Human-readable labels for the taxonomy for various contexts.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("labels")]
        public List<string>? Labels { get; set; }

        /// <summary>
        /// The title for the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// An alphanumeric identifier for the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        /// <summary>
        /// Whether or not the term cloud should be displayed.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("show_cloud")]
        public bool ShowCloud { get; set; }

        /// <summary>
        /// Types associated with the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("types")]
        public List<string>? Types { get; set; }

        /// <summary>
        /// REST base route for the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("rest_base")]
        public string? RestBase { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonPropertyName("_links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Links? Links { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Taxonomy()
        {
        }
    }
}
