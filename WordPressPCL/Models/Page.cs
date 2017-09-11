using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [JsonProperty("date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Date { get; set; }

        /// <summary>
        /// The date the object was published, as GMT.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("date_gmt")]
        public DateTime DateGmt { get; set; }

        /// <summary>
        /// The globally unique identifier for the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonProperty("guid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid Guid { get; set; }

        /// <summary>
        /// The date the object was last modified, in the site’s timezone.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonProperty("modified", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Modified { get; set; }

        /// <summary>
        /// The date the object was last modified, as GMT.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonProperty("modified_gmt", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime ModifiedGmt { get; set; }

        /// <summary>
        /// An alphanumeric identifier for the object unique to its type.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// A named status for the object.
        /// </summary>
        /// <remarks>
        /// Context: edit
        /// One of: publish, future, draft, pending, private
        /// </remarks>
        [JsonProperty("status")]
        public Status Status { get; set; }

        /// <summary>
        /// Type of Post for the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit, embed
        /// </remarks>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// URL to the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit, embed
        /// </remarks>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// The title for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("title")]
        public Title Title { get; set; }

        /// <summary>
        /// The content for the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("content")]
        public Content Content { get; set; }

        /// <summary>
        /// The excerpt for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("excerpt")]
        public Excerpt Excerpt { get; set; }

        /// <summary>
        /// The id for the author of the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("author", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Author { get; set; }

        /// <summary>
        /// The id of the featured media for the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("featured_media")]
        public int FeaturedMedia { get; set; }

        /// <summary>
        /// The id for the parent of the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("parent", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public int Parent { get; set; }

        /// <summary>
        /// The order of the object in relation to other object of its type.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("menu_order")]
        public int MenuOrder { get; set; }

        /// <summary>
        /// Whether or not comments are open on the object.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: open, closed
        /// </remarks>
        [JsonProperty("comment_status")]
        public OpenStatus CommentStatus { get; set; }

        /// <summary>
        /// Whether or not the object can be pinged.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: open, closed
        /// </remarks>
        [JsonProperty("ping_status")]
        public OpenStatus PingStatus { get; set; }

        /// <summary>
        /// The theme file to use to display the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("template", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Template { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<object> Meta { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

        /// <summary>
        /// Embedded information like featured images
        /// </summary>
        [JsonProperty("_embedded", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Embedded Embedded { get; set; }

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public Page()
        {
        }
    }
}