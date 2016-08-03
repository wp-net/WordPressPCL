using Newtonsoft.Json;
using System;

namespace WordPressPCL.Models
{
	public class Post
	{

		[JsonProperty("date")]
		public DateTime Date { get; set; }

		[JsonProperty("date_gmt")]
		public DateTime DateGmt { get; set; }

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

        // One of: standard, aside, chat, gallery, link, image, quote, status, video, audio
        [JsonProperty("format")]
		public string Format { get; set; }

        [JsonProperty("categories")]
        public int[] Categories { get; set; }

        [JsonProperty("tags")]
        public int[] Tags { get; set; }

        public enum OrderBy
		{
			date, id, include, title, slug
		}
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

	public class About
	{
		[JsonProperty("href")]
		public string Href { get; set; }
	}

	public class Author
	{
		[JsonProperty("embeddable")]
		public bool Embeddable { get; set; }

		[JsonProperty("href")]
		public string Href { get; set; }
	}

	public class Reply
	{
		[JsonProperty("embeddable")]
		public bool Embeddable { get; set; }

		[JsonProperty("href")]
		public string Href { get; set; }
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

	public class HttpsApiWOrgAttachment
	{
		[JsonProperty("href")]
		public string Href { get; set; }
	}

	public class HttpsApiWOrgTerm
	{
		public string Taxonomy { get; set; }

		[JsonProperty("embeddable")]
		public bool Embeddable { get; set; }

		[JsonProperty("href")]
		public string Href { get; set; }
	}

	public class HttpsApiWOrgMeta
	{
		[JsonProperty("embeddable")]
		public bool Embeddable { get; set; }

		[JsonProperty("href")]
		public string Href { get; set; }
	}
}