
using System;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("post")]
        public int PostId { get; set; }

        /// <summary>
        /// The id for the parent of the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("parent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ParentId { get; set; }

        /// <summary>
        /// The id of the user object, if author was a user.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("author")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int AuthorId { get; set; }

        /// <summary>
        /// Display name for the object author.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
		[JsonPropertyName("author_name")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? AuthorName { get; set; }

        /// <summary>
        /// Email address for the object author.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("author_email")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? AuthorEmail { get; set; }

        /// <summary>
        /// URL for the object author.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("author_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? AuthorUrl { get; set; }

        /// <summary>
        /// IP address for the object author.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("author_ip")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? AuthorIP { get; set; }

        /// <summary>
        /// Avatar URLs for the object author.
        /// <see cref="AvatarURL"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("author_avatar_urls")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public AvatarURL? AuthorAvatarUrls { get; set; }

        /// <summary>
        /// User agent for the object author.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("author_user_agent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? AuthorUserAgent { get; set; }

        /// <summary>
        /// The date the object was published.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("date")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime Date { get; set; }

        /// <summary>
        /// The date the object was published as GMT.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
		[JsonPropertyName("date_gmt")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime DateGmt { get; set; }

        /// <summary>
        /// The content for the object.
        /// <see cref="Models.Content"/>
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("content")]
        public Content? Content { get; set; }

        /// <summary>
        /// URL to the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("link")]
        public string? Link { get; set; }

        /// <summary>
        /// State of the object.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public CommentStatus Status { get; set; }

        /// <summary>
        /// Type of Comment for the object.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonPropertyName("type")]
		public string? Type { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("meta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public dynamic? Meta { get; set; }

        /// <summary>
        /// Karma for the object.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("karma")]
        public int Karma { get; set; }

        /// <summary>
        /// Links to another entities
        /// </summary>
        [JsonPropertyName("_links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Links? Links { get; set; }

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