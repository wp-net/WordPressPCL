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

namespace WordPressPCL
{

	/// <summary>
    ///     Main class containing the wrapper client with all public API endpoints.
    /// </summary>
    public class WordPressClient
	{
        private readonly string _wordPressUri;
        /// <summary>
        /// WordPressUri holds the WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/wp/v2/"
        /// </summary>
		public string WordPressUri
		{
			get { return _wordPressUri; }
		}

        private const string defaultPath = "wp/v2/";
        private const string jwtPath = "jwt-auth/v1/";

        public String Username { get; set; }
        public String Password { get; set; }
        public AuthMethod AuthMethod { get; set; }
        public String JWToken { get; set; }

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
        }

        #region Post methods 
        public async Task<IList<Post>> ListPosts(bool embed = false)
		{
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>($"{defaultPath}posts", embed).ConfigureAwait(false);
		}

		public async Task<Post> GetPost(String id, bool embed = false)
		{
			return await GetRequest<Post>($"{defaultPath}posts/{id}", embed).ConfigureAwait(false);
		}

        public async Task<Post> CreatePost(Post postObject)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(postObject).ToString(), Encoding.UTF8, "application/json");
            (var post, HttpResponseMessage response) = await PostRequest<Post>($"{defaultPath}posts", postBody);
            return post;
        }
        #endregion

        #region Comment methods
        public async Task<IList<Comment>> ListComments(bool embed = false)
		{
			return await GetRequest<Comment[]>($"{defaultPath}comments", embed).ConfigureAwait(false);
		}

        public async Task<IList<Comment>> GetCommentsForPost(string id, bool embed = false)
        {
            return await GetRequest<Comment[]>($"{defaultPath}comments?post={id}", embed);
        }

		public async Task<Comment> GetComment(string id, bool embed = false)
		{
			return await GetRequest<Comment>($"{defaultPath}comments/{id}", embed).ConfigureAwait(false);
		}
        #endregion

        #region Tag methods
        public async Task<Tag> CreateTag(Tag tagObject)
        { 
            var postBody = new StringContent(JsonConvert.SerializeObject(tagObject).ToString(), Encoding.UTF8, "application/json"); 
            (var tag, HttpResponseMessage response) = await PostRequest<Tag>($"{defaultPath}tags", postBody);
            return tag;
        }
        #endregion

        #region User methods
        public async Task<User> GetCurrentUser()
        {
            return await GetRequest<User>($"users/me", true).ConfigureAwait(false);
        }
        #endregion

        #region Media methods
        public async Task<Media> GetMedia(string id, bool embed = false)
        {
            return await GetRequest<Media>($"{defaultPath}media/{id}", embed).ConfigureAwait(false);
        }

        public async Task<(Media, HttpStatusCode)> GetMedia(string id, bool statusCode, bool embed = false)
        {
            var media = await GetRequest<Media>($"{defaultPath}media/{id}", embed).ConfigureAwait(false);
            return (media, HttpStatusCode.Accepted);
        }



        #endregion


        #region auth methods

        public async Task RequestJWToken()
        {
            var route = $"{jwtPath}token";
            using (var client = new HttpClient())
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", Username),
                    new KeyValuePair<string, string>("password", Password)
                });

                (JWTUser jwtUser, HttpResponseMessage response) = await PostRequest<JWTUser>(route, formContent, false);
                JWToken = jwtUser.Token;
                return;
            }
        }

        public async Task<bool> IsValidJWToken()
        {
            var route = $"{jwtPath}token/validate";
            using (var client = new HttpClient())
            {
                (JWTUser jwtUser, HttpResponseMessage repsonse) = await PostRequest<JWTUser>(route, null, true);
                return repsonse.IsSuccessStatusCode;
            }
        }

        #endregion


        #region internal http methods
        protected async Task<TClass> GetRequest<TClass>(string route, bool embed, bool isAuthRequired = false)
			where TClass : class
		{
            string embedParam = "";
            if(embed) { embedParam = "?_embed"; }

			using (var client = new HttpClient())
			{
                if (isAuthRequired)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                }
                var response = await client.GetAsync($"{WordPressUri}{route}{embedParam}").ConfigureAwait(false);
                
                if (response.IsSuccessStatusCode)
				{
					var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
					return JsonConvert.DeserializeObject<TClass>(responseString);
				}
			}
			return default(TClass);
		}

        protected async Task<(TClass, HttpResponseMessage)> PostRequest<TClass>(string route, HttpContent postBody, bool isAuthRequired = true)
            where TClass : class
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            using (var client = new HttpClient())
            {
                if (isAuthRequired) {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
                }
                try
                {
                    response = await client.PostAsync($"{WordPressUri}{route}", postBody).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return (JsonConvert.DeserializeObject<TClass>(responseString), response);
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("exception thrown");
                }
            }
            return (default(TClass), response);
        }

        #endregion

    }
}