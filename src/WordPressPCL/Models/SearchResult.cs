using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a result from the WordPress REST API search endpoint (<c>wp/v2/search</c>).
/// </summary>
public class SearchResult
{
    /// <summary>
    /// Unique identifier for the object.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// The title for the object.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// URL to the object.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Object type.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Object subtype.
    /// </summary>
    [JsonPropertyName("subtype")]
    public string? Subtype { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    public Links? Links { get; set; }
}
