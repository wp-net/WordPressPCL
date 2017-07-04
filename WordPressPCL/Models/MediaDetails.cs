using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    public class MediaDetails
    {

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("sizes")]
        public Dictionary<string, MediaSize> Sizes { get; set; }

        [JsonProperty("image_meta")]
        public ImageMeta ImageMeta { get; set; }
    }
}
