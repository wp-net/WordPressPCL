using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Response class for the JWT Plugin
    /// </summary>
    public class JWTSimpleJwtLogin
    {
        /// <summary>
        /// Indicates if the call was successful
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// The response message
        /// </summary>
        [JsonProperty("message")]
        [DefaultValue(null)]
        public string Message { get; set; }

        /// <summary>
        /// The JWT Content
        /// </summary>
        [JsonProperty("data")]
        [DefaultValue(null)]
        public JWTSimpleJwtLoginData Data { get; set; }
    }

    public class JWTSimpleJwtLoginData
    {
        /// <summary>
        /// JWT Token
        /// </summary>
        [JsonProperty("jwt")]
        public string Token { get; set; }
    }
}
