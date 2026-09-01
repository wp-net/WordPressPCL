using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client for the WordPress widget types endpoint (<c>wp/v2/widget-types</c>).
/// </summary>
public class WidgetTypes
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "widget-types";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">Reference to the HTTP helper used for API requests.</param>
    public WidgetTypes(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Gets all registered widget types.
    /// </summary>
    public Task<List<WidgetType>> GetAsync(
        bool embed = false,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<WidgetType>>(
            _methodPath,
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Gets a widget type by its string ID.
    /// </summary>
    public Task<WidgetType> GetByIdAsync(
        string id,
        bool embed = false,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<WidgetType>(
            $"{_methodPath}/{RestPath.EncodeSegment(id)}",
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }
}
