namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/templates</c> endpoint.
/// </summary>
/// <remarks>
/// WordPress core does not support the inherited <see cref="QueryBuilder.Order"/> option for this endpoint.
/// </remarks>
public class TemplatesQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Limit results to a specific customized template post ID.
    /// </summary>
    [QueryText("wp_id")]
    public int WpId { get; set; }

    /// <summary>
    /// Post type for which to resolve templates.
    /// </summary>
    [QueryText("post_type")]
    public string? PostType { get; set; }

    /// <inheritdoc />
    public override string BuildQuery()
    {
        return TemplateQueryString.Build(WpId, PostType, null, Context, Embed);
    }
}
