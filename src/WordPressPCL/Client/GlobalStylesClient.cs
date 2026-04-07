using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the Global Styles endpoint WP REST API (<c>wp/v2/global-styles/{id}</c>).
/// Global styles objects are identified by an integer ID obtained from the site's API index or theme endpoint.
/// </summary>
public class GlobalStylesClient
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "global-styles";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public GlobalStylesClient(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Get the global styles object for a given ID.
    /// </summary>
    /// <param name="id">The global styles post ID</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The global styles object</returns>
    public Task<Models.GlobalStyles> GetByIdAsync(int id, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<Models.GlobalStyles>($"{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get the global styles for a specific theme by its stylesheet slug.
    /// </summary>
    /// <param name="stylesheet">The theme stylesheet slug (e.g. "twentytwentyfour")</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The theme's global styles object</returns>
    public Task<Models.GlobalStyles> GetThemeStylesAsync(string stylesheet, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<Models.GlobalStyles>($"{_methodPath}/themes/{stylesheet}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Update an existing global styles object.
    /// </summary>
    /// <param name="entity">Global styles object with updated values</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated global styles object</returns>
    public async Task<Models.GlobalStyles> UpdateAsync(Models.GlobalStyles entity, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<Models.GlobalStyles>($"{_methodPath}/{entity.Id}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }
}
