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
    public class Users : ICRUDOperation<User>,IEnumerable<User>
    {
        #region Init
        private string _defaultPath;
        private Lazy<IEnumerable<User>> _posts;
        private HttpHelper _httpHelper;
        public Users(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            _posts = new Lazy<IEnumerable<User>>(() => GetAll().GetAwaiter().GetResult());
        }
        #endregion
        #region Interface Realisation
        public async Task<User> Create(User Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<User>($"{_defaultPath}users", postBody)).Item1;
        }

        public async Task<User> Update(User Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<User>($"{_defaultPath}users/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}users/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> GetAll(bool embed = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}users", embed).ConfigureAwait(false);
        }

        public IEnumerable<User> GetBy(Func<User, bool> predicate, bool embed = false)
        {
            return _posts.Value.Where(predicate);
        }

        public async Task<User> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<User>($"{_defaultPath}users/{ID}", embed).ConfigureAwait(false);
        }

        public IEnumerator<User> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _posts.Value.GetEnumerator();
        }
        #endregion

        #region Custom
        public async Task<User> GetCurrentUser()
        {
            return await _httpHelper.GetRequest<User>($"{_defaultPath}users/me", true, true).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> Delete(int ID,int ReassignUserID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}users/{ID}?reassign={ReassignUserID}").ConfigureAwait(false);
        }
        public async Task<HttpResponseMessage> Delete(int ID, User ReassignUser)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}users/{ID}?reassign={ReassignUser.Id}").ConfigureAwait(false);
        }
        #endregion
    }
}
