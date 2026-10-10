using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client for the block template parts endpoint (<c>wp/v2/template-parts</c>).
/// </summary>
public class TemplateParts
{
    private readonly TemplateClient<TemplatePart, TemplatePartsQueryBuilder> _client;
    private const string _methodPath = "template-parts";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">Reference to the HTTP helper used for API requests.</param>
    public TemplateParts(HttpHelper httpHelper)
    {
        _client = new TemplateClient<TemplatePart, TemplatePartsQueryBuilder>(httpHelper, _methodPath);
    }

    /// <summary>
    /// Gets the template parts available to the current user.
    /// </summary>
    public Task<List<TemplatePart>> GetAsync(
        bool embed = false,
        Context context = Context.View,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        return _client.GetAsync(embed, context, useAuth, cancellationToken);
    }

    /// <summary>
    /// Gets a template part by its compound identifier, such as <c>twentytwentyfour//header</c>.
    /// </summary>
    public Task<TemplatePart> GetByIdAsync(
        string id,
        bool embed = false,
        Context context = Context.View,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        return _client.GetByIdAsync(id, embed, context, useAuth, cancellationToken);
    }

    /// <summary>
    /// Queries template parts using the filters supported by WordPress core.
    /// </summary>
    public Task<List<TemplatePart>> QueryAsync(
        TemplatePartsQueryBuilder queryBuilder,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        return _client.QueryAsync(queryBuilder, useAuth, cancellationToken);
    }

    /// <summary>
    /// Creates a template part.
    /// </summary>
    public Task<TemplatePart> CreateAsync(
        TemplatePart entity,
        CancellationToken cancellationToken = default)
    {
        return _client.CreateAsync(entity, cancellationToken);
    }

    /// <summary>
    /// Updates a template part identified by <see cref="TemplateEntity.Id"/>.
    /// </summary>
    public Task<TemplatePart> UpdateAsync(
        TemplatePart entity,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (string.IsNullOrWhiteSpace(entity.Id))
        {
            throw new ArgumentException("The template part ID is required for an update.", nameof(entity));
        }
        return _client.UpdateAsync(entity.Id, entity, cancellationToken);
    }

    /// <summary>
    /// Trashes or permanently deletes a customized template part.
    /// </summary>
    public Task<bool> DeleteAsync(
        string id,
        bool force = false,
        CancellationToken cancellationToken = default)
    {
        return _client.DeleteAsync(id, force, cancellationToken);
    }
}
