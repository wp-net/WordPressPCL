using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the Widget Types endpoint WP REST API (<c>wp/v2/widget-types</c>).
/// </summary>
public class WidgetTypes
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "widget-types";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public WidgetTypes(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Get all registered widget types.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of widget types</returns>
    public Task<List<WidgetType>> GetAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<WidgetType>>(_methodPath, embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get a specific widget type by its ID (e.g. "block").
    /// </summary>
    /// <param name="id">Widget type ID</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested widget type</returns>
    public Task<WidgetType> GetByIdAsync(string id, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<WidgetType>($"{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }
}
