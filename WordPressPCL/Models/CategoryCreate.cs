using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    public class CategoryCreate
    {
        /// <summary>
        ///     HTML description of the term.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     HTML title for the term.
        /// </summary>
        /// <remarks>Context: view, embed, edit</remarks>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     An alphanumeric identifier for the term unique to its type.
        /// </summary>
        /// <remarks>Context: view, embed, edit</remarks>
        [JsonProperty("slug")]
        public string Slug { get; set; }
        
        /// <summary>
        ///     The parent term ID.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("parent")]
        public int Parent { get; set; }
        
        /// <summary>
        ///     Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta")]
        public List<string> Meta { get; set; }

        public CategoryCreate()
        {
        }
    }
}
