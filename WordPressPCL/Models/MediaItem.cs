using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [JsonProperty("date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Date { get; set; }

        /// <summary>
        /// The date the object was published, as GMT.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("date_gmt", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime DateGmt { get; set; }

        /// <summary>
        /// The globally unique identifier for the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("guid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid Guid { get; set; }

        /// <summary>
        /// The date the object was last modified, in the site’s timezone.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("modified", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Modified { get; set; }

        /// <summary>
        /// The date the object was last modified, as GMT.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
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
        /// <see cref="WordPressPCL.Models.Status"/>
        /// </summary>
        /// <remarks>
        /// Context: edit
        /// One of: publish, future, draft, pending, private</remarks>
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MediaQueryStatus Status { get; set; }

        /// <summary>
        /// Type of Post for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// URL to the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// The title for the object.
        /// <see cref="WordPressPCL.Models.Title"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("title")]
        public Title Title { get; set; }

        /// <summary>
        /// The id for the author of the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("author")]
        public int Author { get; set; }

        /// <summary>
        /// Whether or not comments are open on the object.
        /// <see cref="WordPressPCL.Models.OpenStatus"/>
        /// </summary>
        /// <remarks>Context: view, edit
        /// One of: open, closed</remarks>
        [JsonProperty("comment_status")]
        public OpenStatus CommentStatus { get; set; }

        /// <summary>
        /// Whether or not the object can be pinged.
        /// <see cref="WordPressPCL.Models.OpenStatus"/>
        /// </summary>
        /// <remarks>Context: view, edit
        /// One of: open, closed</remarks>
        [JsonProperty("ping_status")]
        public OpenStatus PingStatus { get; set; }

        /// <summary>
        /// Alternative text to display when resource is not displayed.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("alt_text")]
        public string AltText { get; set; }

        /// <summary>
        /// The caption for the resource.
        /// <see cref="WordPressPCL.Models.Caption"/>
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("caption")]
        public Caption Caption { get; set; }

        /// <summary>
        /// The description for the resource.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("description")]
        public Description Description { get; set; }

        /// <summary>
        /// Type of resource.
        /// <see cref="WordPressPCL.Models.MediaType"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed
        /// One of: image, file</remarks>
        [JsonProperty("media_type")]
        public MediaType MediaType { get; set; }

        /// <summary>
        /// MIME type of resource.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        /// <summary>
        /// Details about the resource file, specific to its type.
        /// <see cref="WordPressPCL.Models.MediaDetails"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("media_details")]
        public MediaDetails MediaDetails { get; set; }

        /// <summary>
        /// The id for the associated post of the resource.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("post", NullValueHandling = NullValueHandling.Ignore)]
        public int Post { get; set; }

        /// <summary>
        /// URL to the original resource file.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<object> Meta { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

        /// <summary>
        /// Paremeterless constructor
        /// </summary>
        public MediaItem()
        {
        }
    }
}