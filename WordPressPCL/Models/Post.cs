using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
	public class Post
	{
        /// <summary>
        ///     The date the object was published, in the site's timezone.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
		[JsonProperty("date")]
		public DateTime Date { get; set; }

        /// <summary>
        ///     The date the object was published, as GMT.
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
        /// Unique identifier for the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit, embed
        /// </remarks>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The date the object was last modified, in the site's timezone.
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
        [JsonProperty("author")]
		public int Author { get; set; }

        /// <summary>
        /// The ID of the featured media for the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("featured_media")]
		public int FeaturedMedia { get; set; }

        /// <summary>
        /// Whether or not comments are open on the object.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: open, closed
        /// </remarks>
        [JsonProperty("comment_status")]
		public string CommentStatus { get; set; }

        /// <summary>
        /// Whether or not the object can be pinged.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: open, closed
        /// </remarks>
        [JsonProperty("ping_status")]
		public string PingStatus { get; set; }

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
        [JsonProperty("categories")]
        public int[] Categories { get; set; }

        /// <summary>
        /// The terms assigned to the object in the post_tag taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("tags")]
        public int[] Tags { get; set; }

        /// <summary>
        /// The theme file to use to display the object.
        /// </summary>
        /// <remarks>
        /// Context: view, edit
        /// One of: 
        /// </remarks>
        [JsonProperty("template")]
        public string Template { get; set; }

        /// <summary>
        /// The number of Liveblog Likes the post has.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("liveblog_likes")]
        public int LiveblogLikes { get; set; }

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

    public class Embedded
    {
        [JsonProperty("author")]
        public IList<User> Author { get; set; }

        [JsonProperty("replies")]
        public IList<IList<Comment>> Replies { get; set; }

        [JsonProperty("wp:featuredmedia")]
        public IList<Media> WpFeaturedmedia { get; set; }

        [JsonProperty("wp:term")]
        public IList<IList<Term>> WpTerm { get; set; }
    }


    public enum OrderBy
    {
        date, id, include, title, slug
    }

    public class Guid : RenderedRawBase
	{
    }

	public class Title : RenderedRawBase
	{
    }

    public class Excerpt : RenderedRawBase
	{
        [JsonProperty("protected")]
        public bool IsProtected { get; set; }
    }

	public class VersionHistory : HrefBase
    {
	}

	public class HttpsApiWOrgFeaturedmedia : HrefBase
    {
		[JsonProperty("embeddable")]
		public bool Embeddable { get; set; }
	}
}