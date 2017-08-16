using System.Net.Http;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Users endpoint WP REST API
    /// </summary>
    public class Users : CRUDOperation<User, UsersQueryBuilder>
    {
        #region Init

        private new const string _methodPath = "users";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public Users(ref HttpHelper HttpHelper, string defaultPath) : base(ref HttpHelper, defaultPath, _methodPath, true)
        {
        }

        #endregion Init

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
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?reassign={ReassignUserID}").ConfigureAwait(false);
        }

        /// <summary>
        /// Delete user with reassign articles
        /// </summary>
        /// <param name="ID">User id for delete</param>
        /// <param name="ReassignUser">User object for reassign</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID, User ReassignUser)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?reassign={ReassignUser.Id}").ConfigureAwait(false);
        }

        /// <summary>
        /// Delete User and all his posts
        /// </summary>
        /// <param name="userToDelteID">User Id you want to delete</param>
        /// <param name="userToAssignPostsID">User Id you want the posts to assign to</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> DeleteAndReassignPosts(int userToDelteID, int userToAssignPostsID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{userToDelteID}?force=true&reassign={userToAssignPostsID}").ConfigureAwait(false);
        }

        #endregion Custom
    }
}