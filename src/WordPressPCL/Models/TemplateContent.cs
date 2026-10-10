using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Block markup stored in a template.
/// </summary>
public class TemplateContent
{
    /// <summary>
    /// Raw block markup.
    /// </summary>
    [JsonPropertyName("raw")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Raw { get; set; }

    /// <summary>
    /// Version of the block content format.
    /// </summary>
    [JsonPropertyName("block_version")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? BlockVersion { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateContent()
    {
    }

    /// <summary>
    /// Creates template content from raw block markup.
    /// </summary>
    public TemplateContent(string raw)
    {
        Raw = raw;
    }
}
