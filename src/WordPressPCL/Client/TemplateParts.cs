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
/// Client class for interaction with the Block Template Parts endpoint WP REST API (<c>wp/v2/template-parts</c>).
/// Template parts use compound string identifiers such as <c>twentytwentyfour//header</c>.
/// </summary>
public class TemplateParts
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "template-parts";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public TemplateParts(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Get all template parts.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of template parts</returns>
    public Task<List<TemplatePart>> GetAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<TemplatePart>>(_methodPath, embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get a template part by its compound string ID (e.g. "twentytwentyfour//header").
    /// </summary>
    /// <param name="id">Template part ID</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested template part</returns>
    public Task<TemplatePart> GetByIdAsync(string id, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<TemplatePart>($"{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Execute a parametrized query.
    /// </summary>
    /// <param name="queryBuilder">Query builder with specific parameters</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching template parts</returns>
    public Task<List<TemplatePart>> QueryAsync(TemplatePartsQueryBuilder queryBuilder, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<TemplatePart>>($"{_methodPath}{queryBuilder.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Create a new template part.
    /// </summary>
    /// <param name="entity">Template part object to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created template part</returns>
    public async Task<TemplatePart> CreateAsync(TemplatePart entity, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<TemplatePart>(_methodPath, postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Update an existing template part.
    /// </summary>
    /// <param name="entity">Template part object with updated values</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated template part</returns>
    public async Task<TemplatePart> UpdateAsync(TemplatePart entity, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<TemplatePart>($"{_methodPath}/{entity.Id}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Delete a template part by its compound string ID.
    /// </summary>
    /// <param name="id">Template part ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successfully deleted</returns>
    public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return _httpHelper.DeleteRequestAsync($"{_methodPath}/{id}?force=true", cancellationToken: cancellationToken);
    }
}
