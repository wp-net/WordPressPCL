using System;
using System.Collections.Generic;
using WordPressPCL.Models;

namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/widgets</c> endpoint.
/// </summary>
public class WidgetsQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Limits results to widgets assigned to this sidebar.
    /// </summary>
    public string? Sidebar { get; set; }

    /// <inheritdoc />
    public override string BuildQuery()
    {
        List<string> parameters = [];
        if (Sidebar is not null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(Sidebar);
            parameters.Add($"sidebar={Uri.EscapeDataString(Sidebar)}");
        }

        if (Context != Context.View)
        {
            parameters.Add($"context={Context.ToString().ToLowerInvariant()}");
        }

        if (Embed)
        {
            parameters.Add("_embed=true");
        }

        return parameters.Count == 0 ? string.Empty : $"?{string.Join("&", parameters)}";
    }
}
