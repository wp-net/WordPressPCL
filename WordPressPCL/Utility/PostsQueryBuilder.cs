using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    public class PostsQueryBuilder : QueryBuilder
    {
        [QueryText("page")]
        public int Page { get; set; }
        [QueryText("per_page")]
        public int PerPage { get; set; }
        [QueryText("search")]
        public string Search { get; set; }
        [QueryText("after")]
        public DateTime After { get; set; }
        [QueryText("author")]
        public int[] Authors { get; set; }
        [QueryText("author_exclude")]
        public int[] AuthorsExclude { get; set; }
        [QueryText("before")]
        public DateTime Before { get; set; }
        [QueryText("exclude")]
        public int[] Exclude { get; set; }
        [QueryText("include")]
        public int[] Include { get; set; }
        [QueryText("offset")]
        public int Offset { get; set; }
        [QueryText("orderby")]
        public PostsOrderBy OrderBy { get; set; }
        [QueryText("slug")]
        public string[] Slugs { get; set; }
        [QueryText("status")]
        public Status[] Statuses { get; set; }
        [QueryText("categories")]
        public int[] Categories { get; set; }
        [QueryText("categories_exclude")]
        public int[] CategoriesExclude { get; set; }
        [QueryText("tags")]
        public int[] Tags { get; set; }
        [QueryText("tags_exclude")]
        public int[] TagsExclude { get; set; }
        [QueryText("sticky")]
        public bool Sticky { get; set; }
    }
}
