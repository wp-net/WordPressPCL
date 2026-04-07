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
/// Client class for interaction with the Widgets endpoint WP REST API (<c>wp/v2/widgets</c>).
/// Widgets use string identifiers such as <c>block-3</c>.
/// </summary>
public class Widgets
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "widgets";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Widgets(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Get all widgets.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of widgets</returns>
    public Task<List<Widget>> GetAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<Widget>>(_methodPath, embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get widgets assigned to a specific sidebar.
    /// </summary>
    /// <param name="sidebarId">Sidebar ID</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of widgets in the sidebar</returns>
    public Task<List<Widget>> GetBySidebarAsync(string sidebarId, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<Widget>>(_methodPath.SetQueryParam("sidebar", sidebarId), embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get a specific widget by its string ID (e.g. "block-3").
    /// </summary>
    /// <param name="id">Widget ID</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested widget</returns>
    public Task<Widget> GetByIdAsync(string id, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<Widget>($"{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Execute a parametrized query.
    /// </summary>
    /// <param name="queryBuilder">Query builder with specific parameters</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching widgets</returns>
    public Task<List<Widget>> QueryAsync(WidgetsQueryBuilder queryBuilder, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<Widget>>($"{_methodPath}{queryBuilder.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Create a new widget.
    /// </summary>
    /// <param name="entity">Widget object to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created widget</returns>
    public async Task<Widget> CreateAsync(Widget entity, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<Widget>(_methodPath, postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Update an existing widget.
    /// </summary>
    /// <param name="entity">Widget object with updated values</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated widget</returns>
    public async Task<Widget> UpdateAsync(Widget entity, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<Widget>($"{_methodPath}/{entity.Id}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Delete a widget by its string ID, optionally removing it from the sidebar.
    /// </summary>
    /// <param name="id">Widget ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successfully deleted</returns>
    public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return _httpHelper.DeleteRequestAsync($"{_methodPath}/{id}?force=true", cancellationToken: cancellationToken);
    }
}
