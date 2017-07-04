using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using System.Collections;

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
        public AuthMethod AuthMethod { get; set; }
        //public string JWToken;
        public Posts Posts;
        public Comments Comments;
        public Tags Tags;
        public Users Users;
        public Medias Medias;
        public Categories Categories;
        public Pages Pages;


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
            Medias = new Medias(ref _httpHelper, defaultPath);
            Categories = new Categories(ref _httpHelper, defaultPath);
            Pages = new Pages(ref _httpHelper, defaultPath);
        }

       

        #region Settings methods
        public async Task<Settings> GetSettings()
        {
            return await _httpHelper.GetRequest<Settings>($"{defaultPath}settings", false, true).ConfigureAwait(false);
        }
        public async Task<Settings> UpdateSettings(Settings settings)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(settings).ToString(), Encoding.UTF8, "application/json");
            (var setting, HttpResponseMessage response) = await _httpHelper.PostRequest<Settings>($"{defaultPath}settings", postBody);
            return setting;
        }
        #endregion

        #region auth methods

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

        public async Task<bool> IsValidJWToken()
        {
            var route = $"{jwtPath}token/validate";
            using (var client = new HttpClient())
            {
                (JWTUser jwtUser, HttpResponseMessage repsonse) = await _httpHelper.PostRequest<JWTUser>(route, null, true);
                return repsonse.IsSuccessStatusCode;
            }
        }

        #endregion

        #region internal http methods



        #endregion
    }
}