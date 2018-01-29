using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Type represent Comment Entity of WP REST API
    /// </summary>
	public class Comment : Base
    {
        /// <summary>
        /// The id of the associated post object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("post")]
        public int PostId { get; set; }

        /// <summary>
        /// The id for the parent of the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("parent", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ParentId { get; set; }

        /// <summary>
        /// The id of the user object, if author was a user.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("author", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AuthorId { get; set; }

        /// <summary>
        /// Display name for the object author.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
		[JsonProperty("author_name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AuthorName { get; set; }

        /// <summary>
        /// Email address for the object author.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("author_email", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AuthorEmail { get; set; }

        /// <summary>
        /// URL for the object author.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("author_url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AuthorUrl { get; set; }

        /// <summary>
        /// IP address for the object author.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("author_ip", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AuthorIP { get; set; }

        /// <summary>
        /// Avatar URLs for the object author.
        /// <see cref="WordPressPCL.Models.AvatarURL"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("author_avatar_urls", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AvatarURL AuthorAvatarUrls { get; set; }

        /// <summary>
        /// User agent for the object author.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("author_user_agent", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AuthorUserAgent { get; set; }

        /// <summary>
        /// The date the object was published.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Date { get; set; }

        /// <summary>
        /// The date the object was published as GMT.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
		[JsonProperty("date_gmt", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime DateGmt { get; set; }

        /// <summary>
        /// The content for the object.
        /// <see cref="WordPressPCL.Models.Content"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("content")]
        public Content Content { get; set; }

        /// <summary>
        /// URL to the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// State of the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("status")]
		public CommentStatus Status { get; set; }

        /// <summary>
        /// Type of Comment for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("type")]
		public string Type { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<object> Meta { get; set; }

        /// <summary>
        /// Karma for the object.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("karma")]
        public int Karma { get; set; }

        /// <summary>
        /// Links to another entities
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public Comment()
        {
        }

        /// <summary>
        /// Constructor with required parameters
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <param name="content">Comment content</param>
        public Comment(int postId, string content) : this()
        {
            PostId = postId;
            Content = new Content(content);
        }
    }
}