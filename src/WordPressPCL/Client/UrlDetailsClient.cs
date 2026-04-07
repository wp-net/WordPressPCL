using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the URL Details endpoint WP REST API (<c>wp/v2/url-details</c>).
/// This endpoint returns metadata about an external URL, including its title, description and Open Graph data.
/// Requires authentication.
/// </summary>
public class UrlDetailsClient
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "url-details";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public UrlDetailsClient(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Retrieve metadata for a given URL.
    /// </summary>
    /// <param name="url">The URL to retrieve details for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>URL metadata including title, description and Open Graph data</returns>
    public Task<Models.UrlDetails> GetAsync(string url, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA1507 // "url" is a query parameter name, not the method parameter
        return _httpHelper.GetRequestAsync<Models.UrlDetails>(_methodPath.SetQueryParam("url", url), false, true, cancellationToken: cancellationToken);
#pragma warning restore CA1507
    }
}
