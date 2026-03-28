using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with Post revisions endpoint WP REST API
/// </summary>
public class PostRevisions : IReadOperation<PostRevision>, IDeleteOperation
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "revisions";
    private readonly int _postId;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    /// <param name="postId">ID of post</param>
    public PostRevisions(HttpHelper httpHelper, int postId)
    {
        _httpHelper = httpHelper;
        _postId = postId;
    }
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="ID">Entity Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of operation</returns>
    public Task<bool> DeleteAsync(int ID, CancellationToken cancellationToken = default)
    {
        return _httpHelper.DeleteRequestAsync($"posts/{_postId}/{_methodPath}/{ID}?force=true", cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get latest
    /// </summary>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Latest PostRevisions</returns>
    public Task<List<PostRevision>> GetAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<PostRevision>>($"posts/{_postId}/{_methodPath}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get All
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all result</returns>
    public Task<List<PostRevision>> GetAllAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<PostRevision>>($"posts/{_postId}/{_methodPath}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get Entity by Id
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Entity by Id</returns>
    public Task<PostRevision> GetByIdAsync(object id, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<PostRevision>($"posts/{_postId}/{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }
}
