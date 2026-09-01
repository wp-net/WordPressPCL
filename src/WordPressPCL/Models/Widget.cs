using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a widget instance from the WordPress REST API.
/// </summary>
public class Widget
{
    /// <summary>
    /// Server-generated string identifier for the widget.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    /// <summary>
    /// Widget type ID, corresponding to an ID from <c>wp/v2/widget-types</c>.
    /// </summary>
    [JsonPropertyName("id_base")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IdBase { get; set; }

    /// <summary>
    /// Sidebar containing the widget.
    /// </summary>
    [JsonPropertyName("sidebar")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Sidebar { get; set; }

    /// <summary>
    /// Rendered widget HTML.
    /// </summary>
    [JsonPropertyName("rendered")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Rendered { get; set; }

    /// <summary>
    /// Rendered widget administration form, available in edit context.
    /// </summary>
    [JsonPropertyName("rendered_form")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RenderedForm { get; set; }

    /// <summary>
    /// Widget instance settings, available in edit context.
    /// </summary>
    [JsonPropertyName("instance")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public WidgetInstance? Instance { get; set; }

    /// <summary>
    /// URL-encoded widget form data used for widgets that do not expose instance settings.
    /// </summary>
    [JsonPropertyName("form_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FormData { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Links? Links { get; set; }

    /// <summary>
    /// Additional fields supplied by WordPress or plugins.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement>? AdditionalFields { get; set; }
}

/// <summary>
/// Represents the encoded or raw settings for a widget instance.
/// </summary>
public class WidgetInstance
{
    /// <summary>
    /// Base64-encoded serialized instance settings.
    /// </summary>
    [JsonPropertyName("encoded")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Encoded { get; set; }

    /// <summary>
    /// Cryptographic hash for <see cref="Encoded"/>.
    /// </summary>
    [JsonPropertyName("hash")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Hash { get; set; }

    /// <summary>
    /// Unencoded instance settings for widget types that expose them.
    /// </summary>
    [JsonPropertyName("raw")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public JsonElement? Raw { get; set; }
}
