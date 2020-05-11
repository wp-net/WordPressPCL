using System;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Authentication Methods
    /// <para> JWT - recommended AUTH method</para>
    /// </summary>
    public enum AuthMethod
    {
        /// <summary>
        /// JSON Web Token Authentication method. Need configure your site with this plugin https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/
        /// </summary>
        JWT
    }
}