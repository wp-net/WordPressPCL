using System.Text.Json.Serialization;


namespace WordPressPCL.Models;

/// <summary>
/// Model for unsuccessful request
/// </summary>
public class BadRequest
{
    /// <summary>
    /// Error type
    /// </summary>
    [JsonPropertyName("code")]
    public string? Name { get; set; }

    /// <summary>
    /// Error description
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Additional info
    /// </summary>
    [JsonPropertyName("data")]
    public dynamic? Data { get; set; }
}