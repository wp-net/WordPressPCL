using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the Post Autosaves endpoint WP REST API
/// (<c>wp/v2/posts/{postId}/autosaves</c>).
/// </summary>
public class PostAutosaves
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "autosaves";
    private readonly int _postId;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    /// <param name="postId">ID of the parent post</param>
    public PostAutosaves(HttpHelper httpHelper, int postId)
    {
        _httpHelper = httpHelper;
        _postId = postId;
    }

    /// <summary>
    /// Get the available autosaves for the post.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of autosaves</returns>
    public Task<List<PostRevision>> GetAsync(bool embed = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<PostRevision>>($"posts/{_postId}/{_methodPath}", embed, true, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get a specific autosave by ID.
    /// </summary>
    /// <param name="id">Autosave ID</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested autosave</returns>
    public Task<PostRevision> GetByIdAsync(int id, bool embed = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<PostRevision>($"posts/{_postId}/{_methodPath}/{id}", embed, true, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Create an autosave for the post.
    /// </summary>
    /// <param name="post">The post to autosave</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created autosave</returns>
    public async Task<PostRevision> CreateAsync(Post post, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(post, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<PostRevision>($"posts/{_postId}/{_methodPath}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }
}
