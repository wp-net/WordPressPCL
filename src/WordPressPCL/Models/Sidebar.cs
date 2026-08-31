using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a widget sidebar from the WordPress REST API.
/// </summary>
public class Sidebar
{
    /// <summary>
    /// Unique string identifier for the sidebar.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    /// <summary>
    /// Human-readable sidebar name.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// Sidebar description.
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    /// <summary>
    /// Extra CSS class used by the widgets interface.
    /// </summary>
    [JsonPropertyName("class")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Class { get; set; }

    /// <summary>
    /// HTML placed before each widget.
    /// </summary>
    [JsonPropertyName("before_widget")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BeforeWidget { get; set; }

    /// <summary>
    /// HTML placed after each widget.
    /// </summary>
    [JsonPropertyName("after_widget")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AfterWidget { get; set; }

    /// <summary>
    /// HTML placed before each widget title.
    /// </summary>
    [JsonPropertyName("before_title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BeforeTitle { get; set; }

    /// <summary>
    /// HTML placed after each widget title.
    /// </summary>
    [JsonPropertyName("after_title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AfterTitle { get; set; }

    /// <summary>
    /// Sidebar status, either <c>active</c> or <c>inactive</c>.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; set; }

    /// <summary>
    /// Ordered widget IDs assigned to the sidebar.
    /// </summary>
    [JsonPropertyName("widgets")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Widgets { get; set; }

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
