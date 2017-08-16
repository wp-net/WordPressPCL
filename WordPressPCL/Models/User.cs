using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [JsonProperty("username")]
        public string UserName { get; set; }

        /// <summary>
        /// Display name for the user.
        /// </summary>
        /// <remarks>
        /// Context: embed, view, edit
        /// </remarks>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// First name for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// The email address for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// URL of the user.
        /// </summary>
        /// <remarks>Context: embed, view, edit</remarks>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Description of the user.
        /// </summary>
        /// <remarks>Context: embed, view, edit</remarks>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Author URL of the user.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: embed, view, edit
        /// </remarks>
        [JsonProperty("link", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Link { get; set; }

        /// <summary>
        /// Locale for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("locale", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Locale { get; set; }

        /// <summary>
        /// The nickname for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("nickname", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string NickName { get; set; }

        /// <summary>
        /// An alphanumeric identifier for the user.
        /// </summary>
        /// <remarks>Context: embed, view, edit</remarks>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Registration date for the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("registered_date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime RegisteredDate { get; set; }

        /// <summary>
        /// Roles assigned to the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("roles", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// Password for the user (never included).
        /// </summary>
        /// <remarks>Context:</remarks>
        [JsonProperty("password", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Password { get; set; }

        /// <summary>
        /// All capabilities assigned to the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("capabilities", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<string, bool> Capabilities { get; set; }

        /// <summary>
        /// Any extra capabilities assigned to the user.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("extra_capabilities", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<string, bool> ExtraCapabilities { get; set; }

        /// <summary>
        /// Avatar URLs for the user.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: embed, view, edit
        /// </remarks>
        [JsonProperty("avatar_urls", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public AvatarURL AvatarUrls { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<object> Meta { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

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