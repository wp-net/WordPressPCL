using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a block template from the WordPress REST API.
/// </summary>
public class Template : TemplateEntity
{
    /// <summary>
    /// Whether this is a custom template rather than part of the template hierarchy.
    /// </summary>
    [JsonPropertyName("is_custom")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsCustom { get; set; }

    /// <summary>
    /// Plugin that registered the template.
    /// </summary>
    [JsonPropertyName("plugin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Plugin { get; set; }
}
