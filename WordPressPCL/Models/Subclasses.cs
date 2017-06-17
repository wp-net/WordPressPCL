using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace WordPressPCL.Models
{
    /// <summary>
    /// The actual content of the object, Rendered and/or Raw depending on the context
    /// </summary>
    public class Content : RenderedRawBase
	{
        [JsonProperty("protected")]
        public bool IsProtected { get; set; }
    }

    /// <summary>
    /// Adds a "Rendered" and a "Raw" property to all classes derived from this one
    /// </summary>
    public abstract class RenderedRawBase
    {
        [JsonProperty("rendered")]
        public string Rendered { get; set; }
        [JsonProperty("raw")]
        public string Raw { get; set; }
    }

    /// <summary>
    /// Adds an Href property to all classes derived from this one
    /// </summary>
    public abstract class HrefBase
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }


    /// <summary>
    /// Scope under which the request is made; determines fields present in response.
    /// </summary>
    /// <remarks>
    /// Default: view
    /// One of: view, embed, edit
    /// </remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Context
    {
        [EnumMember(Value = "view")]
        View,
        [EnumMember(Value = "embed")]
        Embed,
        [EnumMember(Value = "edit")]
        Edit
    }


    /// <summary>
    /// Status of Comments, Pings etc.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        /// <summary>
        /// Status is open
        /// </summary>
        [EnumMember(Value = "open")]
        Open,
        /// <summary>
        /// Status is closed
        /// </summary>
        [EnumMember(Value = "closed")]
        Closed,
    }
}