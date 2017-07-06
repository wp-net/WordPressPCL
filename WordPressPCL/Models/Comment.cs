using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Type represent Comment Entity of WP REST API
    /// </summary>
	public class Comment
	{
        /// <summary>
        /// Unique identifier for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// The id of the associated post object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("post")]
        public int PostId { get; set; }

        [JsonProperty("parent")]
        public int ParentId { get; set; }
        /// <summary>
        /// The id of the user object, if author was a user.

        /// <see cref="WordPressPCL.Models.User.Id"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("author")]
        public int AuthorId { get; set; }
        /// <summary>
        /// Display name for the object author.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
		[JsonProperty("author_name")]
		public string AuthorName { get; set; }
        /// <summary>
        /// Email address for the object author.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("author_email")]
        public string AuthorEmail { get; set; }
        /// <summary>
        /// URL for the object author.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("author_url")]
		public string AuthorUrl { get; set; }
        /// <summary>
        /// IP address for the object author.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("author_ip")]
        public string AuthorIP { get; set; }
        /// <summary>
        /// Avatar URLs for the object author.
        /// <see cref="WordPressPCL.Models.AvatarURL"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("author_avatar_urls")]
		public AvatarURL AuthorAvatarUrls { get; set; }
        /// <summary>
        /// User agent for the object author.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("author_user_agent")]
        public string AuthorUserAgent { get; set; }
        /// <summary>
        /// The date the object was published.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        public DateTime Date { get; set; }
        /// <summary>
        /// The date the object was published as GMT.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
		[JsonProperty("date_gmt")]
		public DateTime DateGmt { get; set; }
        /// <summary>
        /// The content for the object.
        /// <see cref="WordPressPCL.Models.Content"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("content")]
        public Content Content { get; set; }
        /// <summary>
        /// The id for the parent of the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("parent")]
        public int Parent { get; set; }
        /// <summary>
        /// URL to the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        public string Link { get; set; }
        /// <summary>
        /// State of the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
		public string Status { get; set; }
        /// <summary>
        /// Type of Comment for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
		public string Type { get; set; }
        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta")]
        public IEnumerable<object> Meta { get; set; }
        /// <summary>
        /// Karma for the object.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        public int Karma { get; set; }
		//public Links _links { get; set; }
	}

	
}