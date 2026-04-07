using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a block template from the WordPress REST API (<c>wp/v2/templates</c>).
/// Templates have a compound string identifier such as <c>twentytwentyfour//index</c>.
/// </summary>
public class Template
{
    /// <summary>
    /// Unique identifier for the template, e.g. "twentytwentyfour//index".
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// An alphanumeric identifier for the template unique to its type.
    /// </summary>
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    /// <summary>
    /// The theme the template was created for.
    /// </summary>
    [JsonPropertyName("theme")]
    public string? Theme { get; set; }

    /// <summary>
    /// Type of post — always <c>wp_template</c>.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Source of template (theme or custom).
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>
    /// Where the template originally comes from (theme or custom).
    /// </summary>
    [JsonPropertyName("origin")]
    public string? Origin { get; set; }

    /// <summary>
    /// The content for the template.
    /// </summary>
    [JsonPropertyName("content")]
    public Content? Content { get; set; }

    /// <summary>
    /// The title for the template.
    /// </summary>
    [JsonPropertyName("title")]
    public Title? Title { get; set; }

    /// <summary>
    /// A human-readable description of the template.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// A named status for the template.
    /// </summary>
    [JsonPropertyName("status")]
    public Status Status { get; set; }

    /// <summary>
    /// The ID of the associated post for custom templates.
    /// </summary>
    [JsonPropertyName("wp_id")]
    public int? WpId { get; set; }

    /// <summary>
    /// Whether the template has a matching file in the active theme.
    /// </summary>
    [JsonPropertyName("has_theme_file")]
    public bool HasThemeFile { get; set; }

    /// <summary>
    /// The ID of the user who last modified the template.
    /// </summary>
    [JsonPropertyName("author")]
    public int Author { get; set; }

    /// <summary>
    /// Whether the template is custom (not bundled with the theme).
    /// </summary>
    [JsonPropertyName("is_custom")]
    public bool IsCustom { get; set; }

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
    /// Parameterless constructor.
    /// </summary>
    public Template()
    {
    }
}
