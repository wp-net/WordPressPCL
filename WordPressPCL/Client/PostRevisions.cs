using System.Collections.Generic;
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
        private readonly HttpHelper _httpHelper;
        private const string _methodPath = "revisions";
        private readonly int _postId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="postId">ID of post</param>
        public PostRevisions(ref HttpHelper httpHelper, int postId)
        {
            _httpHelper = httpHelper;
            _postId = postId;
        }
        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="ID">Entity Id</param>
        /// <returns>Result of operation</returns>
        public Task<bool> Delete(int ID)
        {
            return _httpHelper.DeleteRequestAsync($"posts/{_postId}/{_methodPath}/{ID}?force=true");
        }

        /// <summary>
        /// Get latest
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>Latest PostRevisions</returns>
        public Task<IEnumerable<PostRevision>> GetAsync(bool embed = false, bool useAuth = true)
        {
            return _httpHelper.GetRequestAsync<IEnumerable<PostRevision>>($"posts/{_postId}/{_methodPath}", embed, useAuth);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>List of all result</returns>
        public Task<IEnumerable<PostRevision>> GetAllAsync(bool embed = false, bool useAuth = true)
        {
            return _httpHelper.GetRequestAsync<IEnumerable<PostRevision>>($"posts/{_postId}/{_methodPath}", embed, useAuth);
        }

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>Entity by Id</returns>
        public Task<PostRevision> GetByID(object ID, bool embed = false, bool useAuth = true)
        {
            return _httpHelper.GetRequestAsync<PostRevision>($"posts/{_postId}/{_methodPath}/{ID}", embed, useAuth);
        }
    }
}
