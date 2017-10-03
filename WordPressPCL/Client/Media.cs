using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Interfaces;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Media endpoint WP REST API
    /// </summary>
    public class Media : IUpdateOperation<MediaItem>, IReadOperation<MediaItem>, IDeleteOperation, IQueryOperation<MediaItem, MediaQueryBuilder>
    {
        #region Init

        private string _defaultPath;
        private const string _methodPath = "media";
        private HttpHelper _httpHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public Media(ref HttpHelper HttpHelper, string defaultPath)
        {
            _httpHelper = HttpHelper;
            _defaultPath = defaultPath;
        }

        #endregion Init

        /// <summary>
        /// Create Media entity with attachment
        /// </summary>
        /// <param name="fileStream">stream with file content</param>
        /// <param name="filename">Name of file in WP Media Library</param>
        /// <returns>Created media object</returns>
        public async Task<MediaItem> Create(Stream fileStream, string filename)
        {
            StreamContent content = new StreamContent(fileStream);
            string extension = filename.Split('.').Last();
            content.Headers.TryAddWithoutValidation("Content-Type", MimeTypeHelper.GetMIMETypeFromExtension(extension));
            content.Headers.TryAddWithoutValidation("Content-Disposition", $"attachment; filename={filename}");
            return (await _httpHelper.PostRequest<MediaItem>($"{_defaultPath}{_methodPath}", content)).Item1;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="ID">Entity Id</param>
        /// <returns>Result of operation</returns>
        public async Task<HttpResponseMessage> Delete(int ID)
        {
            return await _httpHelper.DeleteRequest($"{_defaultPath}{_methodPath}/{ID}?force=true").ConfigureAwait(false);
        }

        /// <summary>
        /// Get latest
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Latest media items</returns>
        public async Task<IEnumerable<MediaItem>> Get(bool embed = false, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<MediaItem>>($"{_defaultPath}{_methodPath}", embed, useAuth).ConfigureAwait(false);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of all result</returns>
        public async Task<IEnumerable<MediaItem>> GetAll(bool embed = false, bool useAuth = false)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<MediaItem> entities = new List<MediaItem>();
            List<MediaItem> entities_page = new List<MediaItem>();
            int page = 1;
            do
            {
                entities_page = (await _httpHelper.GetRequest<IEnumerable<MediaItem>>($"{_defaultPath}{_methodPath}?per_page=100&page={page++}", embed, useAuth).ConfigureAwait(false))?.ToList<MediaItem>();
                if (entities_page != null && entities_page.Count > 0) { entities.AddRange(entities_page); }
            } while (entities_page != null && entities_page.Count > 0);

            return entities;
        }

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>Entity by Id</returns>
        public async Task<MediaItem> GetByID(object ID, bool embed = false, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<MediaItem>($"{_defaultPath}{_methodPath}/{ID}", embed, useAuth).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Query builder with specific parameters</param>
        /// <param name="useAuth">Send request with authenication header</param>
        /// <returns>List of filtered result</returns>
        public async Task<IEnumerable<MediaItem>> Query(MediaQueryBuilder queryBuilder, bool useAuth = false)
        {
            return await _httpHelper.GetRequest<IEnumerable<MediaItem>>($"{_defaultPath}{_methodPath}{queryBuilder.BuildQueryURL()}", false, useAuth).ConfigureAwait(false);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="Entity">Entity object</param>
        /// <returns>Updated object</returns>
        public async Task<MediaItem> Update(MediaItem Entity)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<MediaItem>($"{_defaultPath}{_methodPath}/{(Entity as Base).Id}", postBody)).Item1;
        }
    }
}