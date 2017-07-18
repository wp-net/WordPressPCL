using System;
using System.Collections.Generic;
using System.Text;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Comments Query Builder class to construct queries with valid parameters
    /// </summary>
    public class CommentsQueryBuilder : QueryBuilder
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
        /// Limit result set to that from a specific author email. Requires authorization.
        /// </summary>
        [QueryText("author_email")]
        public string AuthorEmail { get; set; }
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
        /// Limit result set to that of a particular comment karma. Requires authorization.
        /// </summary>
        [QueryText("karma")]
        public int Karma { get; set; }
        /// <summary>
        /// Offset the result set by a specific number of items.
        /// </summary>
        [QueryText("offset")]
        public int Offset { get; set; }
        /// <summary>
        /// Limit result set to resources with a specific menu_order value.
        /// </summary>
        [QueryText("menu_order")]
        public int MenuOrder { get; set; }
        /// <summary>
        /// Sort collection by object attribute.
        /// </summary>
        /// <remarks>Default: date_gmt
        /// One of:  date, date_gmt, id, include, post, parent, type</remarks>
        [QueryText("orderby")]
        public CommentsOrderBy OrderBy { get; set; }
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
        /// Limit result set to resources assigned to specific post ids.
        /// </summary>
        [QueryText("post")]
        public int[] Posts { get; set; }
        /// <summary>
        /// Limit result set to posts assigned one or more statuses.
        /// </summary>
        /// <remarks>Default: approve</remarks>
        [QueryText("status")]
        public CommentStatus[] Statuses { get; set; }
        /// <summary>
        /// Limit result set to comments assigned a specific type. Requires authorization.
        /// </summary>
        /// <remarks>Default: comment</remarks>
        [QueryText("type")]
        public string Type { get; set; }
    }
}
