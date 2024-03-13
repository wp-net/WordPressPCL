﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
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
        /// <returns>Created object</returns>
        public virtual async Task<User> CreateAsync(User Entity)
        {
            var entity = _httpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(Entity) : JsonConvert.SerializeObject(Entity, _httpHelper.JsonSerializerSettings);
            using var postBody = new StringContent(entity, Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequestAsync<User>($"{METHOD_PATH}", postBody).ConfigureAwait(false)).Item1;
        }

        /// <summary>
        /// Get latest
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>Get latest users</returns>
        public Task<IEnumerable<User>> GetAsync(bool embed = false, bool useAuth = false)
        {
            return _httpHelper.GetRequestAsync<IEnumerable<User>>($"{METHOD_PATH}", embed, useAuth);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>List of all result</returns>
        public async Task<IEnumerable<User>> GetAllAsync(bool embed = false, bool useAuth = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            var entities = (await _httpHelper.GetRequestAsync<IEnumerable<User>>($"{METHOD_PATH}?per_page=100&page=1", embed, useAuth).ConfigureAwait(false))?.ToList();
            if (_httpHelper.LastResponseHeaders.Contains("X-WP-TotalPages") && Convert.ToInt32(_httpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault(), CultureInfo.InvariantCulture) > 1)
            {
                int totalpages = Convert.ToInt32(_httpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault(), CultureInfo.InvariantCulture);
                for (int page = 2; page <= totalpages; page++)
                {
                    entities.AddRange((await _httpHelper.GetRequestAsync<IEnumerable<User>>($"{METHOD_PATH}?per_page=100&page={page}", embed, useAuth).ConfigureAwait(false))?.ToList());
                }
            }
            return entities;
        }

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>Entity by Id</returns>
        public Task<User> GetByIDAsync(object ID, bool embed = false, bool useAuth = false)
        {
            return _httpHelper.GetRequestAsync<User>($"{METHOD_PATH}/{ID}", embed, useAuth);
        }

        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Query builder with specific parameters</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>List of filtered result</returns>
        public Task<IEnumerable<User>> QueryAsync(UsersQueryBuilder queryBuilder, bool useAuth = false)
        {
            return _httpHelper.GetRequestAsync<IEnumerable<User>>($"{METHOD_PATH}{queryBuilder.BuildQuery()}", false, useAuth);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="Entity">Entity object</param>
        /// <returns>Updated object</returns>
        public async Task<User> UpdateAsync(User Entity)
        {
            var entity = _httpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(Entity) : JsonConvert.SerializeObject(Entity, _httpHelper.JsonSerializerSettings);
            using var postBody = new StringContent(entity, Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequestAsync<User>($"{METHOD_PATH}/{Entity?.Id}", postBody).ConfigureAwait(false)).Item1;
        }

        #region Custom

        /// <summary>
        /// Get current User
        /// </summary>
        /// <returns>Current User</returns>
        public Task<User> GetCurrentUserAsync()
        {
            return _httpHelper.GetRequestAsync<User>($"{METHOD_PATH}/me", true, true);
        }

        /// <summary>
        /// Delete user with reassign articles
        /// </summary>
        /// <param name="ID">User id for delete</param>
        /// <param name="ReassignUserID">User id for reassign</param>
        /// <returns>Result of operation</returns>
        public Task<bool> Delete(int ID, int ReassignUserID)
        {
            return _httpHelper.DeleteRequestAsync($"{METHOD_PATH}/{ID}?force=true&reassign={ReassignUserID}");
        }

        /// <summary>
        /// Delete user with reassign articles
        /// </summary>
        /// <param name="ID">User id for delete</param>
        /// <param name="ReassignUser">User object for reassign</param>
        /// <returns>Result of operation</returns>
        public Task<bool> Delete(int ID, User ReassignUser)
        {
            return _httpHelper.DeleteRequestAsync($"{METHOD_PATH}/{ID}?force=true&reassign={ReassignUser?.Id}");
        }

        #endregion Custom

        #region Application Passwords

        /// <summary>
        /// Create a new Application Password
        /// </summary>
        /// <param name="applicationName">User-defined name for application</param>
        /// <param name="userId">User ID, defaults to "me"</param>
        /// <returns></returns>
        public async Task<ApplicationPassword> CreateApplicationPasswordAsync(string applicationName, string userId = "me")
        {
            var body = new { name = applicationName };
            var entity = _httpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(body) : JsonConvert.SerializeObject(body, _httpHelper.JsonSerializerSettings);
            using var postBody = new StringContent(entity, Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequestAsync<ApplicationPassword>($"{METHOD_PATH}/{userId}/{APPLICATION_PASSWORDS_PATH}", postBody, true).ConfigureAwait(false)).Item1;
        }

        /// <summary>
        /// Get Application Passwords for specified user
        /// </summary>
        /// <param name="userId">User ID, defaults to "me"</param>
        /// <returns>List of registered Application Passwords (without the actual password)</returns>
        public Task<List<ApplicationPassword>> GetApplicationPasswords(string userId = "me")
        {
            return _httpHelper.GetRequestAsync<List<ApplicationPassword>>($"{METHOD_PATH}/{userId}/{APPLICATION_PASSWORDS_PATH}", false, true);
        }

        #endregion
    }
}