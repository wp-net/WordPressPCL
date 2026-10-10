using System;
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
    public Task<List<BlockType>> GetAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return QueryAsync(new BlockTypesQueryBuilder { Embed = embed }, useAuth, cancellationToken);
    }

    /// <summary>
    /// Query registered block types.
    /// </summary>
    /// <param name="queryBuilder">Query builder with the namespace and response context</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching block types</returns>
    public Task<List<BlockType>> QueryAsync(BlockTypesQueryBuilder queryBuilder, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queryBuilder);
        return _httpHelper.GetRequestAsync<List<BlockType>>($"{_methodPath}{queryBuilder.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get all registered block types in a namespace.
    /// </summary>
    /// <param name="namespace">Block namespace, for example <c>core</c></param>
    /// <param name="context">Scope under which the request is made</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of block types in the namespace</returns>
    public Task<List<BlockType>> GetByNamespaceAsync(string @namespace, Context context = Context.View, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        string path = $"{_methodPath}/{Uri.EscapeDataString(@namespace)}"
            .SetQueryParam(nameof(context), context.ToString().ToLowerInvariant());
        return _httpHelper.GetRequestAsync<List<BlockType>>(path, false, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get a specific block type by its namespaced name.
    /// </summary>
    /// <param name="namespace">Block namespace, for example <c>core</c></param>
    /// <param name="name">Block name, for example <c>paragraph</c></param>
    /// <param name="context">Scope under which the request is made</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested block type</returns>
    public Task<BlockType> GetByNameAsync(string @namespace, string name, Context context = Context.View, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        string path = $"{_methodPath}/{Uri.EscapeDataString(@namespace)}/{Uri.EscapeDataString(name)}"
            .SetQueryParam(nameof(context), context.ToString().ToLowerInvariant());
        return _httpHelper.GetRequestAsync<BlockType>(path, false, useAuth, cancellationToken: cancellationToken);
    }
}
