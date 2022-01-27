namespace WordPressPCL.Utility
{
    /// <summary>
    /// Helper clas for URL manipulation
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Creates a new string with queryParam &amp; queryValue appended as query parameters
        /// Ensures values are appended correctly with &amp; or ?
        /// </summary>
        /// <param name="url">Absolute or relative URL</param>
        /// <param name="paramName">Query Parameter Name</param>
        /// <param name="paramValue">Query Parameter Value</param>
        /// <returns>New URL as string</returns>
        public static string SetQueryParam(this string url, string paramName, string paramValue)
        {
            if(url == null)
            {
                return null;
            }

            if (url.Contains("?"))
            {
                url += '&';
            }
            else
            {
                url += '?';
            }

            url += CombineEnsureSingleSeparator(paramName, paramValue, '=');
            return url;
        }

        /// <summary>
        /// Creates a new string with queryParam &amp; queryValue appended as query parameters
        /// Ensures values are appended correctly with &amp; or ?
        /// </summary>
        /// <param name="url">Absolute or relative URL</param>
        /// <param name="paramName">Query Parameter Name</param>
        /// <param name="paramValue">Query Parameter Value</param>
        /// <returns>New URL as string</returns>
        public static string SetQueryParam(this string url, string paramName, int paramValue)
        {
            return SetQueryParam(url, paramName, $"{paramValue}");
        }

        private static string CombineEnsureSingleSeparator(string a, string b, char separator)
        {
            if (string.IsNullOrEmpty(a)) return b;
            if (string.IsNullOrEmpty(b)) return a;
            return a.TrimEnd(separator) + separator + b.TrimStart(separator);
        }
    }


}
