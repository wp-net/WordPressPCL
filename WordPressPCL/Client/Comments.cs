using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        private const string _methodPath = "comments";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public Comments(HttpHelper HttpHelper) : base(HttpHelper, _methodPath)
        {
        }

        #endregion Init

        #region Custom

        /// <summary>
        /// Get comments for Post
        /// </summary>
        /// <param name="PostID">Post id</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>List of comments for post</returns>
        public Task<List<Comment>> GetCommentsForPostAsync(int PostID, bool embed = false, bool useAuth = false)
        {
            return HttpHelper.GetRequestAsync<List<Comment>>($"{_methodPath}?post={PostID}", embed, useAuth);
        }

        /// <summary>
        /// Get all comments for Post
        /// </summary>
        /// <param name="PostID">Post id</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <returns>List of comments for post</returns>
        public async Task<List<Comment>> GetAllCommentsForPostAsync(int PostID, bool embed = false, bool useAuth = false)
        {
            //100 - Max comments per page in WordPress REST API, so this is hack with multiple requests
            List<Comment> comments = await HttpHelper.GetRequestAsync<List<Comment>>($"{_methodPath}?post={PostID}&per_page=100&page=1", embed, useAuth).ConfigureAwait(false);
            if (HttpHelper.LastResponseHeaders.Contains("X-WP-TotalPages") &&
                int.TryParse(HttpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault(), out int totalPages) &&
                totalPages > 1)
            {
                for (int page = 2; page <= totalPages; page++)
                {
                    comments.AddRange(await HttpHelper.GetRequestAsync<List<Comment>>($"{_methodPath}?post={PostID}&per_page=100&page={page}", embed, useAuth).ConfigureAwait(false));
                }
            }
            return comments;
        }

        /// <summary>
        /// Force deletion of comments
        /// </summary>
        /// <param name="ID">Comment Id</param>
        /// <param name="force">force deletion</param>
        /// <returns>Result of operation</returns>
        public Task<bool> DeleteAsync(int ID, bool force = false)
        {
            return HttpHelper.DeleteRequestAsync($"{_methodPath}/{ID}?force={force.ToString().ToLower(CultureInfo.InvariantCulture)}");
        }

        /// <summary>
        /// Update Comment
        /// </summary>
        /// <param name="Entity">Comment object</param>
        /// <returns>Updated comment</returns>
        public new async Task<Comment> UpdateAsync(Comment Entity)
        {
            Dictionary<string, object> body = new();

            if (Entity.PostId != 0)
            {
                body["post"] = Entity.PostId;
            }

            if (Entity.ParentId != 0)
            {
                body["parent"] = Entity.ParentId;
            }

            if (Entity.AuthorId != 0)
            {
                body["author"] = Entity.AuthorId;
            }

            if (!string.IsNullOrWhiteSpace(Entity.AuthorName))
            {
                body["author_name"] = Entity.AuthorName;
            }

            if (!string.IsNullOrWhiteSpace(Entity.AuthorEmail))
            {
                body["author_email"] = Entity.AuthorEmail;
            }

            if (!string.IsNullOrWhiteSpace(Entity.AuthorUrl))
            {
                body["author_url"] = Entity.AuthorUrl;
            }

            if (!string.IsNullOrWhiteSpace(Entity.Status))
            {
                body["status"] = Entity.Status;
            }

            if (Entity.Karma != 0)
            {
                body["karma"] = Entity.Karma;
            }

            if (Entity.Meta != null)
            {
                body["meta"] = Entity.Meta;
            }
            if (!string.IsNullOrWhiteSpace(Entity.Content?.Raw) || !string.IsNullOrWhiteSpace(Entity.Content?.Rendered))
            {
                body["content"] = new
                {
                    raw = Entity.Content?.Raw ?? Entity.Content?.Rendered
                };
            }

            string entity = HttpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(body) : JsonConvert.SerializeObject(body, HttpHelper.JsonSerializerSettings);
            using StringContent postBody = new(entity, Encoding.UTF8, "application/json");
            return (await HttpHelper.PostRequestAsync<Comment>($"{_methodPath}/{Entity?.Id}", postBody).ConfigureAwait(false)).Item1;
        }

        #endregion Custom
    }
}
