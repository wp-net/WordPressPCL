using System.Text.Json.Serialization;


namespace WordPressPCL.Models;

/// <summary>
/// Info about Media Size
/// <see cref="MediaDetails.Sizes"/>
/// </summary>
public class MediaSize
{
    /// <summary>
    /// File
    /// </summary>
    [JsonPropertyName("file")]
    public string? File { get; set; }
    /// <summary>
    /// Media Width
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; set; }
    /// <summary>
    /// Media Height
    /// </summary>
    [JsonPropertyName("height")]
    public int? Height { get; set; }
    /// <summary>
    /// Mime Type
    /// </summary>
    [JsonPropertyName("mime_type")]
    public string? MimeType { get; set; }
    /// <summary>
    /// Url of source media
    /// </summary>
    [JsonPropertyName("source_url")]
    public string? SourceUrl { get; set; }
}
