using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the Block Types endpoint WP REST API (<c>wp/v2/block-types</c>).
/// </summary>
public class BlockTypes
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "block-types";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public BlockTypes(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Get all registered block types.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of block types</returns>
    public Task<List<BlockType>> GetAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<BlockType>>(_methodPath, embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get a specific block type by its namespaced name (e.g. "core/paragraph").
    /// </summary>
    /// <param name="namespace">Block namespace (e.g. "core")</param>
    /// <param name="name">Block name (e.g. "paragraph")</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested block type</returns>
    public Task<BlockType> GetByNameAsync(string @namespace, string name, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<BlockType>($"{_methodPath}/{@namespace}/{name}", embed, useAuth, cancellationToken: cancellationToken);
    }
}
