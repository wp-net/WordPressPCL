using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
	public class Post
	{
        /// <summary>
        ///     The date the object was published, in the site’s timezone.
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
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("guid")]
        public Guid Guid { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("modified")]
		public string Modified { get; set; }

		[JsonProperty("modified_gmt")]
		public string ModifiedGmt { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("slug")]
		public string Slug { get; set; }

        // One of: publish, future, draft, pending, private
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
		public string Type { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("link")]
		public string Link { get; set; }

		[JsonProperty("content")]
		public Content Content { get; set; }

		[JsonProperty("excerpt")]
		public Excerpt Excerpt { get; set; }

		[JsonProperty("author")]
		public int Author { get; set; }

		[JsonProperty("featured_media")]
		public int FeaturedMedia { get; set; }

        // One of: open, closed
        [JsonProperty("comment_status")]
		public string CommentStatus { get; set; }

		[JsonProperty("ping_status")]
		public string PingStatus { get; set; }

		[JsonProperty("sticky")]
		public bool Sticky { get; set; }

        /// <summary>
        ///     The format for the object.
        /// </summary>
        /// <remarks>
        ///     Context: view, edit
        ///     One of: standard
        /// </remarks>
        [JsonProperty("format")]
		public string Format { get; set; }

        [JsonProperty("categories")]
        public int[] Categories { get; set; }

        [JsonProperty("tags")]
        public int[] Tags { get; set; }

        
        [JsonProperty("template")]
        public string Template { get; set; }
        
        
        [JsonProperty("_links")]
        public Links Links { get; set; }

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

    public class Guid
	{
		[JsonProperty("rendered")]
		public string Rendered { get; set; }
        [JsonProperty("raw")]
        public string Raw { get; set; }
    }

	public class Title
	{
		[JsonProperty("rendered")]
		public string Rendered { get; set; }
        [JsonProperty("raw")] 
        public string Raw { get; set; }

    }

    public class Excerpt
	{
		[JsonProperty("rendered")]
		public string Rendered { get; set; }
        [JsonProperty("raw")]
        public string Raw { get; set; }
    }

	public class VersionHistory
	{
		[JsonProperty("href")]
		public string Href { get; set; }
	}

	public class HttpsApiWOrgFeaturedmedia
	{
		[JsonProperty("embeddable")]
		public bool Embeddable { get; set; }

		[JsonProperty("href")]
		public string Href { get; set; }
	}
}