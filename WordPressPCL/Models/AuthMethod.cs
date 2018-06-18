using System;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Authentcation Methods
    /// <para> JWT - recommended AUTH method</para>
    /// </summary>
    public enum AuthMethod
    {
        /// <summary>
        /// Basic HTTP Header authentication
        /// </summary>
        [Obsolete("Use JWT instead of Basic")]
        Basic,

        /// <summary>
        /// JSON Web Token Authentication method. Need configure your site with this plugin https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/
        /// </summary>
        JWT
    }
}