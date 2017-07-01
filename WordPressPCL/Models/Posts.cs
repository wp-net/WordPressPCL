using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models.Interfaces;
using WordPressPCL.Utility;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    public class Posts : ICRUDOperation<Post>, IEnumerable<Post>
    {
        #region Init
        private string _defaultPath;
        private Lazy<IEnumerable<Post>> _posts;
        private HttpHelper _httpHelper;
        public Posts(ref HttpHelper HttpHelper,string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            _posts = new Lazy<IEnumerable<Post>>(() => GetAll().GetAwaiter().GetResult());
        }
        #endregion
        #region Interface Realisation
        public async Task<Post> Create(Post Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Post>($"{_defaultPath}posts", postBody)).Item1;
        }

        public async Task<Post> Update(Post Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Post>($"{_defaultPath}posts/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}posts/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetAll(bool embed=false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<Post> posts = new List<Post>();
            List<Post> posts_page = new List<Post>();
            int page = 1;
            do
            {
                posts_page = (await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}posts?per_page=100&page={page++}", embed).ConfigureAwait(false))?.ToList<Post>();
                if (posts_page != null) { posts.AddRange(posts_page); }
                
            } while (posts_page!=null);
            
            return posts;
        }

        public IEnumerable<Post> GetBy(Func<Post, bool> predicate, bool embed=false)
        {
            return _posts.Value.Where(predicate);
        }

        public async Task<Post> GetByID(int ID, bool embed=false)
        {
            return await _httpHelper.GetRequest<Post>($"{_defaultPath}posts/{ID}", embed).ConfigureAwait(false);
        }

        public IEnumerator<Post> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _posts.Value.GetEnumerator();
        }
        #endregion

        #region Custom
        public async Task<IEnumerable<Post>> GetStickyPosts(bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}posts?sticky=true", embed).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetStickyPosts(QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>(builder.SetRootUrl($"{_defaultPath}posts?sticky=true").ToString(), false).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetPostsByCategory(int categoryId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}posts?categories={categoryId}", embed).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetPostsByCategory(int categoryId, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>(builder.SetRootUrl($"{_defaultPath}posts?categories={categoryId}").ToString(), false).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetPostsByTag(int tagId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}posts?tags={tagId}", embed).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetPostsByTag(int tagId, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>(builder.SetRootUrl($"{_defaultPath}posts?tags={tagId}").ToString(), false).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthor(int authorId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}posts?author={authorId}", embed).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthor(int authorId, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>(builder.SetRootUrl($"{_defaultPath}posts?author={authorId}").ToString(), false).ConfigureAwait(false);
        }
        public async Task<IEnumerable<Post>> GetPostsBySearch(string searchTerm, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>($"{_defaultPath}posts?search={searchTerm}", embed).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Post>> GetPostsBySearch(string searchTerm, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Post>>(builder.SetRootUrl($"{_defaultPath}posts?search={searchTerm}").ToString(), false).ConfigureAwait(false);
        }
        public async Task<IEnumerable<Post>> GetBy(QueryBuilder builder)
        {
            return await _httpHelper.GetRequest<IEnumerable<Post>>(builder.SetRootUrl($"{_defaultPath}posts").ToString(), false).ConfigureAwait(false);
        }
        public async Task<HttpResponseMessage> Delete(int ID,bool force=false)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}posts/{ID}?force={force}").ConfigureAwait(false);
        }
        #endregion
    }
}
