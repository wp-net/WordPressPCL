using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;

namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/search</c> endpoint.
/// </summary>
public class SearchQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Limit results to those matching a string.
    /// </summary>
    [QueryText("search")]
    public string? Search { get; set; }

    /// <summary>
    /// Limit results to items of an object type.
    /// One of: post, term, post-format.
    /// </summary>
    [QueryText("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Limit results to items of one or more object subtypes.
    /// </summary>
    [QueryText("subtype")]
    public List<string>? Subtype { get; set; }

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
    /// Current page of the collection.
    /// </summary>
    [QueryText("page")]
    public int Page { get; set; }

    /// <summary>
    /// Maximum number of items to be returned in result set.
    /// </summary>
    [QueryText("per_page")]
    public int PerPage { get; set; }

    /// <inheritdoc />
    public override string BuildQuery()
    {
        NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);

        if (!string.IsNullOrEmpty(Search)) query.Add("search", Search);
        if (!string.IsNullOrEmpty(Type)) query.Add("type", Type);
        if (Subtype?.Count > 0) query.Add("subtype", string.Join(",", Subtype));
        if (Exclude?.Count > 0) query.Add("exclude", string.Join(",", Exclude));
        if (Include?.Count > 0) query.Add("include", string.Join(",", Include));
        if (Page != default) query.Add("page", Page.ToString(CultureInfo.InvariantCulture));
        if (PerPage != default) query.Add("per_page", PerPage.ToString(CultureInfo.InvariantCulture));

        query.Add("order", Order.ToString().ToLowerInvariant());
        if (Embed) query.Add("_embed", "true");
        query.Add("context", Context.ToString().ToLowerInvariant());

        return $"?{query}";
    }
}
