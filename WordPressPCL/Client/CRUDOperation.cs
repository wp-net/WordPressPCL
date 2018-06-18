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
    /// Base class for CRUDOperation with default implementation of all neccesary operations
    /// </summary>
    /// <typeparam name="TClass">DTO class</typeparam>
    /// <typeparam name="QClass">QueryBuilder class</typeparam>
    public abstract class CRUDOperation<TClass, QClass> : IReadOperation<TClass>, IUpdateOperation<TClass>, ICreateOperation<TClass>, IDeleteOperation, IQueryOperation<TClass, QClass> where TClass : class where QClass : QueryBuilder
    {
        /// <summary>
        /// Path to wp api EX. https://site.com/wp-json/
        /// </summary>
        protected string _defaultPath;

        /// <summary>
        /// path to endpoint EX. posts
        /// </summary>
        protected string _methodPath;

        /// <summary>
        /// HttpHelper object with helper method over HTTP requests
        /// </summary>
        protected HttpHelper _httpHelper;

        /// <summary>
        /// Is object must be force deleted
        /// </summary>
        protected bool _forceDeletion;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        /// <param name="methodPath">path to endpoint, EX. posts</param>
        /// <param name="forceDeletion">is objectes must be force deleted</param>
        protected CRUDOperation(ref HttpHelper HttpHelper, string defaultPath, string methodPath, bool forceDeletion = false)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            _methodPath = methodPath;
            _forceDeletion = forceDeletion;
        }

        /// <summary>
        /// Create Entity
        /// </summary>
        /// <param name="Entity">Entity object</param>
        /// <returns>Created object</returns>
        public async Task<TClass> Create(TClass Entity)
        {
            var entity = _httpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(Entity) : JsonConvert.SerializeObject(Entity, _httpHelper.JsonSerializerSettings);
            var postBody = new StringContent(entity, Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<TClass>($"{_defaultPath}{_methodPath}", postBody).ConfigureAwait(false)).Item1;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="ID">Entity Id</param>
        /// <returns>Result of operation</returns>
        public Task<bool> Delete(int ID)
        {
            return _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}" + (_forceDeletion ? "?force=true" : string.Empty));
        }

        /// <summary>
        /// Get latest
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Entity by Id</returns>
        public Task<IEnumerable<TClass>> Get(bool embed = false, bool useAuth = false)
        {
            return _httpHelper.GetRequest<IEnumerable<TClass>>($"{_defaultPath}{_methodPath}", embed, useAuth);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of all result</returns>
        public async Task<IEnumerable<TClass>> GetAll(bool embed = false, bool useAuth = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<TClass> entities = new List<TClass>();
            entities = (await _httpHelper.GetRequest<IEnumerable<TClass>>($"{_defaultPath}{_methodPath}?per_page=100&page=1", embed, useAuth).ConfigureAwait(false))?.ToList<TClass>();
            if (_httpHelper.LastResponseHeaders.Contains("X-WP-TotalPages") && System.Convert.ToInt32(_httpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault()) > 1)
            {
                int totalpages = System.Convert.ToInt32(_httpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault());
                for (int page = 2; page <= totalpages; page++)
                {
                    entities.AddRange((await _httpHelper.GetRequest<IEnumerable<TClass>>($"{_defaultPath}{_methodPath}?per_page=100&page={page}", embed, useAuth).ConfigureAwait(false))?.ToList<TClass>());
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
        public Task<TClass> GetByID(object ID, bool embed = false, bool useAuth = false)
        {
            return _httpHelper.GetRequest<TClass>($"{_defaultPath}{_methodPath}/{ID}", embed, useAuth);
        }

        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Query builder with specific parameters</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of filtered result</returns>
        public Task<IEnumerable<TClass>> Query(QClass queryBuilder, bool useAuth = false)
        {
            return _httpHelper.GetRequest<IEnumerable<TClass>>($"{_defaultPath}{_methodPath}{queryBuilder.BuildQueryURL()}", false, useAuth);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="Entity">Entity object</param>
        /// <returns>Updated object</returns>
        public async Task<TClass> Update(TClass Entity)
        {
            var entity = _httpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(Entity) : JsonConvert.SerializeObject(Entity, _httpHelper.JsonSerializerSettings);
            var postBody = new StringContent(entity, Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<TClass>($"{_defaultPath}{_methodPath}/{(Entity as Base)?.Id}", postBody).ConfigureAwait(false)).Item1;
        }
    }
}