using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Themes Query Builder class to construct queries with valid parameters
    /// </summary>
    public class ThemesQueryBuilder : QueryBuilder
    {
        /// <summary>
        /// Limit results to specific status
        /// </summary>
        [QueryText("status")]
        public ActivationStatus Status { get; set; }


    }
}