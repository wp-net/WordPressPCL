using System.Collections.Generic;
using Newtonsoft.Json;

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
        [JsonProperty("parent", NullValueHandling = NullValueHandling.Ignore)]
        public int Parent { get; set; }

        /// <summary>
        /// Meta fields.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("meta", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<object> Meta { get; set; }

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public Category() : base()
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name"></param>
        public Category(string name) : this()
        {
            Name = name;
        }
    }
}