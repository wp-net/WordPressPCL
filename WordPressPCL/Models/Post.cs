using Newtonsoft.Json;
using System;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Type represents Entity Post of WP REST API
    /// </summary>
	public class Post : Base
    {
        /// <summary>
        /// The date the object was published, in the site's timezone.
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
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonProperty("guid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid Guid { get; set; }

        /// <summary>
        /// The date the object was last modified, in the site's timezone.
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
        /// A password to protect access to the content and excerpt.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("password")]
        public string Password { get; set; }

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
        /// One of: publish, future, draft, pending, private, trash
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
        /// The title for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("title")]
        public Title Title { get; set; }

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
        /// The ID for the author of the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("author", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Author { get; set; }

        /// <summary>
        /// The ID of the featured media for the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("featured_media", NullValueHandling = NullValueHandling.Ignore)]
        public int? FeaturedMedia { get; set; }

        /// <summary>
        /// Whether or not comments are open on the object.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: open, closed
        /// </remarks>
        [JsonProperty("comment_status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public OpenStatus? CommentStatus { get; set; }

        /// <summary>
        /// Whether or not the object can be pinged.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: open, closed
        /// </remarks>
        [JsonProperty("ping_status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public OpenStatus? PingStatus { get; set; }

        /// <summary>
        /// Whether or not the object should be treated as sticky.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("sticky")]
        public bool Sticky { get; set; }

        /// <summary>
        /// The format for the object.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: standard
        /// </remarks>
        [JsonProperty("format")]
        public string Format { get; set; }

        /// <summary>
        /// The terms assigned to the object in the category taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("categories", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int[] Categories { get; set; }

        /// <summary>
        /// The terms assigned to the object in the post_tag taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("tags", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int[] Tags { get; set; }

        /// <summary>
        /// The theme file to use to display the object.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of:
        /// </remarks>
        [JsonProperty("template", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Template { get; set; }

        /// <summary>
        /// The number of Liveblog Likes the post has.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("liveblog_likes")]
        public int LiveblogLikes { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public dynamic Meta { get; set; }

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
        public Post()
        {
        }
    }
}