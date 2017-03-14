using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    public class PostCreate
    {
        /// <summary>
        ///     The date the object was published, in the site’s timezone.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        ///     The date the object was published, as GMT.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("date_gmt")]
        public DateTime DateGmt { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        public PostCreate()
        {
            Date = DateTime.Now;
            DateGmt = DateTime.UtcNow;
        }
    }
}
