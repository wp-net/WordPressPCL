using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Avatar URLs for the users.
    /// </summary>
    /// <remarks>Default sizes: 24, 48, 96</remarks>
    public class AvatarURL
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
