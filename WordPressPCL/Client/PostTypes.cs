using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with Post Types endpoint WP REST API
/// </summary>
public class PostTypes : IReadOperation<PostType>
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "types";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public PostTypes(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Get latest
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of latest PostTypes</returns>
    public async Task<List<PostType>> GetAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        List<PostType> entities = new();
        Dictionary<string, PostType> entities_page = (await _httpHelper.GetRequestAsync<Dictionary<string, PostType>>($"{_methodPath}", embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false));
        foreach (KeyValuePair<string, PostType> ent in entities_page)
        {
            entities.Add(ent.Value);
        }
        return entities;
    }

    /// <summary>
    /// Get All
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all PostTypes</returns>
    public async Task<List<PostType>> GetAllAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
        List<PostType> entities = new();
        Dictionary<string, PostType> entities_page = (await _httpHelper.GetRequestAsync<Dictionary<string, PostType>>($"{_methodPath}", embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false));
        foreach (KeyValuePair<string, PostType> ent in entities_page)
        {
            entities.Add(ent.Value);
        }
        return entities;
    }

    /// <summary>
    /// Get Entity by Id
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Entity by Id</returns>
    public Task<PostType> GetByIdAsync(object id, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<PostType>($"{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }
}