using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WordPressPCL.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        [EnumMember(Value = "publish")]
        Publish,
        [EnumMember(Value = "private")]
        Private,
        [EnumMember(Value = "future")]
        Future,
        [EnumMember(Value = "draft")]
        Draft,
        [EnumMember(Value = "pending")]
        Pending

    }

    /// <summary>
    /// Status of Comments, Pings etc.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpenStatus
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

    /// <summary>
    /// Sort collection by object attribute.
    /// </summary>
    /// <remarks>Default: date</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderBy
    {
        [EnumMember(Value = "date")]
        Date,
        [EnumMember(Value = "relevance")]
        Relevance,
        [EnumMember(Value = "id")]
        Id,
        [EnumMember(Value = "include")]
        Include,
        [EnumMember(Value = "title")]
        Title,
        [EnumMember(Value = "slug")]
        Slug
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
}
