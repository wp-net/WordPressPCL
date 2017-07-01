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
    public class Medias : ICRUDOperation<Media>, IEnumerable<Media>
    {
        #region Init
        private string _defaultPath;
        private Lazy<IEnumerable<Media>> _posts;
        private HttpHelper _httpHelper;
        public Medias(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            _posts = new Lazy<IEnumerable<Media>>(() => GetAll().GetAwaiter().GetResult());
        }
        #endregion
        #region Interface Realisation
        public async Task<Media> Create(Media Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Media>($"{_defaultPath}media", postBody)).Item1;
        }

        public async Task<Media> Update(Media Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Media>($"{_defaultPath}media/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}media/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<Media>> GetAll(bool embed = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<Media>>($"{_defaultPath}media", embed).ConfigureAwait(false);
        }

        public IEnumerable<Media> GetBy(Func<Media, bool> predicate, bool embed = false)
        {
            return _posts.Value.Where(predicate);
        }

        public async Task<Media> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<Media>($"{_defaultPath}media/{ID}", embed).ConfigureAwait(false);
        }

        public IEnumerator<Media> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _posts.Value.GetEnumerator();
        }
        #endregion

        #region Custom
        public async Task<HttpResponseMessage> Delete(int ID,bool force=false)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}media/{ID}?force={force}").ConfigureAwait(false);
        }
        #endregion
    }
}
