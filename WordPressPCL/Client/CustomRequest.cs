using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Class to create custom requests
    /// </summary>
    public class CustomRequest
    {
        private readonly HttpHelper _httpHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpHelper">HttpHelper class to operate with Http methods</param>
        public CustomRequest(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        /// <summary>
        /// Create object
        /// </summary>
        /// <typeparam name="TInput">type of input object</typeparam>
        /// <typeparam name="TOutput">type of result object</typeparam>
        /// <param name="route">path to exec request</param>
        /// <param name="Entity">object for creation</param>
        /// <param name="ignoreDefaultPath">request should prepend default path to route, defaults to true</param>
        /// <returns>Created object</returns>
        public async Task<TOutput> CreateAsync<TInput, TOutput>(string route, TInput Entity, bool ignoreDefaultPath = true) where TOutput : class
        {
            string entity = _httpHelper.JsonSerializerSettings == null ? JsonConvert.SerializeObject(Entity) : JsonConvert.SerializeObject(Entity, _httpHelper.JsonSerializerSettings);
            using StringContent sc = new(entity, Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequestAsync<TOutput>(route, sc, true, ignoreDefaultPath).ConfigureAwait(false)).Item1;
        }

        /// <summary>
        /// Delete object
        /// </summary>
        /// <param name="route">path to exec delete request</param>
        /// <param name="ignoreDefaultPath">request should prepend default path to route, defaults to true</param>
        /// <returns>Result of deletion</returns>
        public Task<bool> DeleteAsync(string route, bool ignoreDefaultPath = true)
        {
            return _httpHelper.DeleteRequestAsync(route, true, ignoreDefaultPath);
        }

        /// <summary>
        /// Get object/s
        /// </summary>
        /// <typeparam name="TClass">type of object</typeparam>
        /// <param name="route">path to exec request</param>
        /// <param name="embed">is get embed params</param>
        /// <param name="useAuth">i use auth</param>
        /// <param name="ignoreDefaultPath">request should prepend default path to route, defaults to true</param>
        /// <returns>List of objects</returns>
        public Task<TClass> GetAsync<TClass>(string route, bool embed = false, bool useAuth = false, bool ignoreDefaultPath = true) where TClass : class
        {
            return _httpHelper.GetRequestAsync<TClass>(route, embed, useAuth, ignoreDefaultPath);
        }

        /// <summary>
        /// Update object
        /// </summary>
        /// <typeparam name="TInput">type of input object</typeparam>
        /// <typeparam name="TOutput">type of result object</typeparam>
        /// <param name="route">path to exec request</param>
        /// <param name="Entity">object for update</param>
        /// <param name="ignoreDefaultPath">request should prepend default path to route, defaults to true</param>
        /// <returns>Updated object</returns>
        public Task<TOutput> UpdateAsync<TInput, TOutput>(string route, TInput Entity, bool ignoreDefaultPath = true) where TOutput : class
        {
            return CreateAsync<TInput, TOutput>(route, Entity, ignoreDefaultPath);
        }
    }
}