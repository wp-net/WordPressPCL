using System.Text.Json.Serialization;


namespace WordPressPCL.Models;

/// <summary>
/// Base class for Models
/// </summary>
public class Base
{
    /// <summary>
    /// Unique identifier for the object.
    /// </summary>
    /// <remarks>
    /// Read only
    /// Context: view, edit, embed
    /// </remarks>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }
}