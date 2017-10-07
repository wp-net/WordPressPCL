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

        /// <summary>
        /// WordPressUri holds the WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/wp/v2/"
        /// </summary>
		public string WordPressUri
        {
            get { return _wordPressUri; }
        }

        private const string defaultPath = "wp/v2/";
        private const string jwtPath = "jwt-auth/v1/";

        /*public string Username { get; set; }
        public string Password { get; set; }*/

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
        public WordPressClient(string uri)
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
            _httpHelper = new HttpHelper(WordPressUri);
            Posts = new Posts(ref _httpHelper, defaultPath);
            Comments = new Comments(ref _httpHelper, defaultPath);
            Tags = new Tags(ref _httpHelper, defaultPath);
            Users = new Users(ref _httpHelper, defaultPath);
            Media = new Media(ref _httpHelper, defaultPath);
            Categories = new Categories(ref _httpHelper, defaultPath);
            Pages = new Pages(ref _httpHelper, defaultPath);
            Taxonomies = new Taxonomies(ref _httpHelper, defaultPath);
            PostTypes = new PostTypes(ref _httpHelper, defaultPath);
            PostStatuses = new PostStatuses(ref _httpHelper, defaultPath);
            CustomRequest = new CustomRequest(ref _httpHelper);
        }

        #region Settings methods

        /// <summary>
        /// Get site settings
        /// </summary>
        /// <returns>Site settings</returns>
        public Task<Settings> GetSettings()
        {
            return _httpHelper.GetRequest<Settings>($"{defaultPath}settings", false, true);
        }

        /// <summary>
        /// Update site settings
        /// </summary>
        /// <param name="settings">Settings object</param>
        /// <returns>Updated settings</returns>
        public async Task<Settings> UpdateSettings(Settings settings)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(settings).ToString(), Encoding.UTF8, "application/json");
            (var setting, HttpResponseMessage response) = await _httpHelper.PostRequest<Settings>($"{defaultPath}settings", postBody);
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
            var route = $"{jwtPath}token";
            using (var client = new HttpClient())
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", Username),
                    new KeyValuePair<string, string>("password", Password)
                });

                (JWTUser jwtUser, HttpResponseMessage response) = await _httpHelper.PostRequest<JWTUser>(route, formContent, false);
                //JWToken = jwtUser?.Token;
                _httpHelper.JWToken = jwtUser?.Token;
            }
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
            var route = $"{jwtPath}token/validate";
            using (var client = new HttpClient())
            {
                (JWTUser jwtUser, HttpResponseMessage repsonse) = await _httpHelper.PostRequest<JWTUser>(route, null, true);
                return repsonse.IsSuccessStatusCode;
            }
        }

        #endregion auth methods
    }
}