using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a reusable block from the WordPress REST API (<c>wp/v2/blocks</c>).
/// </summary>
public class Block : Base
{
    /// <summary>
    /// The date the object was published, in the site's timezone.
    /// </summary>
    [JsonPropertyName("date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime Date { get; set; }

    /// <summary>
    /// The date the object was published, as GMT.
    /// </summary>
    [JsonPropertyName("date_gmt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime DateGmt { get; set; }

    /// <summary>
    /// The globally unique identifier for the object.
    /// </summary>
    [JsonPropertyName("guid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid? Guid { get; set; }

    /// <summary>
    /// The date the object was last modified, in the site's timezone.
    /// </summary>
    [JsonPropertyName("modified")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime Modified { get; set; }

    /// <summary>
    /// The date the object was last modified, as GMT.
    /// </summary>
    [JsonPropertyName("modified_gmt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime ModifiedGmt { get; set; }

    /// <summary>
    /// An alphanumeric identifier for the object unique to its type.
    /// </summary>
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    /// <summary>
    /// A named status for the object.
    /// </summary>
    [JsonPropertyName("status")]
    public Status Status { get; set; }

    /// <summary>
    /// The title for the object.
    /// </summary>
    [JsonPropertyName("title")]
    public Title? Title { get; set; }

    /// <summary>
    /// The content for the object.
    /// </summary>
    [JsonPropertyName("content")]
    public Content? Content { get; set; }

    /// <summary>
    /// Meta fields.
    /// </summary>
    [JsonPropertyName("meta")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public JsonElement? Meta { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Links? Links { get; set; }

    /// <summary>
    /// Embedded information.
    /// </summary>
    [JsonPropertyName("_embedded")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Embedded? Embedded { get; set; }

    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public Block()
    {
    }
}
