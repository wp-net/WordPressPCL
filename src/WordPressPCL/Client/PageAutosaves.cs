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
/// Client class for interaction with the Page Autosaves endpoint WP REST API
/// (<c>wp/v2/pages/{pageId}/autosaves</c>).
/// </summary>
public class PageAutosaves
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "autosaves";
    private readonly int _pageId;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    /// <param name="pageId">ID of the parent page</param>
    public PageAutosaves(HttpHelper httpHelper, int pageId)
    {
        _httpHelper = httpHelper;
        _pageId = pageId;
    }

    /// <summary>
    /// Get the available autosaves for the page.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of autosaves</returns>
    public Task<List<PostRevision>> GetAsync(bool embed = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<PostRevision>>($"pages/{_pageId}/{_methodPath}", embed, true, cancellationToken: cancellationToken);
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
        return _httpHelper.GetRequestAsync<PostRevision>($"pages/{_pageId}/{_methodPath}/{id}", embed, true, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Create an autosave for the page.
    /// </summary>
    /// <param name="page">The page to autosave</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created autosave</returns>
    public async Task<PostRevision> CreateAsync(Page page, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(page, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<PostRevision>($"pages/{_pageId}/{_methodPath}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }
}
