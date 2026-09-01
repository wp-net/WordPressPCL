using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using RenderedGuid = WordPressPCL.Models.Guid;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a reusable block from the WordPress REST API (<c>wp/v2/blocks</c>).
/// </summary>
public class Block : Base
{
    /// <summary>
    /// The date the block was published, in the site's timezone.
    /// </summary>
    [JsonPropertyName("date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Date { get; set; }

    /// <summary>
    /// The date the block was published, as GMT.
    /// </summary>
    [JsonPropertyName("date_gmt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? DateGmt { get; set; }

    /// <summary>
    /// The globally unique identifier for the block.
    /// </summary>
    [JsonPropertyName("guid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RenderedGuid? Guid { get; set; }

    /// <summary>
    /// URL to the block.
    /// </summary>
    [JsonPropertyName("link")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Link { get; set; }

    /// <summary>
    /// The date the block was last modified, in the site's timezone.
    /// </summary>
    [JsonPropertyName("modified")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Modified { get; set; }

    /// <summary>
    /// The date the block was last modified, as GMT.
    /// </summary>
    [JsonPropertyName("modified_gmt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ModifiedGmt { get; set; }

    /// <summary>
    /// A password to protect access to the content.
    /// </summary>
    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Password { get; set; }

    /// <summary>
    /// An alphanumeric identifier for the block unique to its type.
    /// </summary>
    [JsonPropertyName("slug")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Slug { get; set; }

    /// <summary>
    /// A named status for the block.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Status? Status { get; set; }

    /// <summary>
    /// Type of post for the block.
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }

    /// <summary>
    /// The title for the block.
    /// </summary>
    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Title? Title { get; set; }

    /// <summary>
    /// The content for the block.
    /// </summary>
    [JsonPropertyName("content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Content? Content { get; set; }

    /// <summary>
    /// The theme file to use to display the block.
    /// </summary>
    [JsonPropertyName("template")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Template { get; set; }

    /// <summary>
    /// Meta fields.
    /// </summary>
    [JsonPropertyName("meta")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public JsonElement? Meta { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Links? Links { get; set; }

    /// <summary>
    /// Embedded information.
    /// </summary>
    [JsonPropertyName("_embedded")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Embedded? Embedded { get; set; }
}
