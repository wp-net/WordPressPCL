using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;
using WordPressPCL.Utility;

namespace WordPressPCL.Client {

    /// <summary>
    /// Client class for interaction with Auth endpoints of WP REST API
    /// </summary>
    public class Auth 
    {

        private const string JwtPath = "jwt-auth/v1/";

        /// <summary>
        /// Authentication Method
        /// </summary>
        internal AuthMethod AuthMethod { get; set; }

        /// <summary>
        /// Helper for HTTP requests
        /// </summary>
        private readonly HttpHelper _httpHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public Auth(ref HttpHelper httpHelper) 
        {
            _httpHelper = httpHelper;
        }

        /// <summary>
        /// Perform authentication by JWToken
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="Password">password</param>
        public async Task RequestJWTokenAsync(string username, string Password) {
            var route = $"{JwtPath}token";
            using var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", Password)
                });
            if (AuthMethod == AuthMethod.JWT) {
                (JWTUser jwtUser, _) = await _httpHelper.PostRequestAsync<JWTUser>(route, formContent, false).ConfigureAwait(false);
                _httpHelper.JWToken = jwtUser?.Token;
            } else if (AuthMethod == AuthMethod.JWTAuth) {
                _httpHelper.HttpResponsePreProcessing = RemoveEmptyData;
                (JWTResponse jwtResponse, _) = await _httpHelper.PostRequestAsync<JWTResponse>(route, formContent, false).ConfigureAwait(false);
                _httpHelper.HttpResponsePreProcessing = null;
                _httpHelper.JWToken = jwtResponse?.Data?.Token;
            } else {
                throw new ArgumentException($"Authentication methode {AuthMethod} is not supported");
            }
        }

        /// <summary>
        /// Forget the JWT Auth Token, won't invalidate it serverside though
        /// </summary>
        public void Logout() {
            _httpHelper.JWToken = default;
            _httpHelper.ApplicationPassword = default;
        }

        /// <summary>
        /// Check if token is valid
        /// </summary>
        /// <returns>Result of checking</returns>
        public async Task<bool> IsValidJWTokenAsync() {
            var route = $"{JwtPath}token/validate";
            try {
                if (AuthMethod == AuthMethod.JWT) {
                    (JWTUser jwtUser, HttpResponseMessage repsonse) = await _httpHelper.PostRequestAsync<JWTUser>(route, null, true).ConfigureAwait(false);
                    return repsonse.IsSuccessStatusCode;
                } else if (AuthMethod == AuthMethod.JWTAuth) {
                    _httpHelper.HttpResponsePreProcessing = RemoveEmptyData;
                    (JWTResponse jwtResponse, _) = await _httpHelper.PostRequestAsync<JWTResponse>(route, null, true).ConfigureAwait(false);
                    _httpHelper.HttpResponsePreProcessing = null;
                    return jwtResponse.Success;
                } else {
                    throw new ArgumentException($"Authentication method {AuthMethod} is not supported");
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
            if (!jo.SelectToken("data").HasValues) {
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

        /// <summary>
        /// Store Application Password in the Client
        /// </summary>
        /// <param name="applicationPassword"></param>
        public void SetApplicationPassword(string applicationPassword) {
            _httpHelper.ApplicationPassword = applicationPassword;
        }

    }
}
