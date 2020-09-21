using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

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
        Pending,

        /// <summary>
        /// Pending
        /// </summary>
        [EnumMember(Value = "trash")]
        Trash
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
    /// Status of Comments
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommentStatus
    {
        /// <summary>
        /// Comment is pending to approve
        /// </summary>
        [EnumMember(Value = "hold")]
        Pending,

        /// <summary>
        /// Comment Approved
        /// </summary>
        [EnumMember(Value = "approved")]
        Approved,

        /// <summary>
        /// Comment is spam
        /// </summary>
        [EnumMember(Value = "spam")]
        Spam,

        /// <summary>
        /// Comment is in trash
        /// </summary>
        [EnumMember(Value = "trash")]
        Trash,
    }

    /// <summary>
    /// Status of Media for query builder
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaQueryStatus
    {
        /// <summary>
        /// Media inherit
        /// </summary>
        [EnumMember(Value = "inherit")]
        Inherit,

        /// <summary>
        /// Media is private
        /// </summary>
        [EnumMember(Value = "private")]
        Private,

        /// <summary>
        /// Media is in trash
        /// </summary>
        [EnumMember(Value = "trash")]
        Trash,
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
        Slug,

        /// <summary>
        /// Order By Author
        /// </summary>
        [EnumMember(Value = "author")]
        Author,

        /// <summary>
        /// Order By modified
        /// </summary>
        [EnumMember(Value = "modified")]
        Modified,

        /// <summary>
        /// Order By parent
        /// </summary>
        [EnumMember(Value = "parent")]
        Parent
    }

    /// <summary>
    /// Sort users collection by object attribute.
    /// </summary>
    /// <remarks>Default: name</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UsersOrderBy
    {
        /// <summary>
        /// Order By Name
        /// </summary>
        [EnumMember(Value = "name")]
        Name,

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
        /// Order By Registered Date
        /// </summary>
        [EnumMember(Value = "registered_date")]
        RegisteredDate,

        /// <summary>
        /// Order By Slug
        /// </summary>
        [EnumMember(Value = "slug")]
        Slug,

        /// <summary>
        /// Order By email
        /// </summary>
        [EnumMember(Value = "email")]
        Email,

        /// <summary>
        /// Order By url
        /// </summary>
        [EnumMember(Value = "url")]
        Url
    }

    /// <summary>
    /// Sort terms collection by object attribute.
    /// </summary>
    /// <remarks>Default: name</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TermsOrderBy
    {
        /// <summary>
        /// Order By Name
        /// </summary>
        [EnumMember(Value = "name")]
        Name,

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
        /// Order By Slug
        /// </summary>
        [EnumMember(Value = "slug")]
        Slug,

        /// <summary>
        /// Order By term group
        /// </summary>
        [EnumMember(Value = "term_group")]
        TermGroup,

        /// <summary>
        /// Order By description
        /// </summary>
        [EnumMember(Value = "description")]
        Description,

        /// <summary>
        /// Order By count
        /// </summary>
        [EnumMember(Value = "count")]
        Count
    }

    /// <summary>
    /// Sort posts collection by object attribute.
    /// </summary>
    /// <remarks>Default: date</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaOrderBy
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
    /// Sort pages collection by object attribute.
    /// </summary>
    /// <remarks>Default: date</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PagesOrderBy
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
        Slug,

        /// <summary>
        /// Order By Menu order
        /// </summary>
        [EnumMember(Value = "menu_order")]
        MenuOrder
    }

    /// <summary>
    /// Sort comments collection by object attribute.
    /// </summary>
    /// <remarks>Default: date</remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CommentsOrderBy
    {
        /// <summary>
        /// Order By Date
        /// </summary>
        [EnumMember(Value = "date_gmt")]
        DateGmt,

        /// <summary>
        /// Order By Date
        /// </summary>
        [EnumMember(Value = "date")]
        Date,

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
        /// Order By Post
        /// </summary>
        [EnumMember(Value = "post")]
        Post,

        /// <summary>
        /// Order By Parent
        /// </summary>
        [EnumMember(Value = "parent")]
        Parent,

        /// <summary>
        /// Order ByType
        /// </summary>
        [EnumMember(Value = "type")]
        Type
    }

    /// <summary>
    /// Order By direction
    /// </summary>
    public enum Order
    {        
        /// <summary>
        /// Descending direction
        /// </summary>
        [EnumMember(Value = "desc")]
        DESC,

        /// <summary>
        /// Ascending direction
        /// </summary>
        [EnumMember(Value = "asc")]
        ASC
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

    /// <summary>
    /// Type of Media Query Item
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaQueryType
    {
        /// <summary>
        /// Image
        /// </summary>
        [EnumMember(Value = "image")]
        Image,

        /// <summary>
        /// Video
        /// </summary>
        [EnumMember(Value = "video")]
        Video,

        /// <summary>
        /// Audio
        /// </summary>
        [EnumMember(Value = "audio")]
        Audio,

        /// <summary>
        /// Application
        /// </summary>
        [EnumMember(Value = "application")]
        Application
    }
}