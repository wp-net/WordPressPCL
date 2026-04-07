namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/templates</c> endpoint.
/// </summary>
public class TemplatesQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Limit to templates for a specific theme (stylesheet).
    /// </summary>
    [QueryText("theme")]
    public string Theme { get; set; } = string.Empty;

    /// <summary>
    /// Limit to templates of a specific type.
    /// </summary>
    [QueryText("type")]
    public string Type { get; set; } = string.Empty;

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
