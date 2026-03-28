using System.Text.Json.Serialization;


namespace WordPressPCL.Models
{
    /// <summary>
    /// Type represents Post Status Entity of WP REST API
    /// </summary>
    public class PostStatus
    {
        /// <summary>
        /// The title for the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Whether posts with this resource should be private.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("private")]
        public bool Private { get; set; }

        /// <summary>
        /// Whether posts with this resource should be protected.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("protected")]
        public bool Protected { get; set; }

        /// <summary>
        /// Whether posts with this resource should be public.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("public")]
        public bool Public { get; set; }

        /// <summary>
        /// Whether posts with this resource should be publicly-queryable.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("queryable")]
        public bool Queryable { get; set; }

        /// <summary>
        /// Whether to include posts in the edit listing for their post type.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("show_in_list")]
        public bool ShowInList { get; set; }

        /// <summary>
        /// An alphanumeric identifier for the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonPropertyName("_links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Links? Links { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PostStatus()
        {
        }
    }
}