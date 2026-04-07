using WordPressPCL.Models;

namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/blocks</c> endpoint.
/// </summary>
public class BlocksQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Limit results to those matching a string.
    /// </summary>
    [QueryText("search")]
    public string Search { get; set; } = string.Empty;

    /// <summary>
    /// Limit result set to posts assigned a specific status.
    /// </summary>
    [QueryText("status")]
    public Status Status { get; set; }

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
