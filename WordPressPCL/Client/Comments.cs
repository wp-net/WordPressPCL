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
    public class Comments : ICRUDOperationAsync<Comment>,IEnumerable<Comment>
    {
        #region Init
        private string _defaultPath;
        private const string _methodPath = "comments";
        private Lazy<IEnumerable<Comment>> _posts;
        private HttpHelper _httpHelper;
        public Comments(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            _posts = new Lazy<IEnumerable<Comment>>(() => GetAll().GetAwaiter().GetResult());
        }
        #endregion
        #region Interface Realisation
        public async Task<Comment> Create(Comment Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Comment>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }

        public async Task<Comment> Update(Comment Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Comment>($"{_defaultPath}{_methodPath}/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<Comment>> GetAll(bool embed = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<Comment> comments = new List<Comment>();
            List<Comment> comments_page = new List<Comment>();
            int page = 1;
            do
            {
                comments_page = (await _httpHelper.GetRequest<IEnumerable<Comment>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed).ConfigureAwait(false))?.ToList<Comment>();
                if (comments_page != null) { comments.AddRange(comments_page); }

            } while (comments_page != null);

            return comments;
            //return await _httpHelper.GetRequest<IEnumerable<Comment>>($"{_defaultPath}{_methodPath}", embed).ConfigureAwait(false);
        }

        public IEnumerable<Comment> GetBy(Func<Comment, bool> predicate, bool embed = false)
        {
            return _posts.Value.Where(predicate);
        }

        public async Task<Comment> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<Comment>($"{_defaultPath}{_methodPath}/{ID}", embed).ConfigureAwait(false);
        }

        public IEnumerator<Comment> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _posts.Value.GetEnumerator();
        }
        #endregion

        #region Custom
        public async Task<IEnumerable<Comment>> GetCommentsForPost(string PostID, bool embed = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<Comment>>($"{_defaultPath}{_methodPath}?post={PostID}", embed);
        }
        public async Task<HttpResponseMessage> Delete(int ID, bool force=false)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force={force}").ConfigureAwait(false);
        }
        #endregion
    }
}
