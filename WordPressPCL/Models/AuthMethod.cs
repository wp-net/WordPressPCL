namespace WordPressPCL.Models
{
    /// <summary>
    /// Authentication Methods
    /// <para> JWT - recommended AUTH method</para>
    /// </summary>
    public enum AuthMethod
    {
        /// <summary>
        /// JSON Web Token Authentication method by Enrique Chavez. Need configure your site with this plugin https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/
        /// </summary>
        JWT,
        /// <summary>
        /// JSON Web Token Authentication method by Useful Team. Need configure your site with this plugin https://wordpress.org/plugins/jwt-auth/
        /// </summary>
        JWTAuth
    }
}