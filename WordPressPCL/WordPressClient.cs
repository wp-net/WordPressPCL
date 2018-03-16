using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Client;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL
{
    /// <summary>
    ///     Main class containing the wrapper client with all public API endpoints.
    /// </summary>
    public class WordPressClient
    {
        private readonly string _wordPressUri;
        private readonly HttpHelper _httpHelper;
        private readonly string _defaultPath;
        private const string _jwtPath = "jwt-auth/v1/";

        /// <summary>
        /// WordPressUri holds the WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/wp/v2/"
        /// </summary>
		public string WordPressUri
        {
            get { return _wordPressUri; }
        }

        /// <summary>
        /// Function called when a HttpRequest response to WordPress APIs are readed
        /// Executed before trying to convert json content to a TClass object.
        /// </summary>
        public Func<string, string> HttpResponsePreProcessing
        {
            set
            {
                _httpHelper.HttpResponsePreProcessing = value;
            }
        }

        /// <summary>
        /// Serialization/Deserialization settings for Json.NET library
        /// https://www.newtonsoft.com/json/help/html/SerializationSettings.htm
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings
        {
            set
            {
                _httpHelper.JsonSerializerSettings = value;
            }
            get
            {
                return _httpHelper.JsonSerializerSettings;
            }
        }

        /// <summary>
        /// Authentication method
        /// </summary>
        public AuthMethod AuthMethod { get; set; }

        //public string JWToken;
        /// <summary>
        /// Posts client interaction object
        /// </summary>
        public Posts Posts;

        /// <summary>
        /// Comments client interaction object
        /// </summary>
        public Comments Comments;

        /// <summary>
        /// Tags client interaction object
        /// </summary>
        public Tags Tags;

        /// <summary>
        /// Users client interaction object
        /// </summary>
        public Users Users;

        /// <summary>
        /// Media client interaction object
        /// </summary>
        public Client.Media Media;

        /// <summary>
        /// Categories client interaction object
        /// </summary>
        public Categories Categories;

        /// <summary>
        /// Pages client interaction object
        /// </summary>
        public Pages Pages;

        /// <summary>
        /// Taxonomies client interaction object
        /// </summary>
        public Taxonomies Taxonomies;

        /// <summary>
        /// Post Types client interaction object
        /// </summary>
        public PostTypes PostTypes;

        /// <summary>
        /// Post Statuses client interaction object
        /// </summary>
        public PostStatuses PostStatuses;

        /// <summary>
        /// Custom Request client interaction object
        /// </summary>
        public CustomRequest CustomRequest;

        /// <summary>
        ///     The WordPressClient holds all connection infos and provides methods to call WordPress APIs.
        /// </summary>
        /// <param name="uri">URI for WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/"</param>
        /// <param name="defaultPath">Points to the standard API endpoints</param>
        public WordPressClient(string uri, string defaultPath = "wp/v2/")
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (!uri.EndsWith("/"))
            {
                uri += "/";
            }
            _wordPressUri = uri;
            _defaultPath = defaultPath;

            _httpHelper = new HttpHelper(WordPressUri);
            Posts = new Posts(ref _httpHelper, _defaultPath);
            Comments = new Comments(ref _httpHelper, _defaultPath);
            Tags = new Tags(ref _httpHelper, _defaultPath);
            Users = new Users(ref _httpHelper, _defaultPath);
            Media = new Media(ref _httpHelper, _defaultPath);
            Categories = new Categories(ref _httpHelper, _defaultPath);
            Pages = new Pages(ref _httpHelper, _defaultPath);
            Taxonomies = new Taxonomies(ref _httpHelper, _defaultPath);
            PostTypes = new PostTypes(ref _httpHelper, _defaultPath);
            PostStatuses = new PostStatuses(ref _httpHelper, _defaultPath);
            CustomRequest = new CustomRequest(ref _httpHelper);
        }

        #region Settings methods

        /// <summary>
        /// Get site settings
        /// </summary>
        /// <returns>Site settings</returns>
        public Task<Settings> GetSettings()
        {
            return _httpHelper.GetRequest<Settings>($"{_defaultPath}settings", false, true);
        }

        /// <summary>
        /// Update site settings
        /// </summary>
        /// <param name="settings">Settings object</param>
        /// <returns>Updated settings</returns>
        public async Task<Settings> UpdateSettings(Settings settings)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(settings), Encoding.UTF8, "application/json");
            (var setting, HttpResponseMessage response) = await _httpHelper.PostRequest<Settings>($"{_defaultPath}settings", postBody).ConfigureAwait(false);
            return setting;
        }

        #endregion Settings methods

        #region auth methods

        /// <summary>
        /// Perform authentication by JWToken
        /// </summary>
        /// <param name="Username">username</param>
        /// <param name="Password">password</param>
        public async Task RequestJWToken(string Username, string Password)
        {
            var route = $"{_jwtPath}token";
            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", Username),
                    new KeyValuePair<string, string>("password", Password)
                });

            (JWTUser jwtUser, HttpResponseMessage response) = await _httpHelper.PostRequest<JWTUser>(route, formContent, false).ConfigureAwait(false);
            //JWToken = jwtUser?.Token;
            _httpHelper.JWToken = jwtUser?.Token;
        }

        /// <summary>
        /// Forget the JWT Auth Token
        /// </summary>
        public void Logout()
        {
            _httpHelper.JWToken = default(string);
        }

        /// <summary>
        /// Check if token is valid
        /// </summary>
        /// <returns>Result of checking</returns>
        public async Task<bool> IsValidJWToken()
        {
            var route = $"{_jwtPath}token/validate";
            try
            {
                (JWTUser jwtUser, HttpResponseMessage repsonse) = await _httpHelper.PostRequest<JWTUser>(route, null, true).ConfigureAwait(false);
                return repsonse.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sets an exisitng JWToken
        /// </summary>
        /// <param name="token"></param>
        public void SetJWToken(string token)
        {
            _httpHelper.JWToken = token;
        }

        /// <summary>
        /// Gets the JWToken from the client
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            return _httpHelper.JWToken;
        }

        #endregion auth methods
    }
}