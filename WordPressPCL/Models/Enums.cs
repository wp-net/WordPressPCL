using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Status of Post/Page/Media
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        /// <summary>
        /// Publish
        /// </summary>
        [EnumMember(Value = "publish")]
        Publish,
        /// <summary>
        /// Private
        /// </summary>
        [EnumMember(Value = "private")]
        Private,
        /// <summary>
        /// Future publication
        /// </summary>
        [EnumMember(Value = "future")]
        Future,
        /// <summary>
        /// Draft
        /// </summary>
        [EnumMember(Value = "draft")]
        Draft,
        /// <summary>
        /// Pending
        /// </summary>
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
    /// Sort posts collection by object attribute.
    /// </summary>
    /// <remarks>Default: date</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PostsOrderBy
    {
        /// <summary>
        /// Order By Date
        /// </summary>
        [EnumMember(Value = "date")]
        Date,
        /// <summary>
        /// Order By Relevance
        /// </summary>
        [EnumMember(Value = "relevance")]
        Relevance,

        /// <summary>
        /// Order By Id
        /// </summary>
        [EnumMember(Value = "id")]
        Id,
        /// <summary>
        /// Order By Include
        /// </summary>
        [EnumMember(Value = "include")]
        Include,
        /// <summary>
        /// Order By Title
        /// </summary>
        [EnumMember(Value = "title")]
        Title,
        /// <summary>
        /// Order By Slug
        /// </summary>
        [EnumMember(Value = "slug")]
        Slug
    }
    /// <summary>
    /// Order By direction
    /// </summary>
    public enum Order
    {
        /// <summary>
        /// Ascending direction
        /// </summary>
        [EnumMember(Value ="asc")]
        ASC,
        /// <summary>
        /// Descending direction
        /// </summary>
        [EnumMember(Value ="desc")]
        DESC
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
        /// <summary>
        /// Available in view
        /// </summary>
        [EnumMember(Value = "view")]
        View,
        /// <summary>
        /// Available in Embed
        /// </summary>
        [EnumMember(Value = "embed")]
        Embed,
        /// <summary>
        /// Available in edit
        /// </summary>
        [EnumMember(Value = "edit")]
        Edit
    }
    /// <summary>
    /// Type of Media Item
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaType
    {
        /// <summary>
        /// Image
        /// </summary>
        [EnumMember(Value = "image")]
        Image,
        /// <summary>
        /// File
        /// </summary>
        [EnumMember(Value = "file")]
        File

    }
}
