using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the Search endpoint WP REST API (<c>wp/v2/search</c>).
/// </summary>
public class Search
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "search";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Search(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Search for content matching a given term.
    /// </summary>
    /// <param name="searchTerm">Text to search for</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching search results</returns>
    public Task<List<SearchResult>> SearchAsync(string searchTerm, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<SearchResult>>(_methodPath.SetQueryParam("search", searchTerm), false, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Execute a parametrized search query.
    /// </summary>
    /// <param name="queryBuilder">Query builder with specific parameters</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching search results</returns>
    public Task<List<SearchResult>> QueryAsync(SearchQueryBuilder queryBuilder, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<SearchResult>>($"{_methodPath}{queryBuilder.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken);
    }
}
