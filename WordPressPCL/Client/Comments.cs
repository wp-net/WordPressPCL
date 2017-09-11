using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Comments endpoint WP REST API
    /// </summary>

    public class Comments : CRUDOperation<Comment, CommentsQueryBuilder>
    {
        #region Init

        private new const string _methodPath = "comments";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public Comments(ref HttpHelper HttpHelper, string defaultPath) : base(ref HttpHelper, defaultPath, _methodPath)
        {
        }

        #endregion Init

        #region Custom

        /// <summary>
        /// Get comments for Post
        /// </summary>
        /// <param name="PostID">Post id</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of comments for post</returns>
        public async Task<IEnumerable<Comment>> GetCommentsForPost(string PostID, bool embed = false, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<Comment>>($"{_defaultPath}{_methodPath}?post={PostID}", embed, useAuth);
        }

        /// <summary>
        /// Force deletion of comments
        /// </summary>
        /// <param name="ID">Comment Id</param>
        /// <param name="force">force deletion</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID, bool force = false)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force={force.ToString().ToLower()}").ConfigureAwait(false);
        }

        #endregion Custom
    }
}