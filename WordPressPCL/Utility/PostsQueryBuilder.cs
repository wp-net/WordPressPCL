using System;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Post Query Builder class to construct queries with valid parameters
    /// </summary>
    public class PostsQueryBuilder : QueryBuilder
    {
        /// <summary>
        /// Current page of the collection.
        /// </summary>
        /// <remarks>Default: 1</remarks>
        [QueryText("page")]
        public int Page { get; set; }
        /// <summary>
        /// Maximum number of items to be returned in result set (1-100).
        /// </summary>
        /// <remarks>Default: 10</remarks>
        [QueryText("per_page")]
        public int PerPage { get; set; }
        /// <summary>
        /// Limit results to those matching a string.
        /// </summary>
        [QueryText("search")]
        public string Search { get; set; }
        /// <summary>
        /// Limit response to posts published after a given date
        /// </summary>
        [QueryText("after")]
        public DateTime After { get; set; }
        /// <summary>
        /// Limit result set to posts assigned to specific authors.
        /// </summary>
        [QueryText("author")]
        public int[] Authors { get; set; }
        /// <summary>
        /// Ensure result set excludes posts assigned to specific authors.
        /// </summary>
        [QueryText("author_exclude")]
        public int[] AuthorsExclude { get; set; }
        /// <summary>
        /// Limit response to posts published before a given date
        /// </summary>
        [QueryText("before")]
        public DateTime Before { get; set; }
        /// <summary>
        /// Ensure result set excludes specific IDs.
        /// </summary>
        [QueryText("exclude")]
        public int[] Exclude { get; set; }
        /// <summary>
        /// Limit result set to specific IDs.
        /// </summary>
        [QueryText("include")]
        public int[] Include { get; set; }
        /// <summary>
        /// Offset the result set by a specific number of items.
        /// </summary>
        [QueryText("offset")]
        public int Offset { get; set; }
        /// <summary>
        /// Sort collection by object attribute.
        /// </summary>
        /// <remarks>Default: date
        /// One of: date, relevance, id, include, title, slug</remarks>
        [QueryText("orderby")]
        public PostsOrderBy OrderBy { get; set; }
        /// <summary>
        /// Limit result set to posts with one or more specific slugs.
        /// </summary>
        [QueryText("slug")]
        public string[] Slugs { get; set; }
        /// <summary>
        /// Limit result set to posts assigned one or more statuses.
        /// </summary>
        /// <remarks>Default: publish</remarks>
        [QueryText("status")]
        public Status[] Statuses { get; set; }
        /// <summary>
        /// Limit result set to all items that have the specified term assigned in the categories taxonomy.
        /// </summary>
        [QueryText("categories")]
        public int[] Categories { get; set; }
        /// <summary>
        /// Limit result set to all items except those that have the specified term assigned in the categories taxonomy.
        /// </summary>
        [QueryText("categories_exclude")]
        public int[] CategoriesExclude { get; set; }
        /// <summary>
        /// Limit result set to all items that have the specified term assigned in the tags taxonomy.
        /// </summary>
        [QueryText("tags")]
        public int[] Tags { get; set; }
        /// <summary>
        /// Limit result set to all items except those that have the specified term assigned in the tags taxonomy.
        /// </summary>
        [QueryText("tags_exclude")]
        public int[] TagsExclude { get; set; }
        /// <summary>
        /// Limit result set to items that are sticky.
        /// </summary>
        [QueryText("sticky")]
        public bool Sticky { get; set; }
    }
}
