using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{

    /// <summary>
    /// Client class for interaction with Auth endpoints of WP REST API
    /// </summary>
    public class Auth 
    {

        private const string JwtPath = "jwt-auth/v1/";

        /// <summary>
        /// Authentication Method
        /// </summary>
        public AuthMethod AuthMethod { get; private set; } = AuthMethod.Bearer;

        /// <summary>
        /// JWT Plugin
        /// </summary>
        public JWTPlugin JWTPlugin { get; private set; } = JWTPlugin.JWTAuthByEnriqueChavez;

        /// <summary>
        /// Username for Basic Authentication
        /// </summary>
        public string Username { get; private set; } = string.Empty;

        /// <summary>
        /// Helper for HTTP requests
        /// </summary>
        private readonly HttpHelper _httpHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public Auth(HttpHelper httpHelper) 
        {
            _httpHelper = httpHelper;
        }

        /// <summary>
        /// Sets up the Auth Client to use Bearer Authentication with specified JWTPlugin
        /// </summary>
        /// <param name="jwtPlugin">JWT Plugin to use for Bearer Authentication</param>
        public void UseBearerAuth(JWTPlugin jwtPlugin) 
        {
            AuthMethod = AuthMethod.Bearer;
            JWTPlugin = jwtPlugin;
            _httpHelper.AuthMethod = AuthMethod.Bearer;
        }

        /// <summary>
        /// Sets up the Auth Client to use Basic Authentication with Application Password
        /// </summary>
        /// <param name="username">Username for Basic Authentication</param>
        /// <param name="applicationPassword">Application Password for Basic Authentication</param>
        public void UseBasicAuth(string username, string applicationPassword) 
        {
            AuthMethod = AuthMethod.Basic;
            Username = username;
            _httpHelper.AuthMethod = AuthMethod.Basic;
            _httpHelper.UserName = username;
            _httpHelper.ApplicationPassword = applicationPassword;
        }

        /// <summary>
        /// Perform authentication by JWToken
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        public async Task RequestJWTokenAsync(string username, string password) {
            if (AuthMethod == AuthMethod.Basic) {
                throw new WPException("Cannot request JWToken with Basic Authentication");
            }

            var route = $"{JwtPath}token";
            using var formContent = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
            });
            
            switch (JWTPlugin) {
                case JWTPlugin.JWTAuthByEnriqueChavez:
                    var (jwtUser, _) = await _httpHelper.PostRequestAsync<JWTUser>(route, formContent, isAuthRequired: false, ignoreDefaultPath: true).ConfigureAwait(false);
                    _httpHelper.JWToken = jwtUser?.Token;
                    break;
                case JWTPlugin.JWTAuthByUsefulTeam:
                    _httpHelper.HttpResponsePreProcessing = RemoveEmptyData;
                    var (jwtResponse, _) = await _httpHelper.PostRequestAsync<JWTResponse>(route, formContent, isAuthRequired: false, ignoreDefaultPath: true).ConfigureAwait(false);
                    _httpHelper.HttpResponsePreProcessing = null;
                    _httpHelper.JWToken = jwtResponse?.Data?.Token;
                    break;
                default:
                    throw new WPException("Invalid JWT Plugin");
            }
        }

        /// <summary>
        /// Forget the JWT Auth Token, won't invalidate it serverside though
        /// </summary>
        public void Logout() {
            _httpHelper.JWToken = default;
            _httpHelper.UserName = default;
            _httpHelper.ApplicationPassword = default;
        }

        /// <summary>
        /// Check if token is valid
        /// </summary>
        /// <returns>Result of checking</returns>
        public async Task<bool> IsValidJWTokenAsync() {
            if (AuthMethod == AuthMethod.Basic) {
                throw new WPException("Cannot check validity of JWToken with Basic Authentication");
            }

            var route = $"{JwtPath}token/validate";
            try {
                switch (JWTPlugin) {
                    case JWTPlugin.JWTAuthByEnriqueChavez:
                        var (jwtUser, repsonse) = await _httpHelper.PostRequestAsync<JWTUser>(route, null, isAuthRequired: true, ignoreDefaultPath: true).ConfigureAwait(false);
                        return repsonse.IsSuccessStatusCode;
                    case JWTPlugin.JWTAuthByUsefulTeam:
                        _httpHelper.HttpResponsePreProcessing = RemoveEmptyData;
                        var (jwtResponse, _) = await _httpHelper.PostRequestAsync<JWTResponse>(route, null, isAuthRequired: true, ignoreDefaultPath: true).ConfigureAwait(false);
                        _httpHelper.HttpResponsePreProcessing = null;
                        return jwtResponse.Success;
                    default:
                        throw new WPException("Invalid JWT Plugin");
                }
            } catch (WPException) {
                return false;
            }
        }

        /// <summary>
        /// Removes an empty data field in a Json string, e.g. when checking validity of token
        /// </summary>
        /// <param name="response">Json response input string</param>
        /// <returns>Json response output string</returns>
        private string RemoveEmptyData(string response) {
            JObject jo = JObject.Parse(response);
            if (jo.SelectToken("data") != null && !jo.SelectToken("data").HasValues) {
                jo.Remove("data");
            }
            return jo.ToString();
        }

        /// <summary>
        /// Sets an existing JWToken
        /// </summary>
        /// <param name="token"></param>
        public void SetJWToken(string token) {
            _httpHelper.JWToken = token;
        }

        /// <summary>
        /// Gets the JWToken from the client
        /// </summary>
        /// <returns></returns>
        public string GetToken() {
            return _httpHelper.JWToken;
        }

    }
}
