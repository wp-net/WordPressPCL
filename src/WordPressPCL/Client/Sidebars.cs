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
/// Client for the WordPress sidebars endpoint (<c>wp/v2/sidebars</c>).
/// </summary>
public class Sidebars
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "sidebars";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">Reference to the HTTP helper used for API requests.</param>
    public Sidebars(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Gets all publicly visible registered sidebars.
    /// </summary>
    public Task<List<Sidebar>> GetAsync(
        bool embed = false,
        bool useAuth = false,
        CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<Sidebar>>(
            _methodPath,
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Gets a publicly visible sidebar by its string ID.
    /// </summary>
    public Task<Sidebar> GetByIdAsync(
        string id,
        bool embed = false,
        bool useAuth = false,
        CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<Sidebar>(
            $"{_methodPath}/{RestPath.EncodeSegment(id)}",
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Replaces the ordered widget assignment for a sidebar.
    /// </summary>
    public async Task<Sidebar> UpdateAsync(
        Sidebar entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        string id = RestPath.EncodeSegment(entity.Id!);
        JsonObject payload = [];
        if (entity.Widgets is not null)
        {
            payload["widgets"] = JsonSerializer.SerializeToNode(
                entity.Widgets,
                _httpHelper.JsonSerializerOptions);
        }

        using StringContent postBody = new(
            payload.ToJsonString(_httpHelper.JsonSerializerOptions),
            Encoding.UTF8,
            "application/json");
        return (await _httpHelper.PostRequestAsync<Sidebar>(
            $"{_methodPath}/{id}",
            postBody,
            cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }
}
