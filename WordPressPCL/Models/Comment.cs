using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        [JsonProperty("author_email")]
        public string AuthorEmail { get; set; }

        [JsonProperty("author_url")]
		public string AuthorUrl { get; set; }

        [JsonProperty("author_ip")]
        public string AuthorIP { get; set; }

        [JsonProperty("author_avatar_urls")]
		public AvatarURL AuthorAvatarUrls { get; set; }

        [JsonProperty("author_user_agent")]
        public string AuthorUserAgent { get; set; }

        public DateTime Date { get; set; }

		[JsonProperty("date_gmt")]
		public DateTime DateGmt { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("parent")]
        public int Parent { get; set; }

        public string Link { get; set; }

		public string Status { get; set; }

		public string Type { get; set; }

        [JsonProperty("meta")]
        public IEnumerable<object> Meta { get; set; }

        public int Karma { get; set; }
		//public Links _links { get; set; }
	}

	
}