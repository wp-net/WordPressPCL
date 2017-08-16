using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Media endpoint WP REST API
    /// </summary>
    public class Media : CRUDOperation<MediaItem, MediaQueryBuilder>
    {
        #region Init

        private new const string _methodPath = "media";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public Media(ref HttpHelper HttpHelper, string defaultPath) : base(ref HttpHelper, defaultPath, _methodPath, true)
        {
        }

        #endregion Init

        private new MediaItem Create(MediaItem Entity)
        {
            throw new InvalidOperationException("Use Create(fileStream,fileName) instead of this method");
            /*var postBody = new StringContent(JsonConvert.SerializeObject(Entity).ToString(), Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequest<MediaItem>($"{_defaultPath}{_methodPath}", postBody)).Item1;*/
        }

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
    }
}