using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client for single global styles records and active-theme styles
/// (<c>wp/v2/global-styles</c>).
/// </summary>
public class GlobalStylesClient
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "global-styles";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">Reference to the HTTP helper used for API requests.</param>
    public GlobalStylesClient(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Gets a global styles record by its post ID.
    /// </summary>
    public Task<GlobalStyles> GetByIdAsync(
        int id,
        bool embed = false,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
        return _httpHelper.GetRequestAsync<GlobalStyles>(
            $"{_methodPath}/{id}",
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Gets the merged settings and styles for the active theme.
    /// </summary>
    /// <param name="stylesheet">
    /// Active theme stylesheet, including a parent directory when the theme is nested.
    /// </param>
    /// <param name="embed">Include embedded resources.</param>
    /// <param name="useAuth">Send the request with an authentication header.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Task<GlobalStyles> GetThemeStylesAsync(
        string stylesheet,
        bool embed = false,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        string encodedStylesheet = RestPath.EncodeSegments(stylesheet);
        return _httpHelper.GetRequestAsync<GlobalStyles>(
            $"{_methodPath}/themes/{encodedStylesheet}",
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Updates the settings, styles, or title of a global styles record.
    /// </summary>
    public async Task<GlobalStyles> UpdateAsync(
        int id,
        GlobalStyles entity,
        CancellationToken cancellationToken = default)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
        ArgumentNullException.ThrowIfNull(entity);
        JsonObject payload = JsonSerializer.SerializeToNode(
            entity,
            _httpHelper.JsonSerializerOptions)!.AsObject();
        payload.Remove("id");
        payload.Remove("_links");
        string json = payload.ToJsonString(_httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<GlobalStyles>(
            $"{_methodPath}/{id}",
            postBody,
            cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }
}
