using System;
using System.Net.Http;

namespace WordPressPCL.Models
{
    /// <summary>
    /// WordPress request exceptions
    /// </summary>
    public class WPException : Exception
    {
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="request">additional request info</param>
        public WPException(BadRequest request) : base() { RequestData = request; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">additional message</param>
        /// <param name="request">additional request info</param>
        public WPException(string message, BadRequest request) : base(message) { RequestData = request; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">additional message</param>
        /// <param name="inner">inner exception</param>
        /// <param name="request">additional request info</param>
        public WPException(string message, Exception inner, BadRequest request) : base(message, inner) { RequestData = request; }

        /// <summary>
        /// Bad request data info
        /// </summary>
        public BadRequest RequestData { get; set; }
    }

    /// <summary>
    /// An exception resulting from a malformed WP response from a
    /// WP REST call, where the response is still a valid (but not
    /// successful) HTTP response. For example, the response returned
    /// from an endpoint that is not a WP REST endpoint
    /// </summary>
    public class WPUnexpectedException : Exception
    {
        /// <summary>
        /// Construct a WPUnexpectedException from a response
        /// </summary>
        /// <param name="response">The raw response</param>
        /// <param name="resonseBody">The response body, if any</param>     
        public WPUnexpectedException(HttpResponseMessage response, string resonseBody)
            : base(FormatExceptionMessage(response))
        {
            Response = response;
            ResponseBody = resonseBody;
        }

        /// <summary>
        /// The response that triggered the error
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// The response body (if any) that was returned with the error status
        /// </summary>
        public string ResponseBody { get; set; }

        private static string FormatExceptionMessage(HttpResponseMessage response)
        {
            return $"Server returned HTTP status {response.StatusCode}";
        }
    }
}