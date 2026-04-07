using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a widget instance from the WordPress REST API (<c>wp/v2/widgets</c>).
/// </summary>
public class Widget
{
    /// <summary>
    /// Unique identifier for the widget.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The type of the widget (e.g. "block").
    /// </summary>
    [JsonPropertyName("id_base")]
    public string? IdBase { get; set; }

    /// <summary>
    /// The sidebar the widget belongs to.
    /// </summary>
    [JsonPropertyName("sidebar")]
    public string? Sidebar { get; set; }

    /// <summary>
    /// HTML representation of the widget.
    /// </summary>
    [JsonPropertyName("rendered")]
    public string? Rendered { get; set; }

    /// <summary>
    /// HTML representation of the widget admin form.
    /// </summary>
    [JsonPropertyName("rendered_form")]
    public string? RenderedForm { get; set; }

    /// <summary>
    /// The widget's serialized state, including raw settings values.
    /// </summary>
    [JsonPropertyName("instance")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public JsonElement? Instance { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Links? Links { get; set; }
}
