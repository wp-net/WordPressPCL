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
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<User>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }

        /// <summary>
        /// Get latest
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Get latest users</returns>
        public async Task<IEnumerable<User>> Get(bool embed = false, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}{_methodPath}", embed, useAuth).ConfigureAwait(false);
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
            List<User> entities_page = new List<User>();
            int page = 1;
            do
            {
                entities_page = (await _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed, useAuth).ConfigureAwait(false))?.ToList<User>();
                if (entities_page != null && entities_page.Count > 0) { entities.AddRange(entities_page); }
            } while (entities_page != null && entities_page.Count > 0);

            return entities;
        }

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Entity by Id</returns>
        public async Task<User> GetByID(object ID, bool embed = false, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<User>($"{_defaultPath}{_methodPath}/{ID}", embed, useAuth).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Query builder with specific parameters</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of filtered result</returns>
        public async Task<IEnumerable<User>> Query(UsersQueryBuilder queryBuilder, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}{_methodPath}{queryBuilder.BuildQueryURL()}", false, useAuth).ConfigureAwait(false);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="Entity">Entity object</param>
        /// <returns>Updated object</returns>
        public async Task<User> Update(User Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<User>($"{_defaultPath}{_methodPath}/{(Entity as Base).Id}", postBody)).Item1;
        }

        #region Custom

        /// <summary>
        /// Get current User
        /// </summary>
        /// <returns>Current User</returns>
        public async Task<User> GetCurrentUser()
        {
            return await _httpHelper.GetRequest<User>($"{_defaultPath}{_methodPath}/me", true, true).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete user with reassign articles
        /// </summary>
        /// <param name="ID">User id for delete</param>
        /// <param name="ReassignUserID">User id for reassign</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID, int ReassignUserID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force=true&reassign={ReassignUserID}").ConfigureAwait(false);
        }

        /// <summary>
        /// Delete user with reassign articles
        /// </summary>
        /// <param name="ID">User id for delete</param>
        /// <param name="ReassignUser">User object for reassign</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID, User ReassignUser)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force=true&reassign={ReassignUser.Id}").ConfigureAwait(false);
        }

        #endregion Custom
    }
}