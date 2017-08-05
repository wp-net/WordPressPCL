using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Helper class incapsulates common HTTP requests methods
    /// </summary>
    public class HttpHelper
    {
        private string _WordpressURI;
        /// <summary>
        /// JSON Web Token
        /// </summary>
        public string JWToken { get; set; }
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
           
            using (var client = new HttpClient())
            {
                if (isAuthRequired)
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
                }
                try
                {
                    response = await client.GetAsync($"{_WordpressURI}{route}{embedParam}").ConfigureAwait(false);
                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<TClass>(responseString);
                    }
                    else
                    {
                        Debug.WriteLine(responseString);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("exception thrown: " + ex.Message);
                }
            }
            return default(TClass);
        }

        internal async Task<(TClass, HttpResponseMessage)> PostRequest<TClass>(string route, HttpContent postBody, bool isAuthRequired = true)
            where TClass : class
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (isAuthRequired)
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
                }
                try
                {
                    response = await client.PostAsync($"{_WordpressURI}{route}", postBody).ConfigureAwait(false);
                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        return (JsonConvert.DeserializeObject<TClass>(responseString), response);
                    }
                    else
                    {
                        Debug.WriteLine(responseString);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("exception thrown: " + ex.Message);
                }
            }
            return (default(TClass), response);
        }

        internal async Task<HttpResponseMessage> DeleteRequest(string route, bool isAuthRequired = true)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            using (var client = new HttpClient())
            {
                if (isAuthRequired)
                {
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Utility.Authentication.Base64Encode($"{Username}:{Password}"));
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
                }
                try
                {
                    response = await client.DeleteAsync($"{_WordpressURI}{route}").ConfigureAwait(false);
                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }
                    else
                    {
                        Debug.WriteLine(responseString);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("exception thrown: " + ex.Message);
                }
            }
            return response;
        }
    }
}
