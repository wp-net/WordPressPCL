using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Meta info (EXIF) of image media
    /// <see cref="WordPressPCL.Models.MediaDetails"/>
    /// </summary>
    public class ImageMeta
    {
        /// <summary>
        /// Aperture
        /// </summary>
        [JsonProperty("aperture")]
        public string Aperture { get; set; }
        /// <summary>
        /// Credit
        /// </summary>
        [JsonProperty("credit")]
        public string Credit { get; set; }
        /// <summary>
        /// Camera Model
        /// </summary>
        [JsonProperty("camera")]
        public string Camera { get; set; }
        /// <summary>
        /// Image Caption
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        [JsonProperty("created_timestamp")]
        public string CreatedTimestamp { get; set; }
        /// <summary>
        /// Copyright
        /// </summary>
        [JsonProperty("copyright")]
        public string Copyright { get; set; }
        /// <summary>
        /// Focal Length
        /// </summary>
        [JsonProperty("focal_length")]
        public string FocalLength { get; set; }
        /// <summary>
        /// ISO
        /// </summary>
        [JsonProperty("iso")]
        public string Iso { get; set; }
        /// <summary>
        /// Shutter Speed
        /// </summary>
        [JsonProperty("shutter_speed")]
        public string ShutterSpeed { get; set; }
        /// <summary>
        /// Image Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Orientation
        /// </summary>
        [JsonProperty("orientation")]
        public string Orientation { get; set; }
        /// <summary>
        /// Image keywords
        /// </summary>
        [JsonProperty("keywords")]
        public IList<string> Keywords { get; set; }
    }
}
