using Newtonsoft.Json;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Terms of the type tag
    /// </summary>
    public class Tag : Term
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
        /// Meta object
        /// </summary>
        /// <remarks>Context: view</remarks>
        [JsonProperty("meta", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<object> Meta { get; set; }
        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public Tag():base()
        {

        }
        /// <summary>
        /// Constructor with required parameters
        /// </summary>
        /// <param name="name">Tag name</param>
        public Tag(string name):this()
        {
            Name = name;
        }
    }
}
