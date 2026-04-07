using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a widget sidebar from the WordPress REST API (<c>wp/v2/sidebars</c>).
/// </summary>
public class Sidebar
{
    /// <summary>
    /// Unique identifier for the sidebar.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Unique name identifying the sidebar.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the sidebar.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Extra CSS class to assign to the sidebar in the customizer.
    /// </summary>
    [JsonPropertyName("class")]
    public string? Class { get; set; }

    /// <summary>
    /// HTML content to prepend to each widget's HTML output when assigned to this sidebar.
    /// </summary>
    [JsonPropertyName("before_widget")]
    public string? BeforeWidget { get; set; }

    /// <summary>
    /// HTML content to append to each widget's HTML output when assigned to this sidebar.
    /// </summary>
    [JsonPropertyName("after_widget")]
    public string? AfterWidget { get; set; }

    /// <summary>
    /// HTML content to prepend to each widget's title when assigned to this sidebar.
    /// </summary>
    [JsonPropertyName("before_title")]
    public string? BeforeTitle { get; set; }

    /// <summary>
    /// HTML content to append to each widget's title when assigned to this sidebar.
    /// </summary>
    [JsonPropertyName("after_title")]
    public string? AfterTitle { get; set; }

    /// <summary>
    /// Status of the sidebar.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Nested widgets assigned to the sidebar.
    /// </summary>
    [JsonPropertyName("widgets")]
    public List<string>? Widgets { get; set; }
}
