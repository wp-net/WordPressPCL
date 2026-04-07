namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/widgets</c> endpoint.
/// </summary>
public class WidgetsQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Limit results to widgets assigned to a specific sidebar.
    /// </summary>
    [QueryText("sidebar")]
    public string Sidebar { get; set; } = string.Empty;
}
