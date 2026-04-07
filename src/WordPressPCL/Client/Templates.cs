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
/// Client class for interaction with the Block Templates endpoint WP REST API (<c>wp/v2/templates</c>).
/// Templates use compound string identifiers such as <c>twentytwentyfour//index</c>.
/// </summary>
public class Templates
{
    private readonly HttpHelper _httpHelper;
    private const string _methodPath = "templates";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Templates(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Get all templates.
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of templates</returns>
    public Task<List<Template>> GetAsync(bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<Template>>(_methodPath, embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get a template by its compound string ID (e.g. "twentytwentyfour//index").
    /// </summary>
    /// <param name="id">Template ID</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The requested template</returns>
    public Task<Template> GetByIdAsync(string id, bool embed = false, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<Template>($"{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Execute a parametrized query.
    /// </summary>
    /// <param name="queryBuilder">Query builder with specific parameters</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching templates</returns>
    public Task<List<Template>> QueryAsync(TemplatesQueryBuilder queryBuilder, bool useAuth = true, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<Template>>($"{_methodPath}{queryBuilder.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Create a new template.
    /// </summary>
    /// <param name="entity">Template object to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created template</returns>
    public async Task<Template> CreateAsync(Template entity, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<Template>(_methodPath, postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Update an existing template.
    /// </summary>
    /// <param name="entity">Template object with updated values</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated template</returns>
    public async Task<Template> UpdateAsync(Template entity, CancellationToken cancellationToken = default)
    {
        string json = JsonSerializer.Serialize(entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<Template>($"{_methodPath}/{entity.Id}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Delete a template by its compound string ID.
    /// </summary>
    /// <param name="id">Template ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successfully deleted</returns>
    public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return _httpHelper.DeleteRequestAsync($"{_methodPath}/{id}?force=true", cancellationToken: cancellationToken);
    }
}
