using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Helper class incapsulates common HTTP requests methods
    /// </summary>
    public class HttpHelper
    {
        private static HttpClient _httpClient = new HttpClient();
        private string _WordpressURI;

        /// <summary>
        /// JSON Web Token
        /// </summary>
        public string JWToken { get; set; }

        /// <summary>
        /// Function called when a HttpRequest response is readed
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
        /// <param name="WordpressURI">base WP RESR API endpoint EX. http://demo.com/wp-json/ </param>
        public HttpHelper(string WordpressURI)
        {
            _WordpressURI = WordpressURI;
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
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);

            if (isAuthRequired)
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                if (_httpClient.DefaultRequestHeaders.Authorization == null || _httpClient.DefaultRequestHeaders.Authorization.Parameter != JWToken)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
            try
            {
                response = await _httpClient.GetAsync($"{_WordpressURI}{route}{embedParam}").ConfigureAwait(false);
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
                    Debug.WriteLine(responseString);
                    var badrequest = JsonConvert.DeserializeObject<BadRequest>(responseString);
                    throw new WPException(badrequest.Message, badrequest);
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
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);

            if (isAuthRequired)
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (_httpClient.DefaultRequestHeaders.Authorization == null || _httpClient.DefaultRequestHeaders.Authorization.Parameter != JWToken)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
            try
            {
                response = await _httpClient.PostAsync($"{_WordpressURI}{route}", postBody).ConfigureAwait(false);
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
                    Debug.WriteLine(responseString);
                    var badrequest = JsonConvert.DeserializeObject<BadRequest>(responseString);
                    throw new WPException(badrequest.Message, badrequest);
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
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);

            if (isAuthRequired)
            {
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (_httpClient.DefaultRequestHeaders.Authorization == null || _httpClient.DefaultRequestHeaders.Authorization.Parameter != JWToken)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
            try
            {
                response = await _httpClient.DeleteAsync($"{_WordpressURI}{route}").ConfigureAwait(false);
                LastResponseHeaders = response.Headers;
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine(responseString);
                    var badrequest = JsonConvert.DeserializeObject<BadRequest>(responseString);
                    throw new WPException(badrequest.Message, badrequest);
                }
            }
            catch (WPException) { throw; }
            catch (Exception ex)
            {
                Debug.WriteLine("exception thrown: " + ex.Message);
                throw;
            }
        }
    }
}