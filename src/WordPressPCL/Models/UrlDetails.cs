using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents the URL details returned by the WordPress REST API (<c>wp/v2/url-details</c>).
/// </summary>
public class UrlDetails
{
    /// <summary>
    /// The page title of the linked URL.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// The icon URL of the linked URL.
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// A description of the linked URL.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The Open Graph image of the linked URL.
    /// </summary>
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    /// <summary>
    /// The Open Graph description of the linked URL.
    /// </summary>
    [JsonPropertyName("og_description")]
    public string? OgDescription { get; set; }

    /// <summary>
    /// The Open Graph title of the linked URL.
    /// </summary>
    [JsonPropertyName("og_title")]
    public string? OgTitle { get; set; }

    /// <summary>
    /// The Open Graph image data of the linked URL.
    /// </summary>
    [JsonPropertyName("og_image")]
    public List<OgImage>? OgImage { get; set; }
}

/// <summary>
/// Open Graph image entry returned inside <see cref="UrlDetails"/>.
/// </summary>
public class OgImage
{
    /// <summary>
    /// The image width in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; }

    /// <summary>
    /// The image height in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; }

    /// <summary>
    /// The image URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// The MIME type of the image.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
