using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Base class for CRUDOperation with default implementation of all necessary operations
/// </summary>
/// <typeparam name="TClass">DTO class</typeparam>
/// <typeparam name="QClass">QueryBuilder class</typeparam>
public abstract class CRUDOperation<TClass, QClass> : IReadOperation<TClass>, IUpdateOperation<TClass>, ICreateOperation<TClass>, IDeleteOperation, IQueryOperation<TClass, QClass> where TClass : class where QClass : QueryBuilder
{
    /// <summary>
    /// path to endpoint EX. posts
    /// </summary>
    protected string MethodPath { get; }

    /// <summary>
    /// Helper for HTTP requests
    /// </summary>
    protected internal HttpHelper _httpHelper = null!;

    /// <summary>
    /// Helper for HTTP requests
    /// </summary>
    protected HttpHelper HttpHelper
    {
        get => _httpHelper;
        private set => _httpHelper = value;
    }

    /// <summary>
    /// Is object must be force deleted
    /// </summary>
    protected bool ForceDeletion { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    /// <param name="methodPath">path to endpoint, EX. posts</param>
    /// <param name="forceDeletion">is objects must be force deleted</param>
    protected CRUDOperation(HttpHelper httpHelper, string methodPath, bool forceDeletion = false)
    {
        HttpHelper = httpHelper;
        MethodPath = methodPath;
        ForceDeletion = forceDeletion;
    }

    /// <summary>
    /// Create Entity
    /// </summary>
    /// <param name="Entity">Entity object</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created object</returns>
    public async Task<TClass> CreateAsync(TClass Entity, CancellationToken cancellationToken = default)
    {
        string entity = JsonSerializer.Serialize(Entity, HttpHelper.JsonSerializerOptions);
        using StringContent postBody = new(entity, Encoding.UTF8, "application/json");
        return (await HttpHelper.PostRequestAsync<TClass>(MethodPath, postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param name="Id">Entity Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of operation</returns>
    public Task<bool> DeleteAsync(int Id, CancellationToken cancellationToken = default)
    {
        string path = $"{MethodPath}/{Id}".SetQueryParam("force", ForceDeletion.ToString().ToLower(CultureInfo.InvariantCulture))!;
        return HttpHelper.DeleteRequestAsync(path, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get latest
    /// </summary>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Entity by Id</returns>
    public Task<List<TClass>> GetAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return HttpHelper.GetRequestAsync<List<TClass>>(MethodPath, embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get All
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all result</returns>
    public async Task<List<TClass>> GetAllAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
        string url = MethodPath.SetQueryParam("per_page", "100")!.SetQueryParam("page", "1")!;
        (List<TClass>? entities, System.Net.Http.Headers.HttpResponseHeaders? headers) = await HttpHelper.GetRequestWithHeadersAsync<List<TClass>>(url, embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false);
        (int _, int totalPages) = HttpHelper.ParsePaginationHeaders(headers);
        for (int page = 2; page <= totalPages; page++)
        {
            url = MethodPath.SetQueryParam("per_page", "100")!.SetQueryParam("page", page.ToString(CultureInfo.InvariantCulture))!;
            entities.AddRange(await HttpHelper.GetRequestAsync<List<TClass>>(url, embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false));
        }
        return entities;
    }

    /// <summary>
    /// Get a single page of results with pagination metadata.
    /// </summary>
    /// <param name="page">Page number (1-based). Default: 1</param>
    /// <param name="perPage">Items per page (1–100). Default: 10</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// A <see cref="PagedResult{TClass}"/> containing the items for the requested page
    /// plus the <c>X-WP-Total</c> and <c>X-WP-TotalPages</c> metadata.
    /// </returns>
    public async Task<PagedResult<TClass>> GetPagedAsync(int page = 1, int perPage = 10, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
#pragma warning disable CA1507 // Use nameof to express symbol names
        string url = MethodPath.SetQueryParam("per_page", perPage.ToString(CultureInfo.InvariantCulture))!
                               .SetQueryParam("page", page.ToString(CultureInfo.InvariantCulture))!;
#pragma warning restore CA1507
        (List<TClass>? items, System.Net.Http.Headers.HttpResponseHeaders? headers) = await HttpHelper.GetRequestWithHeadersAsync<List<TClass>>(url, embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false);
        (int total, int totalPages) = HttpHelper.ParsePaginationHeaders(headers);
        return new PagedResult<TClass>(items, total, totalPages);
    }

    /// <summary>
    /// Execute a parametrized query and return results with pagination metadata.
    /// </summary>
    /// <param name="queryBuilder">Query builder with specific parameters</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// A <see cref="PagedResult{TClass}"/> containing the matching items plus
    /// <c>X-WP-Total</c> and <c>X-WP-TotalPages</c> metadata.
    /// </returns>
    public async Task<PagedResult<TClass>> QueryPagedAsync(QClass queryBuilder, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        (List<TClass>? items, System.Net.Http.Headers.HttpResponseHeaders? headers) = await HttpHelper.GetRequestWithHeadersAsync<List<TClass>>($"{MethodPath}{queryBuilder.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false);
        (int total, int totalPages) = HttpHelper.ParsePaginationHeaders(headers);
        return new PagedResult<TClass>(items, total, totalPages);
    }

    /// <summary>
    /// Get Entity by Id
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Entity by Id</returns>
    public Task<TClass> GetByIdAsync(object id, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return HttpHelper.GetRequestAsync<TClass>($"{MethodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Create a parametrized query and get a result
    /// </summary>
    /// <param name="queryBuilder">Query builder with specific parameters</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of filtered result</returns>
    public Task<List<TClass>> QueryAsync(QClass queryBuilder, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return HttpHelper.GetRequestAsync<List<TClass>>($"{MethodPath}{queryBuilder.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="Entity">Entity object</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated object</returns>
    public async Task<TClass> UpdateAsync(TClass Entity, CancellationToken cancellationToken = default)
    {
        string entity = JsonSerializer.Serialize(Entity, HttpHelper.JsonSerializerOptions);
        using StringContent postBody = new(entity, Encoding.UTF8, "application/json");
        return (await HttpHelper.PostRequestAsync<TClass>($"{MethodPath}/{(Entity as Base)?.Id}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }
}
