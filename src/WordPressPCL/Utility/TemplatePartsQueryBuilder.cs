namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/template-parts</c> endpoint.
/// </summary>
public class TemplatePartsQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Limit to template parts for a specific theme (stylesheet).
    /// </summary>
    [QueryText("theme")]
    public string Theme { get; set; } = string.Empty;

    /// <summary>
    /// Limit to template parts assigned to a specific area (e.g. "header", "footer").
    /// </summary>
    [QueryText("area")]
    public string Area { get; set; } = string.Empty;

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
