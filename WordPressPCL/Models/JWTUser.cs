using System.Text.Json.Serialization;


namespace WordPressPCL.Models;

/// <summary>
/// User class for working with the JWT plugin
/// </summary>
public class JWTUser
{
    /// <summary>
    /// The JWT Token used to authorize requests
    /// </summary>
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    /// <summary>
    /// User Display Name
    /// </summary>
    [JsonPropertyName("user_display_name")]
    public string? DisplayName { get; set; }

    /// <summary>
    /// User Email Address
    /// </summary>
    [JsonPropertyName("user_email")]
    public string? Email { get; set; }

    /// <summary>
    /// User Nice Name
    /// </summary>
    [JsonPropertyName("user_nicename")]
    public string? NiceName { get; set; }
}
