using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using WordPressPCL.Models;

namespace WordPressPCL.Utility;

internal static class TemplateQueryString
{
    public static string Build(
        int wpId,
        string? postType,
        string? area,
        Context context,
        bool embed)
    {
        NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
        query.Add("context", context.ToString().ToLowerInvariant());
        if (wpId > 0) query.Add("wp_id", wpId.ToString(CultureInfo.InvariantCulture));
        if (!string.IsNullOrWhiteSpace(postType)) query.Add("post_type", postType);
        if (!string.IsNullOrWhiteSpace(area)) query.Add("area", area);
        if (embed) query.Add("_embed", "true");
        return $"?{query}";
    }
}
