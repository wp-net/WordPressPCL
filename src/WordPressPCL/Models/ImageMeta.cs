
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Meta info (EXIF) of image media
/// <see cref="MediaDetails"/>
/// </summary>
public class ImageMeta
{
    /// <summary>
    /// Aperture
    /// </summary>
    [JsonPropertyName("aperture")]
    public string? Aperture { get; set; }
    /// <summary>
    /// Credit
    /// </summary>
    [JsonPropertyName("credit")]
    public string? Credit { get; set; }
    /// <summary>
    /// Camera Model
    /// </summary>
    [JsonPropertyName("camera")]
    public string? Camera { get; set; }
    /// <summary>
    /// Image Caption
    /// </summary>
    [JsonPropertyName("caption")]
    public string? Caption { get; set; }
    /// <summary>
    /// Created Date
    /// </summary>
    [JsonPropertyName("created_timestamp")]
    public string? CreatedTimestamp { get; set; }
    /// <summary>
    /// Copyright
    /// </summary>
    [JsonPropertyName("copyright")]
    public string? Copyright { get; set; }
    /// <summary>
    /// Focal Length
    /// </summary>
    [JsonPropertyName("focal_length")]
    public string? FocalLength { get; set; }
    /// <summary>
    /// ISO
    /// </summary>
    [JsonPropertyName("iso")]
    public string? Iso { get; set; }
    /// <summary>
    /// Shutter Speed
    /// </summary>
    [JsonPropertyName("shutter_speed")]
    public string? ShutterSpeed { get; set; }
    /// <summary>
    /// Image Title
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    /// <summary>
    /// Orientation
    /// </summary>
    [JsonPropertyName("orientation")]
    public string? Orientation { get; set; }
    /// <summary>
    /// Image keywords
    /// </summary>
    [JsonPropertyName("keywords")]
    public IList<string>? Keywords { get; set; }
}
