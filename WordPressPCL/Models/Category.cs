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
     
    /// <summary>
    /// Order sort attribute ascending or descending.
    /// </summary>
    /// <remarks>Default: asc</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Order
    {
        [EnumMember(Value = "asc")]
        Asc,
        [EnumMember(Value = "desc")]
        Desc
    }
    
    /// <summary>
    /// Sort collection by term attribute.
    /// </summary>
    /// <remarks>Default: name</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderBy
    {
        [EnumMember(Value = "name")]
        Name,
        [EnumMember(Value = "id")]
        Id,
        [EnumMember(Value = "include")]
        Include,
        [EnumMember(Value = "slug")]
        Slug
        [EnumMember(Value = "term_group")]
        TermGroup,
        [EnumMember(Value = "description")]
        Description,
        [EnumMember(Value = "count")]
        Count
        
    }

}
