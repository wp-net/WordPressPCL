using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Links helper class
/// </summary>
public class Links
{
    /// <summary>
    /// Self link
    /// </summary>
    [JsonPropertyName("self")]
    public List<Self>? Self { get; set; }

    /// <summary>
    /// Collection of links
    /// <see cref="Models.Collection"/>
    /// </summary>
    [JsonPropertyName("collection")]
    public List<Collection>? Collection { get; set; }

    /// <summary>
    /// About info
    /// <see cref="Models.About"/>
    /// </summary>
    [JsonPropertyName("about")]
    public List<About>? About { get; set; }

    /// <summary>
    /// WordPress post Type
    /// </summary>
    [JsonPropertyName("wp:post_type")]
    public List<WpPostType>? WpPostType { get; set; }

    /// <summary>
    /// Curries
    /// </summary>
    [JsonPropertyName("curies")]
    public List<Cury>? Curies { get; set; }

    /// <summary>
    /// Author
    /// </summary>
    [JsonPropertyName("author")]
    public List<Author>? Author { get; set; }

    /// <summary>
    /// Replies
    /// </summary>
    [JsonPropertyName("replies")]
    public List<Reply>? Replies { get; set; }

    /// <summary>
    /// Versions
    /// </summary>
    [JsonPropertyName("version-history")]
    public List<VersionHistory>? Versions { get; set; }

    /// <summary>
    /// Attachment
    /// </summary>
    [JsonPropertyName("wp:attachment")]
    public List<HttpsApiWOrgAttachment>? Attachment { get; set; }

    /// <summary>
    /// Featured media
    /// </summary>
    [JsonPropertyName("wp:featuredmedia")]
    public List<HttpsApiWOrgFeaturedmedia>? FeaturedMedia { get; set; }

    /// <summary>
    /// Featured media
    /// </summary>
    [JsonPropertyName("wp:term")]
    public List<HttpsApiWOrgTerm>? Term { get; set; }
}
