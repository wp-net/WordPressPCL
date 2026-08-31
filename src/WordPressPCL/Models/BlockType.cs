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
    /// Unique name identifying the block type.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Human-readable title for the block type.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Description of the block type.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Icon for the block type.
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// Block attribute schemas keyed by attribute name.
    /// </summary>
    [JsonPropertyName("attributes")]
    public IDictionary<string, JsonElement>? Attributes { get; set; }

    /// <summary>
    /// Context values provided by blocks of this type.
    /// </summary>
    /// <remarks>
    /// Kept as JSON because older WordPress releases returned an array while current releases return an object.
    /// </remarks>
    [JsonPropertyName("provides_context")]
    public JsonElement? ProvidesContext { get; set; }

    /// <summary>
    /// Context values inherited by blocks of this type.
    /// </summary>
    [JsonPropertyName("uses_context")]
    public List<string>? UsesContext { get; set; }

    /// <summary>
    /// Custom CSS selectors keyed by feature.
    /// </summary>
    [JsonPropertyName("selectors")]
    public IDictionary<string, JsonElement>? Selectors { get; set; }

    /// <summary>
    /// Editor-facing settings supported by the block type.
    /// </summary>
    [JsonPropertyName("supports")]
    public IDictionary<string, JsonElement>? Supports { get; set; }

    /// <summary>
    /// Block category.
    /// </summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; }

    /// <summary>
    /// Whether the block is dynamically rendered.
    /// </summary>
    [JsonPropertyName("is_dynamic")]
    public bool IsDynamic { get; set; }

    /// <summary>
    /// Editor script handles.
    /// </summary>
    [JsonPropertyName("editor_script_handles")]
    public List<string>? EditorScriptHandles { get; set; }

    /// <summary>
    /// Public-facing and editor script handles.
    /// </summary>
    [JsonPropertyName("script_handles")]
    public List<string>? ScriptHandles { get; set; }

    /// <summary>
    /// Public-facing script handles.
    /// </summary>
    [JsonPropertyName("view_script_handles")]
    public List<string>? ViewScriptHandles { get; set; }

    /// <summary>
    /// Public-facing script module IDs.
    /// </summary>
    [JsonPropertyName("view_script_module_ids")]
    public List<string>? ViewScriptModuleIds { get; set; }

    /// <summary>
    /// Editor style handles.
    /// </summary>
    [JsonPropertyName("editor_style_handles")]
    public List<string>? EditorStyleHandles { get; set; }

    /// <summary>
    /// Public-facing and editor style handles.
    /// </summary>
    [JsonPropertyName("style_handles")]
    public List<string>? StyleHandles { get; set; }

    /// <summary>
    /// Public-facing style handles.
    /// </summary>
    [JsonPropertyName("view_style_handles")]
    public List<string>? ViewStyleHandles { get; set; }

    /// <summary>
    /// Block style variations.
    /// </summary>
    [JsonPropertyName("styles")]
    public List<JsonElement>? Styles { get; set; }

    /// <summary>
    /// Block variations.
    /// </summary>
    [JsonPropertyName("variations")]
    public List<JsonElement>? Variations { get; set; }

    /// <summary>
    /// Public text domain.
    /// </summary>
    [JsonPropertyName("textdomain")]
    public string? Textdomain { get; set; }

    /// <summary>
    /// Parent block types.
    /// </summary>
    [JsonPropertyName("parent")]
    public List<string>? Parent { get; set; }

    /// <summary>
    /// Ancestor block types.
    /// </summary>
    [JsonPropertyName("ancestor")]
    public List<string>? Ancestor { get; set; }

    /// <summary>
    /// Block types allowed as direct children.
    /// </summary>
    [JsonPropertyName("allowed_blocks")]
    public List<string>? AllowedBlocks { get; set; }

    /// <summary>
    /// Search keywords for the block type.
    /// </summary>
    [JsonPropertyName("keywords")]
    public List<string>? Keywords { get; set; }

    /// <summary>
    /// Example data for the block type.
    /// </summary>
    [JsonPropertyName("example")]
    public JsonElement? Example { get; set; }

    /// <summary>
    /// Block types and relative positions where this block is automatically inserted.
    /// </summary>
    [JsonPropertyName("block_hooks")]
    public IDictionary<string, string>? BlockHooks { get; set; }

    /// <summary>
    /// Deprecated editor script handle.
    /// </summary>
    [JsonPropertyName("editor_script")]
    public string? EditorScript { get; set; }

    /// <summary>
    /// Deprecated public-facing and editor script handle.
    /// </summary>
    [JsonPropertyName("script")]
    public string? Script { get; set; }

    /// <summary>
    /// Deprecated public-facing script handle.
    /// </summary>
    [JsonPropertyName("view_script")]
    public string? ViewScript { get; set; }

    /// <summary>
    /// Deprecated editor style handle.
    /// </summary>
    [JsonPropertyName("editor_style")]
    public string? EditorStyle { get; set; }

    /// <summary>
    /// Deprecated public-facing and editor style handle.
    /// </summary>
    [JsonPropertyName("style")]
    public string? Style { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    /// <summary>
    /// Additional fields supplied by newer WordPress versions, plugins or custom registrations.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement>? AdditionalFields { get; set; }
}
