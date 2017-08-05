using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Details of media item
    /// <see cref="WordPressPCL.Models.MediaItem.MediaDetails"/>
    /// </summary>
    public class MediaDetails
    {
        /// <summary>
        /// Media width
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }
        /// <summary>
        /// Media height
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }
        /// <summary>
        /// File
        /// </summary>
        [JsonProperty("file")]
        public string File { get; set; }
        /// <summary>
        /// Sizes
        /// </summary>
        [JsonProperty("sizes")]
        public IDictionary<string, MediaSize> Sizes { get; set; }
        /// <summary>
        /// Meta info of Image
        /// </summary>
        [JsonProperty("image_meta")]
        public ImageMeta ImageMeta { get; set; }
    }
}
