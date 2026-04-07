using System.Text.Json;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a global styles object from the WordPress REST API (<c>wp/v2/global-styles/{id}</c>).
/// </summary>
public class GlobalStyles : Base
{
    /// <summary>
    /// The title for the global styles object.
    /// </summary>
    [JsonPropertyName("title")]
    public Title? Title { get; set; }

    /// <summary>
    /// Global settings as defined in theme.json.
    /// </summary>
    [JsonPropertyName("settings")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public JsonElement? Settings { get; set; }

    /// <summary>
    /// Global styles as defined in theme.json.
    /// </summary>
    [JsonPropertyName("styles")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public JsonElement? Styles { get; set; }

    /// <summary>
    /// Whether this is the global styles user theme JSON.
    /// </summary>
    [JsonPropertyName("is_global_styles_user_theme_json")]
    public bool IsGlobalStylesUserThemeJson { get; set; }

    /// <summary>
    /// Links to related resources.
    /// </summary>
    [JsonPropertyName("_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Links? Links { get; set; }

    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public GlobalStyles()
    {
    }
}
