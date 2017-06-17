using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Terms of the type category
    /// </summary>
    public class Category : Term
    {
        /// <summary>
        /// Number of published posts for the term.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit
        /// </remarks>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// HTML description of the term.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The parent term ID.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("parent")]
        public int Parent { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta")]
        public IList<object> Meta { get; set; }

    }

}
