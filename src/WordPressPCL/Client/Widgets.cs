using System;
using System.Collections.Generic;
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
/// Client for the WordPress widgets endpoint (<c>wp/v2/widgets</c>).
/// </summary>
public class Widgets
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "widgets";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">Reference to the HTTP helper used for API requests.</param>
    public Widgets(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Gets all publicly visible widgets.
    /// </summary>
    public Task<List<Widget>> GetAsync(
        bool embed = false,
        bool useAuth = false,
        CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<Widget>>(
            _methodPath,
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Gets publicly visible widgets assigned to a sidebar.
    /// </summary>
    public Task<List<Widget>> GetBySidebarAsync(
        string sidebarId,
        bool embed = false,
        bool useAuth = false,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sidebarId);
        string query = new WidgetsQueryBuilder { Sidebar = sidebarId }.BuildQuery();
        return _httpHelper.GetRequestAsync<List<Widget>>(
            $"{_methodPath}{query}",
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Gets a publicly visible widget by its string ID.
    /// </summary>
    public Task<Widget> GetByIdAsync(
        string id,
        bool embed = false,
        bool useAuth = false,
        CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<Widget>(
            $"{_methodPath}/{RestPath.EncodeSegment(id)}",
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Gets publicly visible widgets matching a core-supported collection query.
    /// </summary>
    public Task<List<Widget>> QueryAsync(
        WidgetsQueryBuilder queryBuilder,
        bool useAuth = false,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queryBuilder);
        return _httpHelper.GetRequestAsync<List<Widget>>(
            $"{_methodPath}{queryBuilder.BuildQuery()}",
            false,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Creates a widget. WordPress generates the widget ID and defaults an omitted sidebar to
    /// <c>wp_inactive_widgets</c>.
    /// </summary>
    public async Task<Widget> CreateAsync(
        Widget entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentException.ThrowIfNullOrWhiteSpace(entity.IdBase);
        if (entity.Sidebar is not null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(entity.Sidebar);
        }

        string json = SerializeWritableFields(entity, includeIdBase: true);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<Widget>(
            _methodPath,
            postBody,
            cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Updates a widget identified by <see cref="Widget.Id"/>.
    /// </summary>
    public async Task<Widget> UpdateAsync(
        Widget entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        string id = RestPath.EncodeSegment(entity.Id!);
        string json = SerializeWritableFields(entity, includeIdBase: false);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<Widget>(
            $"{_methodPath}/{id}",
            postBody,
            cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Deletes a widget or moves it to the inactive sidebar.
    /// </summary>
    /// <param name="id">Widget ID.</param>
    /// <param name="force">
    /// <see langword="true"/> to permanently remove the widget; <see langword="false"/>
    /// to move it to the inactive sidebar.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public Task<bool> DeleteAsync(
        string id,
        bool force = false,
        CancellationToken cancellationToken = default)
    {
        string route = $"{_methodPath}/{RestPath.EncodeSegment(id)}"
            .SetQueryParam(nameof(force), force.ToString().ToLowerInvariant());
        return _httpHelper.DeleteRequestAsync(route, cancellationToken: cancellationToken);
    }

    private string SerializeWritableFields(Widget entity, bool includeIdBase)
    {
        JsonObject payload = JsonSerializer.SerializeToNode(
            entity,
            _httpHelper.JsonSerializerOptions)!.AsObject();
        payload.Remove("id");
        payload.Remove("rendered");
        payload.Remove("rendered_form");
        payload.Remove("_links");
        if (!includeIdBase)
        {
            payload.Remove("id_base");
        }

        return payload.ToJsonString(_httpHelper.JsonSerializerOptions);
    }
}
