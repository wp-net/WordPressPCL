using Newtonsoft.Json;
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

        /// <summary>
        /// Display name for the user.
        /// </summary>
        /// <remarks>
        /// Context: embed, view, edit
        /// </remarks>
        [JsonProperty("name")]
        public string Name { get; set; }

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

        /// <summary>
        /// An alphanumeric identifier for the user.
        /// </summary>
        /// <remarks>Context: embed, view, edit</remarks>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Avatar URLs for the user.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: embed, view, edit
        /// </remarks>
        [JsonProperty("avatar_urls")]
        public AvatarUrls AvatarUrls { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta")]
        public IList<object> Meta { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonProperty("_links")]
        public Links Links { get; set; }
    }

    /// <summary>
    /// Avatar URLs for the users.
    /// </summary>
    /// <remarks>Default sizes: 24, 48, 96</remarks>
    public class AvatarUrls
    {
        /// <summary>
        /// Avatar URL 24x24 pixels
        /// </summary>
        [JsonProperty("24")]
        public string Size24 { get; set; }

        /// <summary>
        /// Avatar URL 48x48 pixels
        /// </summary>
        [JsonProperty("48")]
        public string Size48 { get; set; }

        /// <summary>
        /// Avatar URL 96x96 pixels
        /// </summary>
        [JsonProperty("96")]
        public string Size96 { get; set; }
    }

}
