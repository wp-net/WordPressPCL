using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a registered block type from the WordPress REST API (<c>wp/v2/block-types</c>).
/// </summary>
public class BlockType
{
    /// <summary>
    /// Block API version.
    /// </summary>
    [JsonPropertyName("api_version")]
    public int ApiVersion { get; set; }

    /// <summary>
    /// Unique name identifying the block type, e.g. "core/paragraph".
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Human-readable title for the block type.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// A short description of the block type.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The category the block type belongs to.
    /// </summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; }

    /// <summary>
    /// An icon for the block type.
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// Additional keywords that describe the block type.
    /// </summary>
    [JsonPropertyName("keywords")]
    public List<string>? Keywords { get; set; }

    /// <summary>
    /// The text domain for translations.
    /// </summary>
    [JsonPropertyName("textdomain")]
    public string? Textdomain { get; set; }

    /// <summary>
    /// Whether the block is dynamically rendered.
    /// </summary>
    [JsonPropertyName("is_dynamic")]
    public bool IsDynamic { get; set; }

    /// <summary>
    /// Editor-facing settings supported by the block type.
    /// </summary>
    [JsonPropertyName("supports")]
    public JsonElement? Supports { get; set; }

    /// <summary>
    /// Named block styles available for the block type.
    /// </summary>
    [JsonPropertyName("styles")]
    public JsonElement? Styles { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }
}
