using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Plugins Query Builder class to construct queries with valid parameters
    /// </summary>
    public class PluginsQueryBuilder : QueryBuilder
    {
        /// <summary>
        /// Limit results to those matching a string.
        /// </summary>
        [QueryText("search")]
        public string Search { get; set; }

        /// <summary>
        /// Limit results to specific status
        /// </summary>
        [QueryText("status")]
        public ActivationStatus Status { get; set; }


    }
}