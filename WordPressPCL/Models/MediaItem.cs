
using System;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Type represent Media Entity of WP REST API
    /// </summary>
    public class MediaItem : Base
    {
        /// <summary>
        /// The date the object was published, in the site’s timezone.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("date")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime Date { get; set; }

        /// <summary>
        /// The date the object was published, as GMT.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("date_gmt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime DateGmt { get; set; }

        /// <summary>
        /// The globally unique identifier for the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("guid")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid? Guid { get; set; }

        /// <summary>
        /// The date the object was last modified, in the site’s timezone.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("modified")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime Modified { get; set; }

        /// <summary>
        /// The date the object was last modified, as GMT.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("modified_gmt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime ModifiedGmt { get; set; }

        /// <summary>
        /// An alphanumeric identifier for the object unique to its type.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        /// <summary>
        /// A named status for the object.
        /// <see cref="Models.Status"/>
        /// </summary>
        /// <remarks>
        /// Context: edit
        /// One of: publish, future, draft, pending, private</remarks>
        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public MediaQueryStatus Status { get; set; }

        /// <summary>
        /// Type of Post for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// URL to the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("link")]
        public string? Link { get; set; }

        /// <summary>
        /// The title for the object.
        /// <see cref="Models.Title"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("title")]
        public Title? Title { get; set; }

        /// <summary>
        /// The id for the author of the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("author")]
        public int Author { get; set; }

        /// <summary>
        /// Whether or not comments are open on the object.
        /// <see cref="OpenStatus"/>
        /// </summary>
        /// <remarks>Context: view, edit
        /// One of: open, closed</remarks>
        [JsonPropertyName("comment_status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public OpenStatus? CommentStatus { get; set; }

        /// <summary>
        /// Whether or not the object can be pinged.
        /// <see cref="OpenStatus"/>
        /// </summary>
        /// <remarks>Context: view, edit
        /// One of: open, closed</remarks>
        [JsonPropertyName("ping_status")]
        public OpenStatus? PingStatus { get; set; }

        /// <summary>
        /// Alternative text to display when resource is not displayed.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("alt_text")]
        public string? AltText { get; set; }

        /// <summary>
        /// The caption for the resource.
        /// <see cref="Models.Caption"/>
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("caption")]
        public Caption? Caption { get; set; }

        /// <summary>
        /// The description for the resource.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("description")]
        public Description? Description { get; set; }

        /// <summary>
        /// Type of resource.
        /// <see cref="Models.MediaType"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed
        /// One of: image, file</remarks>
        [JsonPropertyName("media_type")]
        public MediaType MediaType { get; set; }

        /// <summary>
        /// MIME type of resource.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("mime_type")]
        public string? MimeType { get; set; }

        /// <summary>
        /// Details about the resource file, specific to its type.
        /// <see cref="Models.MediaDetails"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("media_details")]
        public MediaDetails? MediaDetails { get; set; }

        /// <summary>
        /// The id for the associated post of the resource.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("post")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Post { get; set; }

        /// <summary>
        /// URL to the original resource file.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("source_url")]
        public string? SourceUrl { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("meta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public dynamic? Meta { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonPropertyName("_links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Links? Links { get; set; }

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public MediaItem()
        {
        }
    }
}