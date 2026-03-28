using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
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

        private const string _methodPath = "media";
        private readonly HttpHelper _httpHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public Media(HttpHelper HttpHelper)
        {
            _httpHelper = HttpHelper;
        }

        #endregion Init

        /// <summary>
        /// Create Media entity with attachment
        /// </summary>
        /// <param name="fileStream">stream with file content</param>
        /// <param name="filename">Name of file in WP Media Library</param>
        /// <param name="mimeType">Override for automatic mime type detection</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created media object</returns>
        public async Task<MediaItem> CreateAsync(Stream fileStream, string filename, string? mimeType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            using StreamContent content = new(fileStream);
            if (string.IsNullOrEmpty(mimeType))
            {
                string extension = filename.Split('.').Last();
                content.Headers.TryAddWithoutValidation("Content-Type", MimeTypeHelper.GetMIMETypeFromExtension(extension));
            }
            else
            {
                content.Headers.TryAddWithoutValidation("Content-Type", mimeType);
            }
            content.Headers.TryAddWithoutValidation("Content-Disposition", $"attachment; filename={filename}");
            return (await _httpHelper.PostRequestAsync<MediaItem>($"{_methodPath}", content, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
        }

        /// <summary>
        /// Create Media entity with attachment
        /// </summary>
        /// <param name="filePath">Local Path to file</param>
        /// <param name="filename">Name of file in WP Media Library</param>
        /// <param name="mimeType">Override for automatic mime type detection</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created media object</returns>
        public async Task<MediaItem> CreateAsync(string filePath, string filename, string? mimeType = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (File.Exists(filePath))
            {
                using StreamContent content = new(File.OpenRead(filePath));
                if (string.IsNullOrEmpty(mimeType))
                {
                    string extension = filename.Split('.').Last();
                    content.Headers.TryAddWithoutValidation("Content-Type", MimeTypeHelper.GetMIMETypeFromExtension(extension));
                }
                else
                {
                    content.Headers.TryAddWithoutValidation("Content-Type", mimeType);
                }
                content.Headers.TryAddWithoutValidation("Content-Disposition", $"attachment; filename={filename}");
                return (await _httpHelper.PostRequestAsync<MediaItem>($"{_methodPath}", content, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
            }
            else
            {
                throw new FileNotFoundException($"{filePath} was not found");
            }
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="ID">Entity Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of operation</returns>
        public Task<bool> DeleteAsync(int ID, CancellationToken cancellationToken = default)
        {
            return _httpHelper.DeleteRequestAsync($"{_methodPath}/{ID}?force=true", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get latest
        /// </summary>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Latest media items</returns>
        public Task<List<MediaItem>> GetAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
        {
            return _httpHelper.GetRequestAsync<List<MediaItem>>($"{_methodPath}", embed, useAuth, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="embed">Include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of all result</returns>
        public async Task<List<MediaItem>> GetAllAsync(bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
        {
            //100 - Max posts per page in WordPress REST API, so this is hack with multiple requests
            List<MediaItem> entities = await _httpHelper.GetRequestAsync<List<MediaItem>>($"{_methodPath}?per_page=100&page=1", embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (_httpHelper.LastResponseHeaders?.Contains("X-WP-TotalPages") == true && Convert.ToInt32(_httpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault(), CultureInfo.InvariantCulture) > 1)
            {
                int totalpages = Convert.ToInt32(_httpHelper.LastResponseHeaders.GetValues("X-WP-TotalPages").FirstOrDefault(), CultureInfo.InvariantCulture);
                for (int page = 2; page <= totalpages; page++)
                {
                    entities.AddRange(await _httpHelper.GetRequestAsync<List<MediaItem>>($"{_methodPath}?per_page=100&page={page}", embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false));
                }
            }
            return entities;
        }

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="embed">include embed info</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Entity by Id</returns>
        public Task<MediaItem> GetByIdAsync(object id, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
        {
            return _httpHelper.GetRequestAsync<MediaItem>($"{_methodPath}/{id}", embed, useAuth, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Create a parametrized query and get a result
        /// </summary>
        /// <param name="queryBuilder">Query builder with specific parameters</param>
        /// <param name="useAuth">Send request with authentication header</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of filtered result</returns>
        public Task<List<MediaItem>> QueryAsync(MediaQueryBuilder queryBuilder, bool useAuth = false, CancellationToken cancellationToken = default)
        {
            return _httpHelper.GetRequestAsync<List<MediaItem>>($"{_methodPath}{queryBuilder?.BuildQuery()}", false, useAuth, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="Entity">Entity object</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated object</returns>
        public async Task<MediaItem> UpdateAsync(MediaItem Entity, CancellationToken cancellationToken = default)
        {
            string entity = JsonSerializer.Serialize(Entity, _httpHelper.JsonSerializerOptions);
            using StringContent postBody = new(entity, Encoding.UTF8, "application/json");
            return (await _httpHelper.PostRequestAsync<MediaItem>($"{_methodPath}/{Entity?.Id}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
        }
    }
}
