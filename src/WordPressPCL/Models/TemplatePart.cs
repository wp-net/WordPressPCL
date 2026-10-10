using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a block template part from the WordPress REST API.
/// </summary>
public class TemplatePart : TemplateEntity
{
    /// <summary>
    /// Area where the template part is intended for use, such as header or footer.
    /// </summary>
    [JsonPropertyName("area")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Area { get; set; }
}
