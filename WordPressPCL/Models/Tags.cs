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
    public class Tags : ICRUDOperation<Tag>, IEnumerable<Tag>
    {
        #region Init
        private string _defaultPath;
        private Lazy<IEnumerable<Tag>> _posts;
        private HttpHelper _httpHelper;
        public Tags(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            _posts = new Lazy<IEnumerable<Tag>>(() => GetAll().GetAwaiter().GetResult());
        }
        #endregion
        #region Interface Realisation
        public async Task<Tag> Create(Tag Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Tag>($"{_defaultPath}tags", postBody)).Item1;
        }

        public async Task<Tag> Update(Tag Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Tag>($"{_defaultPath}tags/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}tags/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<Tag>> GetAll(bool embed = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<Tag>>($"{_defaultPath}tags", embed).ConfigureAwait(false);
        }

        public IEnumerable<Tag> GetBy(Func<Tag, bool> predicate, bool embed = false)
        {
            return _posts.Value.Where(predicate);
        }

        public async Task<Tag> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<Tag>($"{_defaultPath}tags/{ID}", embed).ConfigureAwait(false);
        }

        public IEnumerator<Tag> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _posts.Value.GetEnumerator();
        }
        #endregion

        #region Custom
        
        #endregion
    }
}
