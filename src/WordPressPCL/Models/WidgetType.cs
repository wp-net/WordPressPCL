using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a registered widget type from the WordPress REST API.
/// </summary>
public class WidgetType
{
    /// <summary>
    /// Unique string identifier for the widget type.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Human-readable widget type name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Widget type description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Whether the widget type supports multiple instances.
    /// </summary>
    [JsonPropertyName("is_multi")]
    public bool IsMulti { get; set; }

    /// <summary>
    /// Widget type CSS class name.
    /// </summary>
    [JsonPropertyName("classname")]
    public string? Classname { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }

    /// <summary>
    /// Additional fields supplied by WordPress or plugins.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement>? AdditionalFields { get; set; }
}
