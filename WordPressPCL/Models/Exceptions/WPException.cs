using System;

namespace WordPressPCL.Models.Exceptions
{
    /// <summary>
    /// WordPress request exceptions
    /// </summary>
    public class WPException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WPException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public WPException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public WPException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor
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
