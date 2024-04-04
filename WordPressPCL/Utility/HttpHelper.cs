﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Helper class encapsulates common HTTP requests methods
    /// </summary>
    public class HttpHelper
    {
        // non-static HTTPClient so different WordPressClients can have different base addresses
        private readonly HttpClient _defaultHttpClient = new();
        private readonly HttpClient _httpClient;
        private readonly string _defaultPath;
        private readonly Uri _baseUri;

        /// <summary>
        /// JSON Web Token
        /// </summary>
        public string JWToken { get; set; }

        /// <summary>
        /// The Application Password to be used for authentication
        /// </summary>
        internal string ApplicationPassword { get; set; }

        /// <summary>
        /// Authentication Method
        /// </summary>
        internal AuthMethod AuthMethod { get; set; }

        /// <summary>
        /// The username to be used with the Application Password
        /// </summary>
        internal string UserName { get; set; }

        /// <summary>
        /// Function called when a HttpRequest response is read
        /// Executed before trying to convert json content to a TClass object.
        /// </summary>
        public Func<string, string> HttpResponsePreProcessing { get; set; }


        /// <summary>
        /// Serialization/Deserialization settings for Json.NET library
        /// https://www.newtonsoft.com/json/help/html/SerializationSettings.htm
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; set; }
        /// <summary>
        /// Headers returns by WP and http server from last response
        /// </summary>
        public HttpResponseHeaders LastResponseHeaders { get; set; }

        /// <summary>
        /// Constructor
        /// <paramref name="wordpressURI"/>
        /// </summary>
        /// <param name="wordpressURI">base WP REST API endpoint EX. http://demo.com/wp-json/ </param>
        /// <param name="defaultPath"></param>
        public HttpHelper(Uri wordpressURI, string defaultPath)
        {
            _httpClient = _defaultHttpClient;
            _httpClient.BaseAddress = wordpressURI;
            _defaultPath = defaultPath;

            // by default don't crash on missing member
            JsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
        }

        /// <summary>
        /// Constructor
        /// <paramref name="httpClient"/>
        /// </summary>
        /// <param name="httpClient">Http client which would be used for sending requests to the WordPress API endpoint.</param>
        /// <param name="defaultPath">Relative path to standard API endpoints, defaults to "wp/v2/"</param>
        /// <param name="wordpressURI">(optional) Base WP REST API endpoint EX. http://demo.com/wp-json/. Use this if the BaseAddress of the httpClient is not set.</param>
        public HttpHelper(HttpClient httpClient, string defaultPath, Uri wordpressURI = null)
        {
            _httpClient = httpClient;
            _defaultPath = defaultPath;
            _baseUri = wordpressURI;
            // by default don't crash on missing member
            JsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
        }

        internal async Task<TClass> GetRequestAsync<TClass>(string route, bool embed, bool isAuthRequired = false, bool ignoreDefaultPath = false)
            where TClass : class
        {
            route = BuildRoute(ignoreDefaultPath, route);
            string embedParam = "";
            if (embed)
            {
                if (route.Contains("?"))
                {
                    embedParam = "&_embed";
                }
                else
                {
                    embedParam = "?_embed";
                }
            }
            route += embedParam;

            HttpResponseMessage response;
            using (HttpRequestMessage requestMessage = new(HttpMethod.Get, route))
            {
                SetAuthHeader(isAuthRequired, requestMessage);
                response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            }
            LastResponseHeaders = response.Headers;
            string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (HttpResponsePreProcessing != null)
                {
                    responseString = HttpResponsePreProcessing(responseString);
                }

                return DeserializeJsonResponse<TClass>(response, responseString);
            }
            else
            {
                throw CreateUnexpectedResponseException(response, responseString);
            }
        }

        internal async Task<(TClass, HttpResponseMessage)> PostRequestAsync<TClass>(string route, HttpContent postBody, bool isAuthRequired = true, bool ignoreDefaultPath = false)
            where TClass : class
        {
            route = BuildRoute(ignoreDefaultPath, route);
            HttpResponseMessage response;
            using (HttpRequestMessage requestMessage = new(HttpMethod.Post, route))
            {
                SetAuthHeader(isAuthRequired, requestMessage);
                requestMessage.Content = postBody;
                response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            }

            LastResponseHeaders = response.Headers;
            string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (HttpResponsePreProcessing != null)
                {
                    responseString = HttpResponsePreProcessing(responseString);
                }

                return (DeserializeJsonResponse<TClass>(response, responseString), response);
            }
            else
            {
                throw CreateUnexpectedResponseException(response, responseString);
            }
        }

        internal async Task<bool> DeleteRequestAsync(string route, bool isAuthRequired = true, bool ignoreDefaultPath = false)
        {
            route = BuildRoute(ignoreDefaultPath, route);
            HttpResponseMessage response;
            using (HttpRequestMessage requestMessage = new(HttpMethod.Delete, route))
            {
                SetAuthHeader(isAuthRequired, requestMessage);
                response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            }

            LastResponseHeaders = response.Headers;
            string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw CreateUnexpectedResponseException(response, responseString);
            }
        }

        internal async Task<HttpResponseHeaders> HeadRequestAsync(string route, bool isAuthRequired = false, bool ignoreDefaultPath = false)
        {
            route = BuildRoute(ignoreDefaultPath, route);
            HttpResponseMessage response;
            using (HttpRequestMessage requestMessage = new(HttpMethod.Head, route))
            {
                SetAuthHeader(isAuthRequired, requestMessage);
                response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            }

            LastResponseHeaders = response.Headers;
            if (response.IsSuccessStatusCode)
            {
                return response.Headers;
            }

            throw new WPUnexpectedException(response, string.Empty);
        }

        private void SetAuthHeader(bool isAuthRequired, HttpRequestMessage requestMessage)
        {
            if (isAuthRequired)
            {
                if (AuthMethod == AuthMethod.Bearer)
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
                }
                else if (AuthMethod == AuthMethod.Basic)
                {
                    byte[] authToken = Encoding.ASCII.GetBytes($"{UserName}:{ApplicationPassword}");
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                }
                else
                {
                    throw new WPException("Unsupported Authentication Method");
                }
            }
        }

        private string BuildRoute(bool ignoreDefaultPath, string route)
        {
            if (string.IsNullOrEmpty(route))
            {
                return route;
            }

            string processedRoute = ignoreDefaultPath ? route : $"{_defaultPath}{route}";

            if (_baseUri != null)
            {
                processedRoute = $"{_baseUri.AbsoluteUri.TrimEnd('/')}/{processedRoute.TrimStart('/')}";
            }

            return processedRoute;
        }

        private TClass DeserializeJsonResponse<TClass>(HttpResponseMessage response, string responseString)
        {
            try
            {
                if (JsonSerializerSettings != null)
                {
                    return JsonConvert.DeserializeObject<TClass>(responseString, JsonSerializerSettings);
                }
                return JsonConvert.DeserializeObject<TClass>(responseString);
            }
            catch (JsonReaderException)
            {
                (bool success, string sanitizedResponse) = TryGetResponseFromMalformedResponse(responseString);
                if (!success)
                {
                    throw new WPUnexpectedException(response, responseString);
                }

                if (JsonSerializerSettings != null)
                {
                    return JsonConvert.DeserializeObject<TClass>(sanitizedResponse, JsonSerializerSettings);
                }
                return JsonConvert.DeserializeObject<TClass>(sanitizedResponse);
            }
        }

        private static (bool, string) TryGetResponseFromMalformedResponse(string responseString)
        {
            responseString = responseString.Trim();
            string jsonSingleItemRegex = @"\{""id"":.+\}$";
            Match match = Regex.Match(responseString, jsonSingleItemRegex);
            if (match.Success)
            {
                return (true, match.Value);
            }
            string jsonCollectionRegex = @"\[({""id"":.+},?)*\]$";
            match = Regex.Match(responseString, jsonCollectionRegex);
            if (match.Success)
            {
                return (true, match.Value);
            }

            return (false, string.Empty);
        }

        private static Exception CreateUnexpectedResponseException(HttpResponseMessage response, string responseString)
        {
            BadRequest badrequest;
            try
            {
                badrequest = JsonConvert.DeserializeObject<BadRequest>(responseString);
            }
            catch (JsonReaderException)
            {
                // the response is not a well formed bad request
                return new WPUnexpectedException(response, responseString);
            }
            return new WPException(badrequest.Message, badrequest);
        }
    }
}
