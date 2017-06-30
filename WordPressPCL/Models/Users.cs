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
    public class Users : ICRUDOperation<User>
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
            throw new NotImplementedException("Create method is not supported for Users");
        }

        public async Task<User> Update(User Entity)
        {
            throw new NotImplementedException("Update method is not supported for Users");
        }

        public async Task<HttpResponseMessage> Delete(int ID)
        {
            throw new NotImplementedException("Delete is not supported for Users");
        }

        public async Task<IEnumerable<User>> GetAll(bool embed = false)
        {
            return await _httpHelper.GetRequest<User[]>($"{_defaultPath}users", embed).ConfigureAwait(false);
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
        #endregion
    }
}
