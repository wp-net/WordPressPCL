
using System;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Type represents Page Entity of WP REST API
    /// </summary>
    public class Page : Base
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
        public DateTime DateGmt { get; set; }

        /// <summary>
        /// The globally unique identifier for the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonPropertyName("guid")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid? Guid { get; set; }

        /// <summary>
        /// The date the object was last modified, in the site’s timezone.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonPropertyName("modified")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime Modified { get; set; }

        /// <summary>
        /// The date the object was last modified, as GMT.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
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
        /// </summary>
        /// <remarks>
        /// Context: edit
        /// One of: publish, future, draft, pending, private, trash
        /// </remarks>
        [JsonPropertyName("status")]
        public Status Status { get; set; } 

        /// <summary>
        /// Type of Post for the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit, embed
        /// </remarks>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// URL to the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit, embed
        /// </remarks>
        [JsonPropertyName("link")]
        public string? Link { get; set; }

        /// <summary>
        /// The title for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("title")]
        public Title? Title { get; set; }

        /// <summary>
        /// The content for the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("content")]
        public Content? Content { get; set; }

        /// <summary>
        /// The excerpt for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("excerpt")]
        public Excerpt? Excerpt { get; set; }

        /// <summary>
        /// The id for the author of the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("author")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Author { get; set; }

        /// <summary>
        /// The id of the featured media for the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("featured_media")]
        public int FeaturedMedia { get; set; }

        /// <summary>
        /// The id for the parent of the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("parent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Parent { get; set; }

        /// <summary>
        /// The order of the object in relation to other object of its type.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("menu_order")]
        public int MenuOrder { get; set; }

        /// <summary>
        /// Whether or not comments are open on the object.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: open, closed
        /// </remarks>
        [JsonPropertyName("comment_status")]
        public OpenStatus? CommentStatus { get; set; }

        /// <summary>
        /// Whether or not the object can be pinged.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: open, closed
        /// </remarks>
        [JsonPropertyName("ping_status")]
        public OpenStatus? PingStatus { get; set; }

        /// <summary>
        /// The theme file to use to display the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("template")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Template { get; set; }

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
        /// Embedded information like featured images
        /// </summary>
        [JsonPropertyName("_embedded")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Embedded? Embedded { get; set; }

        /// <summary>
        /// A password to protect access to the content and excerpt.
        /// </summary>
        /// <remarks>Context: edit</remarks>

        [JsonPropertyName("password")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Password { get; set; }

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public Page()
        {
        }
    }
}