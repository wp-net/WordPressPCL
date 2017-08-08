using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Utility;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using WordPressPCL.Models;
using WordPressPCL.Interfaces;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Tags endpoint WP REST API
    /// </summary>
    public class Tags : ICRUDOperationAsync<Tag>
    {
        #region Init
        private string _defaultPath;
        private const string _methodPath = "tags";
        
        private HttpHelper _httpHelper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public Tags(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            
        }
        #endregion
        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Tags query builder with specific parameters</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of filtered tags</returns>
        public async Task<IEnumerable<Tag>> Query(TagsQueryBuilder queryBuilder, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<Tag>>($"{_defaultPath}{_methodPath}{queryBuilder.BuildQueryURL()}", false,useAuth).ConfigureAwait(false);
        }
        #region Interface Realisation
        /// <summary>
        /// Create Tag
        /// </summary>
        /// <param name="Entity">Tag object</param>
        /// <returns>Created tag object</returns>
        public async Task<Tag> Create(Tag Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Tag>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }
        /// <summary>
        /// Update Tag
        /// </summary>
        /// <param name="Entity">Tag object</param>
        /// <returns>Updated Tag object</returns>
        public async Task<Tag> Update(Tag Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Tag>($"{_defaultPath}{_methodPath}/{Entity.Id}", postBody)).Item1;
        }
        /// <summary>
        /// Delete Tag
        /// </summary>
        /// <param name="ID">Tag Id</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force=true").ConfigureAwait(false);
        }
        /// <summary>
        /// Get All Tags
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of all Tags</returns>
        public async Task<IEnumerable<Tag>> GetAll(bool embed = false, bool useAuth = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<Tag> tags = new List<Tag>();
            List<Tag> tags_page = new List<Tag>();
            int page = 1;
            do
            {
                tags_page = (await _httpHelper.GetRequest<IEnumerable<Tag>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed,useAuth).ConfigureAwait(false))?.ToList<Tag>();
                if (tags_page != null && tags_page.Count > 0) { tags.AddRange(tags_page); }

            } while (tags_page != null && tags_page.Count > 0);

            return tags;
            //return await _httpHelper.GetRequest<IEnumerable<Tag>>($"{_defaultPath}{_methodPath}", embed).ConfigureAwait(false);
        }


        /// <summary>
        /// Get Tag by Id
        /// </summary>
        /// <param name="ID">Tag ID</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Tag by Id</returns>
        public async Task<Tag> GetByID(int ID, bool embed = false, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<Tag>($"{_defaultPath}{_methodPath}/{ID}", embed,useAuth).ConfigureAwait(false);
        }

        #endregion

        #region Custom
        
        #endregion
    }
}
