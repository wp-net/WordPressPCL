﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using WordPressPCL.Client;
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
        /// Settings client interaction object
        /// </summary>
        public Settings Settings { get; private set; }

        /// <summary>
        /// Plugins client interaction object
        /// </summary>
        public Plugins Plugins { get; private set; }

        /// <summary>
        /// Plugins client interaction object
        /// </summary>
        public Themes Themes { get; private set; }

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
        public WordPressClient(
            string uri,
            string defaultPath = DEFAULT_PATH): this(new Uri(uri), defaultPath)
        {
        }


		/// <summary>
		/// The WordPressClient holds all connection infos and provides methods to call WordPress APIs.
		/// </summary>
		/// <param name="httpClient">HttpClient with BaseAddress set which will be used for sending requests to the WordPress API endpoint.</param>
		/// <param name="defaultPath">Relative path to standard API endpoints, defaults to "wp/v2/"</param>
		/// <param name="uri">URI for WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/".  Use this if the BaseAddress of the httpClient is not set.</param>
		public WordPressClient(HttpClient httpClient, string defaultPath = DEFAULT_PATH, Uri uri = null)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }
            WordPressUri = uri ?? httpClient.BaseAddress;
            _httpHelper = new HttpHelper(httpClient, defaultPath, uri);
            SetupSubClients(_httpHelper);
        }

        private void SetupSubClients(HttpHelper httpHelper) {
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
            Settings = new Settings(httpHelper);
            Plugins = new Plugins(httpHelper);
            Themes = new Themes(httpHelper);
        }

    }
}