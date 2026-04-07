using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the Page Revisions endpoint WP REST API
/// (<c>wp/v2/pages/{pageId}/revisions</c>).
/// </summary>
public class PageRevisions : IReadOperation<PostRevision>, IDeleteOperation
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "revisions";
    private readonly int _pageId;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    /// <param name="pageId">ID of the parent page</param>
    public PageRevisions(HttpHelper httpHelper, int pageId)
    {
        _httpHelper = httpHelper;
        _pageId = pageId;
    }

    /// <summary>
    /// Delete a revision by ID.
    /// </summary>
    /// <param name="ID">Revision Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of operation</returns>
    public Task<bool> DeleteAsync(int ID, CancellationToken cancellationToken = default)
    {
        return _httpHelper.DeleteRequestAsync($"pages/{_pageId}/{_methodPath}/{ID}?force=true", cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get the most recent revisions.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of page revisions</returns>
    public Task<List<PostRevision>> GetAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<PostRevision>>($"pages/{_pageId}/{_methodPath}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get all revisions.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all page revisions</returns>
    public Task<List<PostRevision>> GetAllAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return GetAsync(embed, useAuth, cancellationToken);
    }

    /// <summary>
    /// Get a revision by ID.
    /// </summary>
    /// <param name="id">Revision ID</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested page revision</returns>
    public Task<PostRevision> GetByIdAsync(object id, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<PostRevision>($"pages/{_pageId}/{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }
}
