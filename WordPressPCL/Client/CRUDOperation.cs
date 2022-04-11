using Newtonsoft.Json;
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
        internal protected HttpHelper _httpHelper;
        
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
        /// <returns>Created object</returns>
        public async Task<TClass> CreateAsync(TClass Entity)
        {
            var entity = HttpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(Entity) : JsonConvert.SerializeObject(Entity, HttpHelper.JsonSerializerSettings);
            using var postBody = new StringContent(entity, Encoding.UTF8, "application/json");
            return (await HttpHelper.PostRequestAsync<TClass>(MethodPath, postBody).ConfigureAwait(false)).Item1;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="Id">Entity Id</param>
        /// <returns>Result of operation</returns>
        public Task<bool> DeleteAsync(int Id)
        {
            var path = $"{MethodPath}/{Id}".SetQueryParam("force", ForceDeletion.ToString().ToLower(CultureInfo.InvariantCulture));
            return HttpHelper.DeleteRequestAsync(path);
        }

        /// <summary>
        /// Get latest
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>Entity by Id</returns>
            public Task<IEnumerable<TClass>> GetAsync(bool embed = false, bool useAuth = false)
        {
            return HttpHelper.GetRequestAsync<IEnumerable<TClass>>(MethodPath, embed, useAuth);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>List of all result</returns>
        public async Task<IEnumerable<TClass>> GetAllAsync(bool embed = false, bool useAuth = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            var url = MethodPath.SetQueryParam("per_page", "100").SetQueryParam("page", "1");
            var entities = (await HttpHelper.GetRequestAsync<IEnumerable<TClass>>(url, embed, useAuth).ConfigureAwait(false))?.ToList();
            if (HttpHelper.LastResponseHeaders.Contains("X-WP-TotalPages") && Convert.ToInt32(HttpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault(), CultureInfo.InvariantCulture) > 1)
            {
                int totalpages = Convert.ToInt32(HttpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault(), CultureInfo.InvariantCulture);
                for (int page = 2; page <= totalpages; page++)
                {
                    url = MethodPath.SetQueryParam("per_page","100").SetQueryParam("page", page.ToString());
                    entities.AddRange((await HttpHelper.GetRequestAsync<IEnumerable<TClass>>(url, embed, useAuth).ConfigureAwait(false))?.ToList());
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
        public Task<TClass> GetByIDAsync(object ID, bool embed = false, bool useAuth = false)
        {
            return HttpHelper.GetRequestAsync<TClass>($"{MethodPath}/{ID}", embed, useAuth);
        }

        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Query builder with specific parameters</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>List of filtered result</returns>
        public Task<IEnumerable<TClass>> QueryAsync(QClass queryBuilder, bool useAuth = false)
        {
            return HttpHelper.GetRequestAsync<IEnumerable<TClass>>($"{MethodPath}{queryBuilder.BuildQuery()}", false, useAuth);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="Entity">Entity object</param>
        /// <returns>Updated object</returns>
        public async Task<TClass> UpdateAsync(TClass Entity)
        {
            var entity = HttpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(Entity) : JsonConvert.SerializeObject(Entity, HttpHelper.JsonSerializerSettings);
            using var postBody = new StringContent(entity, Encoding.UTF8, "application/json");
            return (await HttpHelper.PostRequestAsync<TClass>($"{MethodPath}/{(Entity as Base)?.Id}", postBody).ConfigureAwait(false)).Item1;
        }
    }
}