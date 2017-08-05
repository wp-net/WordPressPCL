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
    /// Client class for interaction with Comments endpoint WP REST API
    /// </summary>

    public class Comments : ICRUDOperationAsync<Comment>
    {
        #region Init
        private string _defaultPath;
        private const string _methodPath = "comments";
        private HttpHelper _httpHelper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public Comments(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
        }
        #endregion
        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Comments query builder with specific parameters</param>
        /// <returns>List of filtered comments</returns>
        public async Task<IEnumerable<Comment>> Query(CommentsQueryBuilder queryBuilder)
        {
            return await _httpHelper.GetRequest<IEnumerable<Comment>>($"{_defaultPath}{_methodPath}{queryBuilder.BuildQueryURL()}", false).ConfigureAwait(false);
        }
        #region Interface Realisation
        /// <summary>
        /// Create Comment
        /// </summary>
        /// <param name="Entity">Comment object</param>
        /// <returns>Created comment object</returns>
        public async Task<Comment> Create(Comment Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Comment>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }
        /// <summary>
        /// Update Comment
        /// </summary>
        /// <param name="Entity">Comment object</param>
        /// <returns>Updated comment object</returns>
        public async Task<Comment> Update(Comment Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Comment>($"{_defaultPath}{_methodPath}/{Entity.Id}", postBody)).Item1;
        }
        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param name="ID">Comment Id</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}").ConfigureAwait(false);
        }
        /// <summary>
        /// Get All Comments
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <returns>List of all Comments</returns>
        public async Task<IEnumerable<Comment>> GetAll(bool embed = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<Comment> comments = new List<Comment>();
            List<Comment> comments_page = new List<Comment>();
            int page = 1;
            do
            {
                comments_page = (await _httpHelper.GetRequest<IEnumerable<Comment>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed).ConfigureAwait(false))?.ToList<Comment>();
                if (comments_page != null && comments_page.Count>0) { comments.AddRange(comments_page); }

            } while (comments_page != null && comments_page.Count > 0);

            return comments;
            //return await _httpHelper.GetRequest<IEnumerable<Comment>>($"{_defaultPath}{_methodPath}", embed).ConfigureAwait(false);
        }


        /// <summary>
        /// Get Comment by Id
        /// </summary>
        /// <param name="ID">Comment ID</param>
        /// <param name="embed">include embed info</param>
        /// <returns>Comment by Id</returns>
        public async Task<Comment> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<Comment>($"{_defaultPath}{_methodPath}/{ID}", embed).ConfigureAwait(false);
        }

        
        #endregion

        #region Custom
        /// <summary>
        /// Get comments for Post
        /// </summary>
        /// <param name="PostID">Post id</param>
        /// <param name="embed">include embed info</param>
        /// <returns>List of comments for post</returns>
        public async Task<IEnumerable<Comment>> GetCommentsForPost(string PostID, bool embed = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<Comment>>($"{_defaultPath}{_methodPath}?post={PostID}", embed);
        }
        /// <summary>
        /// Force deletion of comments
        /// </summary>
        /// <param name="ID">Comment Id</param>
        /// <param name="force">force deletion</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID, bool force=false)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force={force}").ConfigureAwait(false);
        }
        #endregion
    }
}
