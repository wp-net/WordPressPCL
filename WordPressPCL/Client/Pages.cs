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
    public class Pages : ICRUDOperationAsync<Page>, IEnumerable<Page>
    {
        #region Init
        private string _defaultPath;
        private const string _methodPath = "pages";
        private Lazy<IEnumerable<Page>> _posts;
        private HttpHelper _httpHelper;
        public Pages(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            _posts = new Lazy<IEnumerable<Page>>(() => GetAll().GetAwaiter().GetResult());
        }
        #endregion
        #region Interface Realisation
        public async Task<Page> Create(Page Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Page>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }

        public async Task<Page> Update(Page Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Page>($"{_defaultPath}{_methodPath}/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<Page>> GetAll(bool embed = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<Page> posts = new List<Page>();
            List<Page> posts_page = new List<Page>();
            int page = 1;
            do
            {
                posts_page = (await _httpHelper.GetRequest<IEnumerable<Page>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed).ConfigureAwait(false))?.ToList<Page>();
                if (posts_page != null) { posts.AddRange(posts_page); }

            } while (posts_page != null);

            return posts;
        }

        public IEnumerable<Page> GetBy(Func<Page, bool> predicate, bool embed = false)
        {
            return _posts.Value.Where(predicate);
        }

        public async Task<Page> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<Page>($"{_defaultPath}{_methodPath}/{ID}", embed).ConfigureAwait(false);
        }

        public IEnumerator<Page> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _posts.Value.GetEnumerator();
        }
        #endregion

        #region Custom
        

        public async Task<IEnumerable<Page>> GetPagesByAuthor(int authorId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Page>>($"{_defaultPath}{_methodPath}?author={authorId}", embed).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Page>> GetPagesByAuthor(int authorId, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Page>>(builder.SetRootUrl($"{_defaultPath}{_methodPath}?author={authorId}").ToString(), false).ConfigureAwait(false);
        }
        public async Task<IEnumerable<Page>> GetPagesBySearch(string searchTerm, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Page>>($"{_defaultPath}{_methodPath}?search={searchTerm}", embed).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Page>> GetPagesBySearch(string searchTerm, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await _httpHelper.GetRequest<IEnumerable<Page>>(builder.SetRootUrl($"{_defaultPath}{_methodPath}?search={searchTerm}").ToString(), false).ConfigureAwait(false);
        }
        public async Task<IEnumerable<Page>> GetBy(QueryBuilder builder)
        {
            return await _httpHelper.GetRequest<IEnumerable<Page>>(builder.SetRootUrl($"{_defaultPath}{_methodPath}").ToString(), false).ConfigureAwait(false);
        }
        public async Task<HttpResponseMessage> Delete(int ID, bool force = false)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force={force}").ConfigureAwait(false);
        }
        #endregion
    }
}
