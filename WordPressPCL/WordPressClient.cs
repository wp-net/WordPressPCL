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
using WordPressPCL.Client;

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
        }

        #region Post methods 
        public async Task<IList<Post>> ListPosts(bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>($"{defaultPath}posts", embed).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListPosts(QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>(builder.SetRootUrl($"{defaultPath}posts").ToString(), false).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListStickyPosts(bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>($"{defaultPath}posts?sticky=true", embed).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListStickyPosts(QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>(builder.SetRootUrl($"{defaultPath}posts?sticky=true").ToString(), false).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListPostsByCategory(int categoryId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>($"{defaultPath}posts?categories={categoryId}", embed).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListPostsByCategory(int categoryId, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>(builder.SetRootUrl($"{defaultPath}posts?categories={categoryId}").ToString(), false).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListPostsByTag(int tagId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>($"{defaultPath}posts?tags={tagId}", embed).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListPostsByTag(int tagId, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>(builder.SetRootUrl($"{defaultPath}posts?tags={tagId}").ToString(), false).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListPostsByAuthor(int authorId, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>($"{defaultPath}posts?author={authorId}", embed).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListPostsByAuthor(int authorId, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>(builder.SetRootUrl($"{defaultPath}posts?author={authorId}").ToString(), false).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListPostsBySearch(string searchTerm, bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>($"{defaultPath}posts?search={searchTerm}", embed).ConfigureAwait(false);
        }

        public async Task<IList<Post>> ListPostsBySearch(string searchTerm, QueryBuilder builder)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return await GetRequest<Post[]>(builder.SetRootUrl($"{defaultPath}posts?search={searchTerm}").ToString(), false).ConfigureAwait(false);
        }

        public async Task<Post> GetPost(int postId, bool embed = false)
        {
            return await GetRequest<Post>($"{defaultPath}posts/{postId}", embed).ConfigureAwait(false);
        }

        public async Task<Post> CreatePost(PostCreate postObject)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(postObject).ToString(), Encoding.UTF8, "application/json");
            (var post, HttpResponseMessage response) = await PostRequest<Post>($"{defaultPath}posts", postBody);
            return post;
        }

        public async Task<Post> UpdatePost(Post postObject)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(postObject).ToString(), Encoding.UTF8, "application/json");
            (var post, HttpResponseMessage response) = await PostRequest<Post>($"{defaultPath}posts/{postObject.Id}", postBody);
            return post;
        }

        public async Task<HttpResponseMessage> DeletePost(int id)
        {
            var response = await DeleteRequest($"{defaultPath}posts/{id}").ConfigureAwait(false);
            return response;
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

        public async Task<Comment> CreateComment(CommentCreate commentObject, int postId)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(commentObject).ToString(), Encoding.UTF8, "application/json");
            (var comment, HttpResponseMessage response) = await PostRequest<Comment>($"{defaultPath}comments", postBody);
            return comment;
        }

        public async Task<HttpResponseMessage> DeleteComment(int id)
        {
            var response = await DeleteRequest($"{defaultPath}comments/{id}").ConfigureAwait(false);
            return response;
        }
        #endregion
		
	      #region Category methods
	      public async Task<IList<Category>> ListCategories(bool embed = false)
        {
            // default values 
            // int page = 1, int per_page = 10, int offset = 0, Category.Order order = Category.Order.Asc, Category.OrderBy orderby = Category.OrderBy.Name
            return await GetRequest<Post[]>($"{defaultPath}categories", embed).ConfigureAwait(false);
        }

        public async Task<Category> GetCategory(int categoryId, bool embed = false)
        {
            return await GetRequest<Post[]>($"{defaultPath}categories?category={categoryId}", embed).ConfigureAwait(false);
        }

        public async Task<Category> CreateCategory(CategoryCreate postObject)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(postObject).ToString(), Encoding.UTF8, "application/json");
            (var post, HttpResponseMessage response) = await PostRequest<Category>($"{defaultPath}categories", postBody);
            return post;
        }

        public async Task<Category> UpdateCategory(Category postObject)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(postObject).ToString(), Encoding.UTF8, "application/json");
            (var post, HttpResponseMessage response) = await PostRequest<Category>($"{defaultPath}categories/{postObject.Id}", postBody);
            return post;
        }

        public async Task<HttpResponseMessage> DeleteCategory(int id)
        {
            var response = await DeleteRequest($"{defaultPath}categories/{id}").ConfigureAwait(false);
            return response;
        }
	      #endregion

        #region Tag methods
        public async Task<Tag> CreateTag(Tag tagObject)
        {
            var postBody = new StringContent(JsonConvert.SerializeObject(tagObject).ToString(), Encoding.UTF8, "application/json");
            (var tag, HttpResponseMessage response) = await PostRequest<Tag>($"{defaultPath}tags", postBody);
            return tag;
        }

        public async Task<IList<Tag>> ListTags(bool embed = false)
        {
            return await GetRequest<Tag[]>($"{defaultPath}tags", embed).ConfigureAwait(false);
        }
        
        public async Task<Tag> GetTag(int tagId, bool embed = false)
        {
            return await GetRequest<Tag>($"{defaultPath}tags/{tagId}", embed).ConfigureAwait(false);
        }
        #endregion

        #region User methods
        public async Task<User> GetCurrentUser()
        {
            return await GetRequest<User>($"{defaultPath}users/me", true, true).ConfigureAwait(false);
        }

        public async Task<IList<User>> ListUsers()
        {
            return await GetRequest<IList<User>>($"{defaultPath}users", false, false).ConfigureAwait(false);
        }

        public async Task<User> GetUser(int id)
        {
            return await GetRequest<User>($"{defaultPath}users/{id}", false, false).ConfigureAwait(false);
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

        #region Settings methods
        /// <summary>
        /// Get site settings
        /// </summary>
        /// <returns>Site settings</returns>
        public async Task<Settings> GetSettings()
        {
            return await _httpHelper.GetRequest<Settings>($"{defaultPath}settings", false, true).ConfigureAwait(false);
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
        #endregion

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

        #endregion
    }
}
