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
    public class Tags : ICRUDOperationAsync<Tag>
    {
        #region Init
        private string _defaultPath;
        private const string _methodPath = "tags";
        
        private HttpHelper _httpHelper;
        public Tags(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            
        }
        #endregion
        #region Interface Realisation
        public async Task<Tag> Create(Tag Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Tag>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }

        public async Task<Tag> Update(Tag Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<Tag>($"{_defaultPath}{_methodPath}/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<Tag>> GetAll(bool embed = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<Tag> tags = new List<Tag>();
            List<Tag> tags_page = new List<Tag>();
            int page = 1;
            do
            {
                tags_page = (await _httpHelper.GetRequest<IEnumerable<Tag>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed).ConfigureAwait(false))?.ToList<Tag>();
                if (tags_page != null) { tags.AddRange(tags_page); }

            } while (tags_page != null);

            return tags;
            //return await _httpHelper.GetRequest<IEnumerable<Tag>>($"{_defaultPath}{_methodPath}", embed).ConfigureAwait(false);
        }

       

        public async Task<Tag> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<Tag>($"{_defaultPath}{_methodPath}/{ID}", embed).ConfigureAwait(false);
        }

        #endregion

        #region Custom
        
        #endregion
    }
}
