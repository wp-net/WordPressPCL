using Newtonsoft.Json;
using System.Collections.Generic;
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

        private string _defaultPath;
        private const string _methodPath = "users";
        private HttpHelper _httpHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public Users(ref HttpHelper HttpHelper, string defaultPath)
        {
            _httpHelper = HttpHelper;
            _defaultPath = defaultPath;
        }

        #endregion Init

        /// <summary>
        /// Create Entity
        /// </summary>
        /// <param name="Entity">Entity object</param>
        /// <returns>Created object</returns>
        public virtual async Task<User> Create(User Entity)
        {
            var entity = _httpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(Entity) : JsonConvert.SerializeObject(Entity, _httpHelper.JsonSerializerSettings);
            var postBody = new StringContent(entity, Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<User>($"{_defaultPath}{_methodPath}", postBody).ConfigureAwait(false)).Item1;
        }

        /// <summary>
        /// Get latest
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Get latest users</returns>
        public Task<IEnumerable<User>> Get(bool embed = false, bool useAuth = false)
        {
            return _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}{_methodPath}", embed, useAuth);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of all result</returns>
        public async Task<IEnumerable<User>> GetAll(bool embed = false, bool useAuth = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<User> entities = new List<User>();
            entities = (await _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}{_methodPath}?per_page=100&page=1", embed, useAuth).ConfigureAwait(false))?.ToList<User>();
            if (_httpHelper.LastResponseHeaders.Contains("X-WP-TotalPages") && System.Convert.ToInt32(_httpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault()) > 1)
            {
                int totalpages = System.Convert.ToInt32(_httpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault());
                for (int page = 2; page <= totalpages; page++)
                {
                    entities.AddRange((await _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}{_methodPath}?per_page=100&page={page}", embed, useAuth).ConfigureAwait(false))?.ToList<User>());
                }
            }
            return entities;
        }

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Entity by Id</returns>
        public Task<User> GetByID(object ID, bool embed = false, bool useAuth = false)
        {
            return _httpHelper.GetRequest<User>($"{_defaultPath}{_methodPath}/{ID}", embed, useAuth);
        }

        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Query builder with specific parameters</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of filtered result</returns>
        public Task<IEnumerable<User>> Query(UsersQueryBuilder queryBuilder, bool useAuth = false)
        {
            return _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}{_methodPath}{queryBuilder.BuildQueryURL()}", false, useAuth);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="Entity">Entity object</param>
        /// <returns>Updated object</returns>
        public async Task<User> Update(User Entity)
        {
            var entity = _httpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(Entity) : JsonConvert.SerializeObject(Entity, _httpHelper.JsonSerializerSettings);
            var postBody = new StringContent(entity, Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<User>($"{_defaultPath}{_methodPath}/{(Entity as Base)?.Id}", postBody).ConfigureAwait(false)).Item1;
        }

        #region Custom

        /// <summary>
        /// Get current User
        /// </summary>
        /// <returns>Current User</returns>
        public Task<User> GetCurrentUser()
        {
            return _httpHelper.GetRequest<User>($"{_defaultPath}{_methodPath}/me", true, true);
        }

        /// <summary>
        /// Delete user with reassign articles
        /// </summary>
        /// <param name="ID">User id for delete</param>
        /// <param name="ReassignUserID">User id for reassign</param>
        /// <returns>Result of operation</returns>
        public Task<bool> Delete(int ID, int ReassignUserID)
        {
            return _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force=true&reassign={ReassignUserID}");
        }

        /// <summary>
        /// Delete user with reassign articles
        /// </summary>
        /// <param name="ID">User id for delete</param>
        /// <param name="ReassignUser">User object for reassign</param>
        /// <returns>Result of operation</returns>
        public Task<bool> Delete(int ID, User ReassignUser)
        {
            return _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force=true&reassign={ReassignUser.Id}");
        }

        #endregion Custom
    }
}