using System.Collections.Generic;
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

    /// <summary>
    /// Fields of the REST payload that are not mapped to a property of this model, such as those
    /// added by a plugin or by a custom endpoint.
    /// </summary>
    /// <remarks>
    /// Backed by <see cref="JsonExtensionDataAttribute"/>: unrecognized properties are collected here
    /// while deserializing, and each entry is written back as a top-level property while serializing.
    /// The property stays <see langword="null"/> when a response carries no unmapped fields, and neither
    /// a <see langword="null"/> nor an empty dictionary contributes anything to an outgoing payload.
    /// <para>
    /// Values are <see cref="System.Text.Json.JsonElement"/> instances after deserialization, while any
    /// CLR object may be assigned before a request is sent. Entries are written verbatim, so a key that
    /// collides with a mapped property name produces a duplicate JSON property.
    /// </para>
    /// <para>
    /// System.Text.Json allows a single extension data member per type, so a derived model must not
    /// declare one of its own.
    /// </para>
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, object>? CustomFields { get; set; }
}
