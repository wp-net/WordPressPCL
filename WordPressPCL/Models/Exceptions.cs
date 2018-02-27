using System;

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
}