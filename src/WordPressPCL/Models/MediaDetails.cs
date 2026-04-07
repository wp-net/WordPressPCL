
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Details of media item
/// <see cref="MediaItem.MediaDetails"/>
/// </summary>
public class MediaDetails
{
    /// <summary>
    /// Media width
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }
    /// <summary>
    /// Media height
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }
    /// <summary>
    /// File
    /// </summary>
    [JsonPropertyName("file")]
    public string? File { get; set; }
    /// <summary>
    /// Sizes
    /// </summary>
    [JsonPropertyName("sizes")]
    public IDictionary<string, MediaSize>? Sizes { get; set; }
    /// <summary>
    /// Meta info of Image
    /// </summary>
    [JsonPropertyName("image_meta")]
    public ImageMeta? ImageMeta { get; set; }
}
