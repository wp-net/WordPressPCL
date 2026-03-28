using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Posts endpoint WP REST API
    /// </summary>
    public class Posts : CRUDOperation<Post, PostsQueryBuilder>, ICountOperation
    {
        #region Init

        private const string _methodPath = "posts";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public Posts(HttpHelper HttpHelper) : base(HttpHelper, _methodPath)
        {
        }

        #endregion Init

        #region Custom

        /// <summary>
        /// Get sticky posts
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of posts</returns>
        public Task<List<Post>> GetStickyPostsAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
        {
            // default values
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return HttpHelper.GetRequestAsync<List<Post>>(_methodPath.SetQueryParam("sticky", "true")!, embed, useAuth, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get posts by category
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of posts</returns>
        public Task<List<Post>> GetPostsByCategoryAsync(int categoryId, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
        {
            // default values
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return HttpHelper.GetRequestAsync<List<Post>>(_methodPath.SetQueryParam("categories", categoryId)!, embed, useAuth, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get posts by tag
        /// </summary>
        /// <param name="tagId">Tag Id</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of posts</returns>
        public Task<List<Post>> GetPostsByTagAsync(int tagId, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
        {
            // default values
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return HttpHelper.GetRequestAsync<List<Post>>(_methodPath.SetQueryParam("tags", tagId)!, embed, useAuth, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get posts by its author
        /// </summary>
        /// <param name="authorId">Author id</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of posts</returns>
        public Task<List<Post>> GetPostsByAuthorAsync(int authorId, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
        {
            // default values
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return HttpHelper.GetRequestAsync<List<Post>>(_methodPath.SetQueryParam("author", authorId)!, embed, useAuth, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get posts by search term
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of posts</returns>
        public Task<List<Post>> GetPostsBySearchAsync(string searchTerm, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
        {
            // default values
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return HttpHelper.GetRequestAsync<List<Post>>(_methodPath.SetQueryParam("search", searchTerm)!, embed, useAuth, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get count of posts
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of operation</returns>
        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            var responseHeaders = await HttpHelper.HeadRequestAsync(_methodPath, cancellationToken: cancellationToken).ConfigureAwait(false);
            var (total, _) = HttpHelper.ParsePaginationHeaders(responseHeaders);
            return total;
        }

        /// <summary>
        /// Delete post with force deletion
        /// </summary>
        /// <param name="Id">Post id</param>
        /// <param name="force">force deletion</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of operation</returns>
        public Task<bool> DeleteAsync(int Id, bool force = false, CancellationToken cancellationToken = default)
        {
#pragma warning disable CA1507 // Use nameof to express symbol names
            return HttpHelper.DeleteRequestAsync($"{_methodPath}/{Id}"
                .SetQueryParam("force", force.ToString().ToLower(CultureInfo.InvariantCulture))!,
                cancellationToken: cancellationToken
            );
#pragma warning restore CA1507 // Use nameof to express symbol names
        }

        /// <summary>
        /// Get instance ob object to manipulate with post revisions
        /// </summary>
        /// <param name="postId">ID of parent Post</param>
        /// <returns>Post revisions object</returns>
        public PostRevisions Revisions(int postId)
        {
            return new PostRevisions(ref _httpHelper, postId);
        }

        #endregion Custom
    }
}
