using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    public class PostCreate
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
