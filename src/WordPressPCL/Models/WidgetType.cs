using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a registered widget type from the WordPress REST API (<c>wp/v2/widget-types</c>).
/// </summary>
public class WidgetType
{
    /// <summary>
    /// Unique identifier for the widget type (e.g. "block").
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Human-readable name of the widget type.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the widget type.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Whether the widget supports multiple instances.
    /// </summary>
    [JsonPropertyName("is_multi")]
    public bool IsMulti { get; set; }

    /// <summary>
    /// CSS class name for the widget type.
    /// </summary>
    [JsonPropertyName("classname")]
    public string? Classname { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }
}
