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
    /// Client class for interaction with Pages endpoint WP REST API
    /// </summary>
    public class Pages : ICRUDOperationAsync<Page>
    {
        #region Init
        private string _defaultPath;
        private const string _methodPath = "pages";
        private HttpHelper _httpHelper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public Pages(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
        }
        #endregion
        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Pages query builder with specific parameters</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of filtered pages</returns>
        public async Task<IEnumerable<Page>> Query(PagesQueryBuilder queryBuilder, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<Page>>($"{_defaultPath}{_methodPath}{queryBuilder.BuildQueryURL()}", false,useAuth).ConfigureAwait(false);
        }
        #region Interface Realisation
        /// <summary>
        /// Create Page
        /// </summary>
        /// <param name="Entity">Page object</param>
        /// <returns>Created page object</returns>
        public async Task<Page> Create(Page Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Page>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }
        /// <summary>
        /// Update Page
        /// </summary>
        /// <param name="Entity">Page object</param>
        /// <returns>Updated page object</returns>
        public async Task<Page> Update(Page Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Page>($"{_defaultPath}{_methodPath}/{Entity.Id}", postBody)).Item1;
        }
        /// <summary>
        /// Delete Page
        /// </summary>
        /// <param name="ID">Page Id</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}").ConfigureAwait(false);
        }
        /// <summary>
        /// Get All Pages
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of all Pages</returns>
        public async Task<IEnumerable<Page>> GetAll(bool embed = false, bool useAuth = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<Page> posts = new List<Page>();
            List<Page> posts_page = new List<Page>();
            int page = 1;
            do
            {
                posts_page = (await _httpHelper.GetRequest<IEnumerable<Page>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed,useAuth).ConfigureAwait(false))?.ToList<Page>();
                if (posts_page != null && posts_page.Count > 0) { posts.AddRange(posts_page); }

            } while (posts_page != null && posts_page.Count > 0);

            return posts;
        }
        /// <summary>
        /// Get Page by Id
        /// </summary>
        /// <param name="ID">Page ID</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Page by Id</returns>
        public async Task<Page> GetByID(int ID, bool embed = false, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<Page>($"{_defaultPath}{_methodPath}/{ID}", embed,useAuth).ConfigureAwait(false);
        }
        #endregion

        #region Custom

        /// <summary>
        /// Get pages by its author
        /// </summary>
        /// <param name="authorId">Author id</param>
        /// <param name="embed">includ embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of pages</returns>
        public async Task<IEnumerable<Page>> GetPagesByAuthor(int authorId, bool embed = false, bool useAuth = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Page>>($"{_defaultPath}{_methodPath}?author={authorId}", embed,useAuth).ConfigureAwait(false);
        }

        /// <summary>
        /// Get pages by search term
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of pages</returns>
        public async Task<IEnumerable<Page>> GetPagesBySearch(string searchTerm, bool embed = false, bool useAuth = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Page>>($"{_defaultPath}{_methodPath}?search={searchTerm}", embed,useAuth).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Delete page with force deletion
        /// </summary>
        /// <param name="ID">Page id</param>
        /// <param name="force">force deletion</param>
        /// <returns>Result of opertion</returns>
        public async Task<HttpResponseMessage> Delete(int ID, bool force = false)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force={force}").ConfigureAwait(false);
        }
        #endregion
    }
}
