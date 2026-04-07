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
    public string Search { get; set; } = string.Empty;

    /// <summary>
    /// Limit results to items of an object type.
    /// One of: post, term, post-format.
    /// </summary>
    [QueryText("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Limit results to items of one or more object subtypes.
    /// </summary>
    [QueryText("subtype")]
    public string Subtype { get; set; } = string.Empty;

    /// <summary>
    /// Ensure result set excludes specific IDs.
    /// </summary>
    [QueryText("exclude")]
    public int Exclude { get; set; }

    /// <summary>
    /// Limit result set to specific IDs.
    /// </summary>
    [QueryText("include")]
    public int Include { get; set; }

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
}
