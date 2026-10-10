using System.Collections.Specialized;
using System.Web;

namespace WordPressPCL.Utility;

/// <summary>
/// Query builder for the <c>wp/v2/block-types</c> endpoint.
/// </summary>
public class BlockTypesQueryBuilder : QueryBuilder
{
    /// <summary>
    /// Limit results to block types in a namespace.
    /// </summary>
    [QueryText("namespace")]
    public string? Namespace { get; set; }

    /// <inheritdoc />
    public override string BuildQuery()
    {
        NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
        query.Add("context", Context.ToString().ToLowerInvariant());
        if (!string.IsNullOrWhiteSpace(Namespace)) query.Add("namespace", Namespace);
        if (Embed) query.Add("_embed", "true");
        return $"?{query}";
    }
}
