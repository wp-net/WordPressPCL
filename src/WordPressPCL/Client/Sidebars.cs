using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the Sidebars endpoint WP REST API (<c>wp/v2/sidebars</c>).
/// Sidebars support read and update operations only; they cannot be created or deleted via the API.
/// </summary>
public class Sidebars
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "sidebars";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Sidebars(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Get all registered sidebars.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of sidebars</returns>
    public Task<List<Sidebar>> GetAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<Sidebar>>(_methodPath, embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get a specific sidebar by its string ID (e.g. "sidebar-1").
    /// </summary>
    /// <param name="id">Sidebar ID</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested sidebar</returns>
    public Task<Sidebar> GetByIdAsync(string id, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<Sidebar>($"{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Update an existing sidebar (e.g. to re-assign its widgets).
    /// </summary>
    /// <param name="entity">Sidebar object with updated values</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated sidebar</returns>
    public async Task<Sidebar> UpdateAsync(Sidebar entity, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<Sidebar>($"{_methodPath}/{entity.Id}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }
}
