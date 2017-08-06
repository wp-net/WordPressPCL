using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Posts endpoint WP REST API
    /// </summary>
    public class Posts : ICRUDOperationAsync<Post>
    {
        #region Init
        private string _defaultPath;
        private const string _methodPath = "posts";
        private HttpHelper _httpHelper;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public Posts(ref HttpHelper HttpHelper,string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            
        }
        #endregion
        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Posts query builder with specific parameters</param>
        /// <returns>List of filtered posts</returns>
        public async Task<IEnumerable<Post>> Query(PostsQueryBuilder queryBuilder)
        {
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}{_methodPath}{queryBuilder.BuildQueryURL()}",false).ConfigureAwait(false);
        }
        #region Interface Realisation
        /// <summary>
        /// Create Post
        /// </summary>
        /// <param name="Entity">Post object</param>
        /// <returns>Created post object</returns>
        public async Task<Post> Create(Post Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Post>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }

        /// <summary>
        /// Update Post
        /// </summary>
        /// <param name="Entity">Post object</param>
        /// <returns>Updated Post object</returns>
        public async Task<Post> Update(Post Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Post>($"{_defaultPath}{_methodPath}/{Entity.Id}", postBody)).Item1;
        }
        /// <summary>
        /// Delete Post
        /// </summary>
        /// <param name="ID">Post Id</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}").ConfigureAwait(false);
        }
        /// <summary>
        /// Get All Posts
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <returns>List of all Posts</returns>
        public async Task<IEnumerable<Post>> GetAll(bool embed=false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<Post> posts = new List<Post>();
            List<Post> posts_page = new List<Post>();
            int page = 1;
            do
            {
                posts_page = (await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed).ConfigureAwait(false))?.ToList<Post>();
                if (posts_page != null && posts_page.Count>0) { posts.AddRange(posts_page); }
                
            } while (posts_page!=null && posts_page.Count > 0);
            
            return posts;
        }


        /// <summary>
        /// Get Post by Id
        /// </summary>
        /// <param name="ID">Post ID</param>
        /// <param name="embed">include embed info</param>
        /// <returns>Post by Id</returns>
        public async Task<Post> GetByID(int ID, bool embed=false)
        {
            return await _httpHelper.GetRequest<Post>($"{_defaultPath}{_methodPath}/{ID}", embed).ConfigureAwait(false);
        }
        #endregion

        #region Custom
        /// <summary>
        /// Get sticky posts
        /// </summary>
        /// <param name="embed">includ embed info</param>
        /// <returns>List of posts</returns>
        public async Task<IEnumerable<Post>> GetStickyPosts(bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}{_methodPath}?sticky=true", embed).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Get posts by category
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <param name="embed">includ embed info</param>
        /// <returns>List of posts</returns>
        public async Task<IEnumerable<Post>> GetPostsByCategory(int categoryId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}{_methodPath}?categories={categoryId}", embed).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Get posts by tag
        /// </summary>
        /// <param name="tagId">Tag Id</param>
        /// <param name="embed">includ embed info</param>
        /// <returns>List of posts</returns>
        public async Task<IEnumerable<Post>> GetPostsByTag(int tagId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}{_methodPath}?tags={tagId}", embed).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Get posts by its author
        /// </summary>
        /// <param name="authorId">Author id</param>
        /// <param name="embed">includ embed info</param>
        /// <returns>List of posts</returns>
        public async Task<IEnumerable<Post>> GetPostsByAuthor(int authorId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}{_methodPath}?author={authorId}", embed).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Get posts by search term
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="embed">include embed info</param>
        /// <returns>List of posts</returns>
        public async Task<IEnumerable<Post>> GetPostsBySearch(string searchTerm, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}{_methodPath}?search={searchTerm}", embed).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Delete post with force deletion
        /// </summary>
        /// <param name="ID">Post id</param>
        /// <param name="force">force deletion</param>
        /// <returns>Result of opertion</returns>
        public async Task<HttpResponseMessage> Delete(int ID,bool force=false)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force={force}").ConfigureAwait(false);
        }
        #endregion
    }
}
