using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Client;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;
using WordPressPCL.Utility;

namespace WordPressPCL
{
    /// <summary>
    /// Main class containing the wrapper client with all public API endpoints.
    /// </summary>
    public class WordPressClient
    {
        private readonly HttpHelper _httpHelper;
        private const string DEFAULT_PATH = "wp/v2/";

        /// <summary>
        /// WordPressUri holds the WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/wp/v2/"
        /// </summary>
		public Uri WordPressUri { get; private set; }

        /// <summary>
        /// Function called when a HttpRequest response to WordPress APIs are read
        /// Executed before trying to convert json content to a TClass object.
        /// </summary>
        public Func<string, string> HttpResponsePreProcessing
        {
            get => _httpHelper.HttpResponsePreProcessing;
            set => _httpHelper.HttpResponsePreProcessing = value;
        }

        /// <summary>
        /// Serialization/Deserialization settings for Json.NET library
        /// https://www.newtonsoft.com/json/help/html/SerializationSettings.htm
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings
        {
            get => _httpHelper.JsonSerializerSettings;
            set => _httpHelper.JsonSerializerSettings = value;
        }

        /// <summary>
        /// Authentication method
        /// </summary>
        public AuthMethod AuthMethod 
        {
            get => _httpHelper.AuthMethod;
            set 
            { 
                _httpHelper.AuthMethod = value;
                Auth.AuthMethod = value;
            }
        }

        /// <summary>
        /// JWTAuth Plugin
        /// </summary>
        public JWTPlugin JWTPlugin 
        {
            get => Auth.JWTPlugin;
            set => Auth.JWTPlugin = value;
        }

        /// <summary>
        /// The username to be used with the Application Password
        /// </summary>
        public string UserName {
            get => _httpHelper.UserName;
            set => _httpHelper.UserName = value;
        }

        /// <summary>
        /// Auth client interaction object
        /// </summary>
        public Auth Auth { get; private set; }

        //public string JWToken;
        /// <summary>
        /// Posts client interaction object
        /// </summary>
        public Posts Posts { get; private set; }

        /// <summary>
        /// Comments client interaction object
        /// </summary>
        public Comments Comments { get; private set; }

        /// <summary>
        /// Tags client interaction object
        /// </summary>
        public Tags Tags { get; private set; }

        /// <summary>
        /// Users client interaction object
        /// </summary>
        public Users Users { get; private set; }

        /// <summary>
        /// Media client interaction object
        /// </summary>
        public Media Media { get; private set; }

        /// <summary>
        /// Categories client interaction object
        /// </summary>
        public Categories Categories { get; private set; }

        /// <summary>
        /// Pages client interaction object
        /// </summary>
        public Pages Pages { get; private set; }

        /// <summary>
        /// Taxonomies client interaction object
        /// </summary>
        public Taxonomies Taxonomies { get; private set; }

        /// <summary>
        /// Post Types client interaction object
        /// </summary>
        public PostTypes PostTypes { get; private set; }

        /// <summary>
        /// Post Statuses client interaction object
        /// </summary>
        public PostStatuses PostStatuses { get; private set; }

        /// <summary>
        /// Custom Request client interaction object
        /// </summary>
        public CustomRequest CustomRequest { get; private set; }

        /// <summary>
        /// The WordPressClient holds all connection infos and provides methods to call WordPress APIs.
        /// </summary>
        /// <param name="uri">URI for WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/"</param>
        /// <param name="defaultPath">Relative path to standard API endpoints, defaults to "wp/v2/"</param>
        public WordPressClient(Uri uri, string defaultPath = DEFAULT_PATH)
        {
            WordPressUri = uri ?? throw new ArgumentNullException(nameof(uri));
            _httpHelper = new HttpHelper(WordPressUri, defaultPath);
            SetupSubClients(_httpHelper);
        }

        /// <summary>
        /// The WordPressClient holds all connection infos and provides methods to call WordPress APIs.
        /// </summary>
        /// <param name="uri">URI for WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/"</param>
        /// <param name="defaultPath">Relative path to standard API endpoints, defaults to "wp/v2/"</param>
        public WordPressClient(string uri, string defaultPath = DEFAULT_PATH): this(new Uri(uri), defaultPath)
        {
        }


        /// <summary>
        /// The WordPressClient holds all connection infos and provides methods to call WordPress APIs.
        /// </summary>
        /// <param name="httpClient">HttpClient with BaseAddress set which will be used for sending requests to the WordPress API endpoint.</param>
        /// <param name="defaultPath">Relative path to standard API endpoints, defaults to "wp/v2/"</param>
        public WordPressClient(HttpClient httpClient, string defaultPath = DEFAULT_PATH)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }
            WordPressUri = httpClient.BaseAddress;
            _httpHelper = new HttpHelper(httpClient, defaultPath);
            SetupSubClients(_httpHelper);
        }

        private void SetupSubClients(HttpHelper httpHelper)
        {
            Auth = new Auth(httpHelper);
            Posts = new Posts(httpHelper);
            Comments = new Comments(httpHelper);
            Tags = new Tags(httpHelper);
            Users = new Users(httpHelper);
            Media = new Media(httpHelper);
            Categories = new Categories(httpHelper);
            Pages = new Pages(httpHelper);
            Taxonomies = new Taxonomies(httpHelper);
            PostTypes = new PostTypes(httpHelper);
            PostStatuses = new PostStatuses(httpHelper);
            CustomRequest = new CustomRequest(httpHelper);
        }

        #region Settings methods

        /// <summary>
        /// Get site settings
        /// </summary>
        /// <returns>Site settings</returns>
        public Task<Settings> GetSettings()
        {
            return _httpHelper.GetRequestAsync<Settings>("settings", false, true);
        }

        /// <summary>
        /// Update site settings
        /// </summary>
        /// <param name="settings">Settings object</param>
        /// <returns>Updated settings</returns>
        public async Task<Settings> UpdateSettingsAsync(Settings settings)
        {
            using var postBody = new StringContent(JsonConvert.SerializeObject(settings), Encoding.UTF8, "application/json");
            (var setting, _) = await _httpHelper.PostRequestAsync<Settings>("settings", postBody).ConfigureAwait(false);
            return setting;
        }

        #endregion Settings methods

    }
}