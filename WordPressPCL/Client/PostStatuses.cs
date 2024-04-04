using System.Collections.Generic;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Post Types endpoint WP REST API
    /// </summary>
    public class PostStatuses : IReadOperation<PostStatus>
    {
        private readonly HttpHelper _httpHelper;
        private const string _methodPath = "statuses";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public PostStatuses(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        /// <summary>
        /// Get latest Post Statuses
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>Entity by Id</returns>
        public async Task<List<PostStatus>> GetAsync(bool embed = false, bool useAuth = false)
        {
            List<PostStatus> entities = new();
            Dictionary<string, PostStatus> entities_page = await _httpHelper.GetRequestAsync<Dictionary<string, PostStatus>>($"{_methodPath}", embed, useAuth).ConfigureAwait(false);
            foreach (KeyValuePair<string, PostStatus> ent in entities_page)
            {
                entities.Add(ent.Value);
            }
            return entities;
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>List of all result</returns>
        public async Task<List<PostStatus>> GetAllAsync(bool embed = false, bool useAuth = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<PostStatus> entities = new();
            Dictionary<string, PostStatus> entities_page = await _httpHelper.GetRequestAsync<Dictionary<string, PostStatus>>($"{_methodPath}", embed, useAuth).ConfigureAwait(false);
            foreach (KeyValuePair<string, PostStatus> ent in entities_page)
            {
                entities.Add(ent.Value);
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
        public Task<PostStatus> GetByIDAsync(object ID, bool embed = false, bool useAuth = false)
        {
            return _httpHelper.GetRequestAsync<PostStatus>($"{_methodPath}/{ID}", embed, useAuth);
        }
    }
}