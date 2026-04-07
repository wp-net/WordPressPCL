using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with Pages endpoint WP REST API
/// </summary>
public class Pages : CRUDOperation<Page, PagesQueryBuilder>
{
    #region Init

    private const string _methodPath = "pages";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Pages(HttpHelper HttpHelper) : base(HttpHelper, _methodPath)
    {
    }

    #endregion Init

    #region Custom

    /// <summary>
    /// Get pages by its author
    /// </summary>
    /// <param name="authorId">Author id</param>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of pages</returns>
    public Task<List<Page>> GetPagesByAuthorAsync(int authorId, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        // default values
        // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
        return HttpHelper.GetRequestAsync<List<Page>>(_methodPath.SetQueryParam("author", authorId), embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get pages by search term
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of pages</returns>
    public Task<List<Page>> GetPagesBySearchAsync(string searchTerm, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        // default values
        // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
        return HttpHelper.GetRequestAsync<List<Page>>(_methodPath.SetQueryParam("search", searchTerm), embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Delete page with force deletion
    /// </summary>
    /// <param name="ID">Page id</param>
    /// <param name="force">force deletion</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of operation</returns>
    public Task<bool> DeleteAsync(int ID, bool force = false, CancellationToken cancellationToken = default)
    {
        return HttpHelper.DeleteRequestAsync($"{_methodPath}/{ID}?force={force.ToString().ToLower(CultureInfo.InvariantCulture)}", cancellationToken: cancellationToken);
    }

    #endregion Custom
}
