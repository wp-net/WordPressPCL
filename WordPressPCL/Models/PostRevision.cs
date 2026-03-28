
using System;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Type represents Entity Post Revision of WP REST API
/// </summary>
public class PostRevision : Base
{
    /// <summary>
    /// The ID for the author of the object.
    /// </summary>
    /// <remarks>Context: view, edit, embed</remarks>
    [JsonPropertyName("author")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Author { get; set; }

    /// <summary>
    /// The date the object was published, in the site's timezone.
    /// </summary>
    /// <remarks>Context: view, edit, embed</remarks>
		[JsonPropertyName("date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime Date { get; set; }

    /// <summary>
    /// The date the object was published, as GMT.
    /// </summary>
    /// <remarks>Context: view, edit</remarks>
    [JsonPropertyName("date_gmt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime DateGmt { get; set; }

    /// <summary>
    /// The globally unique identifier for the object.
    /// </summary>
    /// <remarks>
    /// Read only
    /// Context: view, edit
    /// </remarks>
    [JsonPropertyName("guid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid? Guid { get; set; }

    /// <summary>
    /// The date the object was last modified, in the site's timezone.
    /// </summary>
    /// <remarks>
    /// Read only
    /// Context: view, edit
    /// </remarks>
    [JsonPropertyName("modified")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime Modified { get; set; }

    /// <summary>
    /// The date the object was last modified, as GMT.
    /// </summary>
    /// <remarks>
    /// Read only
    /// Context: view, edit
    /// </remarks>
    [JsonPropertyName("modified_gmt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime ModifiedGmt { get; set; }

    /// <summary>
    /// The id for the parent of the object.
    /// </summary>
    /// <remarks>Context: view</remarks>
    [JsonPropertyName("parent")]
    public int Parent { get; set; }

    /// <summary>
    /// An alphanumeric identifier for the object unique to its type.
    /// </summary>
    /// <remarks>Context: view, edit, embed</remarks>
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    /// <summary>
    /// The title for the object.
    /// </summary>
    /// <remarks>Context: view, edit, embed</remarks>
    [JsonPropertyName("title")]
    public Title? Title { get; set; }

    /// <summary>
    /// The content for the object.
    /// </summary>
    /// <remarks>Context: view, edit</remarks>
    [JsonPropertyName("content")]
    public Content? Content { get; set; }

    /// <summary>
    /// The excerpt for the object.
    /// </summary>
    /// <remarks>Context: view, edit, embed</remarks>
    [JsonPropertyName("excerpt")]
    public Excerpt? Excerpt { get; set; }

    /// <summary>
    /// Meta fields.
    /// </summary>
    /// <remarks>Context: view, edit</remarks>
    [JsonPropertyName("meta")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public dynamic? Meta { get; set; }

    /// <summary>
    /// Links to related resources
    /// </summary>
    [JsonPropertyName("_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Links? Links { get; set; }

    /// <summary>
    /// Embedded information like featured images
    /// </summary>
    [JsonPropertyName("_embedded")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Embedded? Embedded { get; set; }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public PostRevision()
    {
    }
}