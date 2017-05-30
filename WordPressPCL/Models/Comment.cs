using Newtonsoft.Json;
using System;

namespace WordPressPCL.Models
{
	public class Comment
	{
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("post")]
        public int PostId { get; set; }

        [JsonProperty("parent")]
        public int ParentId { get; set; }

        [JsonProperty("author")]
        public int AuthorId { get; set; }

		[JsonProperty("author_name")]
		public string AuthorName { get; set; }

		[JsonProperty("author_url")]
		public string AuthorUrl { get; set; }

		[JsonProperty("author_avatar_urls")]
		public AuthorAvatarUrls AuthorAvatarUrls { get; set; }

		public DateTime Date { get; set; }

		[JsonProperty("date_gmt")]
		public DateTime DateGmt { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

		public string Link { get; set; }

		public string Status { get; set; }

		public string Type { get; set; }
		//public Links _links { get; set; }
	}

	public class AuthorAvatarUrls
	{
		[JsonProperty("24")]
		public string Size24 { get; set; }

		[JsonProperty("48")]
		public string Size48 { get; set; }

		[JsonProperty("96")]
		public string Size96 { get; set; }
	}
}