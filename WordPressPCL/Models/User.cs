using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: embed, view, edit
        /// </remarks>
        [JsonProperty("id")]
        public int Id { get; set; }

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

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

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
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// An alphanumeric identifier for the user.
        /// </summary>
        /// <remarks>Context: embed, view, edit</remarks>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("registered_date")]
        public DateTime RegisteredDate { get; set; }

        [JsonProperty("roles")]
        public IEnumerable<string> Roles { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("capabilities")]
        public IEnumerable<object> Capabilities { get; set; }

        [JsonProperty("extra_capabilities")]
        public IEnumerable<object> ExtraCapabilities { get; set; }

        /// <summary>
        /// Avatar URLs for the user.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: embed, view, edit
        /// </remarks>
        [JsonProperty("avatar_urls")]
        public AvatarURL AvatarUrls { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta")]
        public IEnumerable<object> Meta { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonProperty("_links")]
        public Links Links { get; set; }
    }

}
