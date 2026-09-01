using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using WordPressPCL.Models;

namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/blocks</c> endpoint.
/// </summary>
public class BlocksQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Current page of the collection.
    /// </summary>
    [QueryText("page")]
    public int Page { get; set; }

    /// <summary>
    /// Maximum number of items to be returned in result set.
    /// </summary>
    [QueryText("per_page")]
    public int PerPage { get; set; }

    /// <summary>
    /// Limit results to those matching a string.
    /// </summary>
    [QueryText("search")]
    public string? Search { get; set; }

    /// <summary>
    /// Limit response to posts published after a given date.
    /// </summary>
    [QueryText("after")]
    public DateTime? After { get; set; }

    /// <summary>
    /// Limit response to posts modified after a given date.
    /// </summary>
    [QueryText("modified_after")]
    public DateTime? ModifiedAfter { get; set; }

    /// <summary>
    /// Limit response to posts published before a given date.
    /// </summary>
    [QueryText("before")]
    public DateTime? Before { get; set; }

    /// <summary>
    /// Limit response to posts modified before a given date.
    /// </summary>
    [QueryText("modified_before")]
    public DateTime? ModifiedBefore { get; set; }

    /// <summary>
    /// Ensure result set excludes specific IDs.
    /// </summary>
    [QueryText("exclude")]
    public List<int>? Exclude { get; set; }

    /// <summary>
    /// Limit result set to specific IDs.
    /// </summary>
    [QueryText("include")]
    public List<int>? Include { get; set; }

    /// <summary>
    /// Offset the result set by a specific number of items.
    /// </summary>
    [QueryText("offset")]
    public int Offset { get; set; }

    /// <summary>
    /// Sort collection by post attribute.
    /// </summary>
    [QueryText("orderby")]
    public PostsOrderBy OrderBy { get; set; }

    /// <summary>
    /// Limit search to specific post columns.
    /// </summary>
    [QueryText("search_columns")]
    public List<string>? SearchColumns { get; set; }

    /// <summary>
    /// Limit result set to posts with one or more specific slugs.
    /// </summary>
    [QueryText("slug")]
    public List<string>? Slugs { get; set; }

    /// <summary>
    /// Limit result set to posts assigned one or more statuses.
    /// </summary>
    [QueryText("status")]
    public List<Status>? Statuses { get; set; }

    /// <inheritdoc />
    public override string BuildQuery()
    {
        NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);

        if (Page != default) query.Add("page", Page.ToString(CultureInfo.InvariantCulture));
        if (PerPage != default) query.Add("per_page", PerPage.ToString(CultureInfo.InvariantCulture));
        if (!string.IsNullOrEmpty(Search)) query.Add("search", Search);
        AddDate(query, "after", After);
        AddDate(query, "modified_after", ModifiedAfter);
        AddDate(query, "before", Before);
        AddDate(query, "modified_before", ModifiedBefore);
        AddList(query, "exclude", Exclude);
        AddList(query, "include", Include);
        if (Offset != default) query.Add("offset", Offset.ToString(CultureInfo.InvariantCulture));
        query.Add("orderby", OrderBy == PostsOrderBy.IncludeSlugs
            ? "include_slugs"
            : OrderBy.ToString().ToLowerInvariant());
        AddList(query, "search_columns", SearchColumns);
        AddList(query, "slug", Slugs);
        if (Statuses?.Count > 0)
        {
            query.Add("status", string.Join(",", Statuses.Select(status => status.ToString().ToLowerInvariant())));
        }
        query.Add("order", Order.ToString().ToLowerInvariant());
        if (Embed) query.Add("_embed", "true");
        query.Add("context", Context.ToString().ToLowerInvariant());

        return $"?{query}";
    }

    private static void AddDate(NameValueCollection query, string name, DateTime? value)
    {
        if (value.HasValue)
        {
            query.Add(name, value.Value.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));
        }
    }

    private static void AddList<T>(NameValueCollection query, string name, List<T>? values)
    {
        if (values?.Count > 0)
        {
            query.Add(name, string.Join(",", values));
        }
    }
}
