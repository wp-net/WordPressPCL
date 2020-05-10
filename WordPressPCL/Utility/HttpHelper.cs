using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Helper class encapsulates common HTTP requests methods
    /// </summary>
    public class HttpHelper
    {
        private static HttpClient _httpClient = new HttpClient();
        private readonly string _WordpressURI;

        /// <summary>
        /// JSON Web Token
        /// </summary>
        public string JWToken { get; set; }

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
        /// <paramref name="WordpressURI"/>
        /// </summary>
        /// <param name="WordpressURI">base WP REST API endpoint EX. http://demo.com/wp-json/ </param>
        public HttpHelper(string WordpressURI)
        {
            _WordpressURI = WordpressURI;

            // by default don't crash on missing member
            JsonSerializerSettings = new JsonSerializerSettings();
            JsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
        }

        internal async Task<TClass> GetRequest<TClass>(string route, bool embed, bool isAuthRequired = false)
            where TClass : class
        {
            string embedParam = "";
            if (embed)
            {
                if (route.Contains("?"))
                    embedParam = "&_embed";
                else
                    embedParam = "?_embed";
            }

            try
            {
                HttpResponseMessage response;
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_WordpressURI}{route}{embedParam}"))
                {
                    if (isAuthRequired)
                    {
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
                    }
                    response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                }
                LastResponseHeaders = response.Headers;
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    if (HttpResponsePreProcessing != null)
                        responseString = HttpResponsePreProcessing(responseString);
                    if (JsonSerializerSettings != null)
                        return JsonConvert.DeserializeObject<TClass>(responseString, JsonSerializerSettings);
                    return JsonConvert.DeserializeObject<TClass>(responseString);
                }
                else
                {
                    throw CreateUnexpectedResponseException(response, responseString);
                }
            }
            catch (WPException) { throw; }
            catch (Exception ex)
            {
                Debug.WriteLine("exception thrown: " + ex.Message);
                throw;
            }
        }

        internal async Task<(TClass, HttpResponseMessage)> PostRequest<TClass>(string route, HttpContent postBody, bool isAuthRequired = true)
            where TClass : class
        {
            try
            {
                HttpResponseMessage response;
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_WordpressURI}{route}"))
                {
                    if (isAuthRequired && !string.IsNullOrEmpty(JWToken))
                    {
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
                    }
                    requestMessage.Content = postBody;
                    response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                }

                LastResponseHeaders = response.Headers;
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    if (HttpResponsePreProcessing != null)
                        responseString = HttpResponsePreProcessing(responseString);
                    if (JsonSerializerSettings != null)
                        return (JsonConvert.DeserializeObject<TClass>(responseString, JsonSerializerSettings), response);
                    return (JsonConvert.DeserializeObject<TClass>(responseString), response);
                }
                else
                {
                    throw CreateUnexpectedResponseException(response, responseString);
                }
            }
            catch (WPException) { throw; }
            catch (Exception ex)
            {
                Debug.WriteLine("exception thrown: " + ex.Message);
                throw;
            }
        }

        internal async Task<bool> DeleteRequest(string route, bool isAuthRequired = true)
        {
            try
            {
                HttpResponseMessage response;
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{_WordpressURI}{route}"))
                {
                    if (isAuthRequired && !string.IsNullOrEmpty(JWToken))
                    {
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
                    }
                    response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                }

                LastResponseHeaders = response.Headers;
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw CreateUnexpectedResponseException(response, responseString);
                }
            }
            catch (WPException) { throw; }
            catch (Exception ex)
            {
                Debug.WriteLine("exception thrown: " + ex.Message);
                throw;
            }
        }

        private static Exception CreateUnexpectedResponseException(HttpResponseMessage response, string responseString)
        {
            Debug.WriteLine(responseString);

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