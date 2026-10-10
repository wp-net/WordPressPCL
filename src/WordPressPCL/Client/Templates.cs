using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client for the block templates endpoint (<c>wp/v2/templates</c>).
/// </summary>
public class Templates
{
    private readonly HttpHelper _httpHelper;
    private readonly TemplateClient<Template, TemplatesQueryBuilder> _client;
    private const string _methodPath = "templates";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">Reference to the HTTP helper used for API requests.</param>
    public Templates(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
        _client = new TemplateClient<Template, TemplatesQueryBuilder>(httpHelper, _methodPath);
    }

    /// <summary>
    /// Gets the templates available to the current user.
    /// </summary>
    public Task<List<Template>> GetAsync(
        bool embed = false,
        Context context = Context.View,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        return _client.GetAsync(embed, context, useAuth, cancellationToken);
    }

    /// <summary>
    /// Gets a template by its compound identifier, such as <c>twentytwentyfour//index</c>.
    /// </summary>
    public Task<Template> GetByIdAsync(
        string id,
        bool embed = false,
        Context context = Context.View,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        return _client.GetByIdAsync(id, embed, context, useAuth, cancellationToken);
    }

    /// <summary>
    /// Queries templates using the filters supported by WordPress core.
    /// </summary>
    public Task<List<Template>> QueryAsync(
        TemplatesQueryBuilder queryBuilder,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        return _client.QueryAsync(queryBuilder, useAuth, cancellationToken);
    }

    /// <summary>
    /// Gets the fallback template for a template hierarchy slug.
    /// An unresolved fallback is returned by WordPress as an empty template with a null ID.
    /// </summary>
    public Task<Template> GetFallbackAsync(
        string slug,
        bool? isCustom = null,
        string? templatePrefix = null,
        bool useAuth = true,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(slug);
        NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
        query.Add("slug", slug);
        if (isCustom.HasValue) query.Add("is_custom", isCustom.Value ? "true" : "false");
        if (!string.IsNullOrWhiteSpace(templatePrefix)) query.Add("template_prefix", templatePrefix);

        return _httpHelper.GetRequestAsync<Template>(
            $"{_methodPath}/lookup?{query}",
            false,
            useAuth,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Creates a template.
    /// </summary>
    public Task<Template> CreateAsync(Template entity, CancellationToken cancellationToken = default)
    {
        return _client.CreateAsync(entity, cancellationToken);
    }

    /// <summary>
    /// Updates a template identified by <see cref="TemplateEntity.Id"/>.
    /// </summary>
    public Task<Template> UpdateAsync(Template entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (string.IsNullOrWhiteSpace(entity.Id))
        {
            throw new ArgumentException("The template ID is required for an update.", nameof(entity));
        }
        return _client.UpdateAsync(entity.Id, entity, cancellationToken);
    }

    /// <summary>
    /// Trashes or permanently deletes a customized template.
    /// </summary>
    public Task<bool> DeleteAsync(
        string id,
        bool force = false,
        CancellationToken cancellationToken = default)
    {
        return _client.DeleteAsync(id, force, cancellationToken);
    }
}
