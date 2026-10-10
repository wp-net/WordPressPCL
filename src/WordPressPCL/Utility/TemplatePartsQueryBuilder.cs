namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/template-parts</c> endpoint.
/// </summary>
/// <remarks>
/// WordPress core does not support the inherited <see cref="QueryBuilder.Order"/> option for this endpoint.
/// </remarks>
public class TemplatePartsQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Limit results to a specific customized template part post ID.
    /// </summary>
    [QueryText("wp_id")]
    public int WpId { get; set; }

    /// <summary>
    /// Limit results to a template part area, such as header or footer.
    /// </summary>
    [QueryText("area")]
    public string? Area { get; set; }

    /// <summary>
    /// Post type for which to resolve template parts.
    /// </summary>
    [QueryText("post_type")]
    public string? PostType { get; set; }

    /// <inheritdoc />
    public override string BuildQuery()
    {
        return TemplateQueryString.Build(WpId, PostType, Area, Context, Embed);
    }
}
