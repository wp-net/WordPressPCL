using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    public class Page
    {
        /// <summary>
        /// Unique identifier for the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit, embed
        /// </remarks>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The date the object was published, in the site’s timezone.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("date")]
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
        [JsonProperty("guid")]
        public Guid Guid { get; set; }

        /// <summary>
        /// The date the object was last modified, in the site’s timezone.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        /// <summary>
        /// The date the object was last modified, as GMT.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonProperty("modified_gmt")]
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
        public string Status { get; set; }

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
        [JsonProperty("author")]
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
        [JsonProperty("parent")]
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
        public Status CommentStatus { get; set; }

        /// <summary>
        /// Whether or not the object can be pinged.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: open, closed
        /// </remarks>
        [JsonProperty("ping_status")]
        public Status PingStatus { get; set; }

        /// <summary>
        /// The theme file to use to display the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("template")]
        public string Template { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta")]
        public IList<object> Meta { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonProperty("_links")]
        public Links Links { get; set; }

        /// <summary>
        /// Embedded information like featured images
        /// </summary>
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

}
