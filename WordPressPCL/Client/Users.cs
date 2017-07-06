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
    public class Users : ICRUDOperationAsync<User>
    {
        #region Init
        private string _defaultPath;
        private const string _methodPath = "users";
        private HttpHelper _httpHelper;
        public Users(ref HttpHelper HttpHelper, string defaultPath)
        {
            _defaultPath = defaultPath;
            _httpHelper = HttpHelper;
            
        }
        #endregion
        #region Interface Realisation
        public async Task<User> Create(User Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<User>($"{_defaultPath}{_methodPath}", postBody)).Item1;
        }

        public async Task<User> Update(User Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<User>($"{_defaultPath}{_methodPath}/{Entity.Id}", postBody)).Item1;
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}").ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> GetAll(bool embed = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<User> users = new List<User>();
            List<User> users_page = new List<User>();
            int page = 1;
            do
            {
                users_page = (await _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed).ConfigureAwait(false))?.ToList<User>();
                if (users_page != null) { users.AddRange(users_page); }

            } while (users_page != null);

            return users;
            //return await _httpHelper.GetRequest<IEnumerable<User>>($"{_defaultPath}{_methodPath}", embed).ConfigureAwait(false);
        }

        

        public async Task<User> GetByID(int ID, bool embed = false)
        {
            return await _httpHelper.GetRequest<User>($"{_defaultPath}{_methodPath}/{ID}", embed).ConfigureAwait(false);
        }

        
        #endregion

        #region Custom
        public async Task<User> GetCurrentUser()
        {
            return await _httpHelper.GetRequest<User>($"{_defaultPath}{_methodPath}/me", true, true).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> Delete(int ID,int ReassignUserID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?reassign={ReassignUserID}").ConfigureAwait(false);
        }
        public async Task<HttpResponseMessage> Delete(int ID, User ReassignUser)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?reassign={ReassignUser.Id}").ConfigureAwait(false);
        }
        #endregion
    }
}
