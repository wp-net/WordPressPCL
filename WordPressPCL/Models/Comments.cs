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
    public class Comments : ICRUDOperation<Comment>
    {
        #region Init
        private string _defaultPath;
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
            return (await _httpHelper.PostRequest<Comment>($"{_defaultPath}comments", postBody)).Item1;
        }

        public async Task<Comment> Update(Comment Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Comment>($"{_defaultPath}comments/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}comments/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<Comment>> GetAll(bool embed = false)
        {
            return await _httpHelper.GetRequest<Comment[]>($"{_defaultPath}comments", embed).ConfigureAwait(false);
        }

        public IEnumerable<Comment> GetBy(Func<Comment, bool> predicate, bool embed = false)
        {
            return _posts.Value.Where(predicate);
        }

        public async Task<Comment> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<Comment>($"{_defaultPath}comments/{ID}", embed).ConfigureAwait(false);
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
            return await _httpHelper.GetRequest<Comment[]>($"{_defaultPath}comments?post={PostID}", embed);
        }
        #endregion
    }
}
