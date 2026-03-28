using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WordPressPCL.Utility;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Type represents Entity User of WP REST API
    /// </summary>
    public class User : Base
    {
        /// <summary>
        /// Login name for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("username")]
        public string? UserName { get; set; }

        /// <summary>
        /// Display name for the user.
        /// </summary>
        /// <remarks>
        /// Context: embed, view, edit
        /// </remarks>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// First name for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Last name for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        /// <summary>
        /// The email address for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// URL of the user.
        /// </summary>
        /// <remarks>Context: embed, view, edit</remarks>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// Description of the user.
        /// </summary>
        /// <remarks>Context: embed, view, edit</remarks>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Author URL of the user.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: embed, view, edit
        /// </remarks>
        [JsonPropertyName("link")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Link { get; set; }

        /// <summary>
        /// Locale for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("locale")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Locale { get; set; }

        /// <summary>
        /// The nickname for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("nickname")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? NickName { get; set; }

        /// <summary>
        /// An alphanumeric identifier for the user.
        /// </summary>
        /// <remarks>Context: embed, view, edit</remarks>
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        /// <summary>
        /// Registration date for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("registered_date")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime RegisteredDate { get; set; }

        /// <summary>
        /// Roles assigned to the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("roles")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string>? Roles { get; set; }

        /// <summary>
        /// Password for the user (never included).
        /// </summary>
        /// <remarks>Context:</remarks>
        [JsonPropertyName("password")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Password { get; set; }

        /// <summary>
        /// All capabilities assigned to the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("capabilities")]
        [JsonConverter(typeof(CapabilitiesDictionaryConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IDictionary<string, bool>? Capabilities { get; set; }

        /// <summary>
        /// Any extra capabilities assigned to the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonPropertyName("extra_capabilities")]
        [JsonConverter(typeof(CapabilitiesDictionaryConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IDictionary<string, bool>? ExtraCapabilities { get; set; }

        /// <summary>
        /// Avatar URLs for the user.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: embed, view, edit
        /// </remarks>
        [JsonPropertyName("avatar_urls")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public AvatarURL? AvatarUrls { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonPropertyName("meta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public dynamic? Meta { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonPropertyName("_links")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Links? Links { get; set; }

        /// <summary>
        /// Constructor with required parameters
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        public User(string userName, string email, string password) : this()
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        /// <summary>
        /// parameterless constructor
        /// </summary>
        public User()
        {
        }
    }
}
