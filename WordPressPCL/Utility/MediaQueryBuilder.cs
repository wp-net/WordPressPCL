using System;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Media Query Builder class to construct queries with valid parameters
    /// </summary>
    public class MediaQueryBuilder : QueryBuilder
    {
        /// <summary>
        /// Current page of the collection.
        /// </summary>
        /// <remarks>Default: 1</remarks>
        [QueryText("page")]
        public int Page { get; set; }

        /// <summary>
        /// Maximum number of items to be returned in result set.
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
        public MediaOrderBy OrderBy { get; set; }

        /// <summary>
        /// Limit result set to those of particular parent ids.
        /// </summary>
        [QueryText("parent")]
        public int[] Parents { get; set; }

        /// <summary>
        /// Limit result set to all items except those of a particular parent id.
        /// </summary>
        [QueryText("parent_exclude")]
        public int[] ParentsExclude { get; set; }

        /// <summary>
        /// Limit result set to posts with one or more specific slugs.
        /// </summary>
        [QueryText("slug")]
        public string[] Slugs { get; set; }

        /// <summary>
        /// Limit result set to posts assigned a specific status; can be comma-delimited list of status types.
        /// </summary>
        /// <remarks>Default:  inherit
        /// One of: inherit, private, trash</remarks>
        [QueryText("status")]
        public MediaQueryStatus[] Statuses { get; set; }

        /// <summary>
        /// Use WP Query arguments to modify the response; private query vars require appropriate authorization.
        /// </summary>
        [QueryText("filter")]
        public string Filter { get; set; }

        /// <summary>
        /// Limit result set to attachments of a particular media type.
        /// </summary>
        [QueryText("media_type")]
        public MediaQueryType MediaType { get; set; }

        /// <summary>
        /// Limit result set to attachments of a particular MIME type.
        /// </summary>
        [QueryText("mime_type")]
        public string MimeType { get; set; }
    }
}