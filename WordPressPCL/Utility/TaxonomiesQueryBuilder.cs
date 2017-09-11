namespace WordPressPCL.Utility
{
    /// <summary>
    /// Taxonomies Query Builder class to construct queries with valid parameters
    /// </summary>
    public class TaxonomiesQueryBuilder : QueryBuilder
    {
        /// <summary>
        /// Limit results to taxonomies associated with a specific post type.
        /// </summary>
        [QueryText("type")]
        public string Type { get; set; }
    }
}