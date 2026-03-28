using System.Text.Json.Serialization;


namespace WordPressPCL.Models;

/// <summary>
/// WordPress main settings
/// </summary>
public class Settings
{
    /// <summary>
    /// Site title.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Site description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Site URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// This address is used for admin purposes. If you change this we will send you an email at your new address to confirm it. The new address will not become active until confirmed.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// A city in the same timezone as you.
    /// </summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    /// <summary>
    /// A date format for all date strings.
    /// </summary>
    [JsonPropertyName("date_format")]
    public string? DateFormat { get; set; }

    /// <summary>
    /// A time format for all time strings.
    /// </summary>
    [JsonPropertyName("time_format")]
    public string? TimeFormat { get; set; }

    /// <summary>
    /// A day number of the week that the week should start on.
    /// </summary>
    [JsonPropertyName("start_of_week")]
    public int StartOfWeek { get; set; }

    /// <summary>
    /// WordPress locale code.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Convert emoticons like :-) and :-P to graphics on display.
    /// </summary>
    [JsonPropertyName("use_smilies")]
    public bool UseSmilies { get; set; }

    /// <summary>
    /// Default category.
    /// </summary>
    [JsonPropertyName("default_category")]
    public int DefaultCategory { get; set; }

    /// <summary>
    /// Default post format.
    /// </summary>
    [JsonPropertyName("default_post_format")]
    public string? DefaultPostFormat { get; set; }

    /// <summary>
    /// Blog pages show at most.
    /// </summary>
    [JsonPropertyName("posts_per_page")]
    public int PostsPerPage { get; set; }

    /// <summary>
    /// Default Ping Status
    /// </summary>
    [JsonPropertyName("default_ping_status")]
    public OpenStatus? DefaultPingStatus { get; set; }

    /// <summary>
    /// Default Comment Status
    /// </summary>
    [JsonPropertyName("default_comment_status")]
    public OpenStatus? DefaultCommentStatus { get; set; }
}