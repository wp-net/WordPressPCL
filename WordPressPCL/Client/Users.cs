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
/// Client class for interaction with Users endpoint WP REST API
/// </summary>
public class Users : ICreateOperation<User>, IUpdateOperation<User>, IReadOperation<User>, IQueryOperation<User, UsersQueryBuilder>
{
    #region Init

    private const string METHOD_PATH = "users";
    private const string APPLICATION_PASSWORDS_PATH = "application-passwords";
    private readonly HttpHelper _httpHelper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Users(HttpHelper HttpHelper)
    {
        _httpHelper = HttpHelper;
    }

    #endregion Init

    /// <summary>
    /// Create Entity
    /// </summary>
    /// <param name="Entity">Entity object</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created object</returns>
    public virtual async Task<User> CreateAsync(User Entity, CancellationToken cancellationToken = default)
    {
        string entity = JsonSerializer.Serialize(Entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(entity, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<User>($"{METHOD_PATH}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Get latest
    /// </summary>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Get latest users</returns>
    public Task<List<User>> GetAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<User>>($"{METHOD_PATH}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get All
    /// </summary>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all result</returns>
    public async Task<List<User>> GetAllAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
        var (entities, headers) = await _httpHelper.GetRequestWithHeadersAsync<List<User>>($"{METHOD_PATH}?per_page=100&page=1", embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false);
        var (_, totalPages) = HttpHelper.ParsePaginationHeaders(headers);
        for (int page = 2; page <= totalPages; page++)
        {
            entities.AddRange(await _httpHelper.GetRequestAsync<List<User>>($"{METHOD_PATH}?per_page=100&page={page}", embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false));
        }
        return entities;
    }

    /// <summary>
    /// Get a single page of users with pagination metadata.
    /// </summary>
    /// <param name="page">Page number (1-based). Default: 1</param>
    /// <param name="perPage">Items per page (1–100). Default: 10</param>
    /// <param name="embed">Include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// A <see cref="PagedResult{User}"/> containing the items for the requested page
    /// plus the <c>X-WP-Total</c> and <c>X-WP-TotalPages</c> metadata.
    /// </returns>
    public async Task<PagedResult<User>> GetPagedAsync(int page = 1, int perPage = 10, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        var (items, headers) = await _httpHelper.GetRequestWithHeadersAsync<List<User>>(
            $"{METHOD_PATH}?per_page={perPage.ToString(CultureInfo.InvariantCulture)}&page={page.ToString(CultureInfo.InvariantCulture)}",
            embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false);
        var (total, totalPages) = HttpHelper.ParsePaginationHeaders(headers);
        return new PagedResult<User>(items, total, totalPages);
    }

    /// <summary>
    /// Execute a parametrized query and return users with pagination metadata.
    /// </summary>
    /// <param name="queryBuilder">Query builder with specific parameters</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// A <see cref="PagedResult{User}"/> containing the matching items plus
    /// <c>X-WP-Total</c> and <c>X-WP-TotalPages</c> metadata.
    /// </returns>
    public async Task<PagedResult<User>> QueryPagedAsync(UsersQueryBuilder queryBuilder, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        var (items, headers) = await _httpHelper.GetRequestWithHeadersAsync<List<User>>($"{METHOD_PATH}{queryBuilder.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false);
        var (total, totalPages) = HttpHelper.ParsePaginationHeaders(headers);
        return new PagedResult<User>(items, total, totalPages);
    }

    /// <summary>
    /// Get Entity by Id
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Entity by Id</returns>
    public Task<User> GetByIdAsync(object id, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<User>($"{METHOD_PATH}/{id}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Create a parametrized query and get a result
    /// </summary>
    /// <param name="queryBuilder">Query builder with specific parameters</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of filtered result</returns>
    public Task<List<User>> QueryAsync(UsersQueryBuilder queryBuilder, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<User>>($"{METHOD_PATH}{queryBuilder.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="Entity">Entity object</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated object</returns>
    public async Task<User> UpdateAsync(User Entity, CancellationToken cancellationToken = default)
    {
        string entity = JsonSerializer.Serialize(Entity, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(entity, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<User>($"{METHOD_PATH}/{Entity?.Id}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    #region Custom

    /// <summary>
    /// Get current User
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Current User</returns>
    public Task<User> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<User>($"{METHOD_PATH}/me", true, true, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Delete user with reassign articles
    /// </summary>
    /// <param name="ID">User id for delete</param>
    /// <param name="ReassignUserID">User id for reassign</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of operation</returns>
    public Task<bool> Delete(int ID, int ReassignUserID, CancellationToken cancellationToken = default)
    {
        return _httpHelper.DeleteRequestAsync($"{METHOD_PATH}/{ID}?force=true&reassign={ReassignUserID}", cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Delete user with reassign articles
    /// </summary>
    /// <param name="ID">User id for delete</param>
    /// <param name="ReassignUser">User object for reassign</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of operation</returns>
    public Task<bool> Delete(int ID, User ReassignUser, CancellationToken cancellationToken = default)
    {
        return _httpHelper.DeleteRequestAsync($"{METHOD_PATH}/{ID}?force=true&reassign={ReassignUser?.Id}", cancellationToken: cancellationToken);
    }

    #endregion Custom

    #region Application Passwords

    /// <summary>
    /// Create a new Application Password
    /// </summary>
    /// <param name="applicationName">User-defined name for application</param>
    /// <param name="userId">User ID, defaults to "me"</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<ApplicationPassword> CreateApplicationPasswordAsync(string applicationName, string userId = "me", CancellationToken cancellationToken = default)
    {
        var body = new { name = applicationName };
        string entity = JsonSerializer.Serialize(body, _httpHelper.JsonSerializerOptions);
        using StringContent postBody = new(entity, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<ApplicationPassword>($"{METHOD_PATH}/{userId}/{APPLICATION_PASSWORDS_PATH}", postBody, true, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    /// <summary>
    /// Get Application Passwords for specified user
    /// </summary>
    /// <param name="userId">User ID, defaults to "me"</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of registered Application Passwords (without the actual password)</returns>
    public Task<List<ApplicationPassword>> GetApplicationPasswords(string userId = "me", CancellationToken cancellationToken = default)
    {
        return _httpHelper.GetRequestAsync<List<ApplicationPassword>>($"{METHOD_PATH}/{userId}/{APPLICATION_PASSWORDS_PATH}", false, true, cancellationToken: cancellationToken);
    }

    #endregion
}
