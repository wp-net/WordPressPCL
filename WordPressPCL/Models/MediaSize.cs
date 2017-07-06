using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Info about Media Size
    /// <see cref="MediaDetails.Sizes"/>
    /// </summary>
    public class MediaSize
    {
        /// <summary>
        /// File
        /// </summary>
        [JsonProperty("file")]
        public string File { get; set; }
        /// <summary>
        /// Media Width
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }
        /// <summary>
        /// Media Height
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }
        /// <summary>
        /// Mime Type
        /// </summary>
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
        /// <summary>
        /// Url of source media
        /// </summary>
        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }
    }
}
