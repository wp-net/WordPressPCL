using System;
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
    /// Client class for interaction with Post revisions endpoint WP REST API
    /// </summary>
    public class PostRevisions : IReadOperation<PostRevision>, IDeleteOperation
    {
        private HttpHelper _httpHelper;
        private string _defaultPath;
        private const string _methodPath = "revisions";
        private int _postId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        /// <param name="postId">ID of post</param>
        public PostRevisions(ref HttpHelper httpHelper, string defaultPath,int postId)
        {
            _httpHelper = httpHelper;
            _defaultPath = defaultPath;
            _postId = postId;
        }
        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="ID">Entity Id</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}posts/{_postId}/{_methodPath}/{ID}?force=true").ConfigureAwait(false);
        }

        /// <summary>
        /// Get latest
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Latest PostRevisions</returns>
        public async Task<IEnumerable<PostRevision>> Get(bool embed = false, bool useAuth = true)
        {
            return await _httpHelper.GetRequest<IEnumerable<PostRevision>>($"{_defaultPath}posts/{_postId}/{_methodPath}", embed, useAuth).ConfigureAwait(false);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of all result</returns>
        public async Task<IEnumerable<PostRevision>> GetAll(bool embed = false, bool useAuth = true)
        {
            return await _httpHelper.GetRequest<IEnumerable<PostRevision>>($"{_defaultPath}posts/{_postId}/{_methodPath}", embed, useAuth).ConfigureAwait(false);
        }

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Entity by Id</returns>
        public async Task<PostRevision> GetByID(object ID, bool embed = false, bool useAuth = true)
        {
            return await _httpHelper.GetRequest<PostRevision>($"{_defaultPath}posts/{_postId}/{_methodPath}/{ID}", embed, useAuth).ConfigureAwait(false);
        }
    }
}
