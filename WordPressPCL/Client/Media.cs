using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Utility;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using WordPressPCL.Models;

namespace WordPressPCL.Client
{
    public class Media : ICRUDOperationAsync<MediaItem>
    {
        #region Init
        private string _defaultPath;
        private const string _methodPath = "media";
        private HttpHelper _httpHelper;
        public Media(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
        }
        #endregion
        #region Interface Realisation
        public async Task<MediaItem> Create(MediaItem Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<MediaItem>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }

        public async Task<MediaItem> Update(MediaItem Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<MediaItem>($"{_defaultPath}{_methodPath}/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<MediaItem>> GetAll(bool embed = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            var medias = new List<MediaItem>();
            var medias_page = new List<MediaItem>();
            int page = 1;
            do
            {
                medias_page = (await _httpHelper.GetRequest<IEnumerable<MediaItem>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed).ConfigureAwait(false))?.ToList<MediaItem>();
                if (medias_page != null) { medias.AddRange(medias_page); }

            } while (medias_page != null);

            return medias;
            //return await _httpHelper.GetRequest<IEnumerable<Media>>($"{_defaultPath}{_methodPath}", embed).ConfigureAwait(false);
        }

        public IEnumerable<MediaItem> GetBy(Func<MediaItem, bool> predicate, bool embed = false)
        {
            // TODO
            return null;
        }

        public async Task<MediaItem> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<MediaItem>($"{_defaultPath}{_methodPath}/{ID}", embed).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> Delete(int ID,bool force=false)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force={force}").ConfigureAwait(false);
        }
        #endregion
    }
}
