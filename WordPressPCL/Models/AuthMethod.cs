namespace WordPressPCL.Models
{
    /// <summary>
    /// Authentication Methods
    /// <para> JWT - recommended AUTH method</para>
    /// </summary>
    public enum AuthMethod
    {
        /// <summary>
        /// Bearer Authentication using token
        /// </summary>
        Bearer,
        /// <summary>
        /// Basic Authentication using Application Passwords introduced in Wordpress 5.6
        /// </summary>
        Basic
    }
}