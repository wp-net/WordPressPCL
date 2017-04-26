using System;
using System.Text;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    public class QueryBuilder
    {
        private string url { get; set; }
        public int page { get; set; }
        public int per_page { get; set; }
        public int offset { get; set; }
        public OrderBy orderBy { get; set; }
        public bool embed { get; set; }
        public QueryBuilder(string url = null)
        {
            this.url = url;
            this.page = 1;
            this.per_page = 10;
            this.offset = 0;
            this.orderBy = OrderBy.date;
            this.embed = false;
        }

        internal QueryBuilder SetRootUrl(string _url) 
        {
            this.url = _url;
            return this;
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(this.url)) return string.Empty;
            else
            {
                StringBuilder sb = new StringBuilder(url);
                if (page > 1)
                {
                    sb.Append(appendQuery(this.url, PAGE_QUERYSTRING));
                    sb.Append(this.page);
                }
                if (embed)
                {
                    sb.Append(appendQuery(this.url, EMBED_QUERYSTRING));
                }
                if (per_page != 10)
                {
                    sb.Append(appendQuery(this.url, PER_PAGE_QUERYSTRING));
                    sb.Append(this.per_page);
                }
                if (offset > 0)
                {
                    sb.Append(appendQuery(this.url, OFFSET_QUERYSTRING));
                    sb.Append(this.offset);
                }
                if (orderBy != OrderBy.date)
                {
                    sb.Append(appendQuery(this.url, ORDER_BY_QUERYSTRING));
                    sb.Append(Convert.ToInt32(this.orderBy));
                }
                return sb.ToString();
            }
        }

        private string appendQuery(string url, string queryString)
        {
            return (url.Contains(QUESTION_MARK) ? AMPERSAND : QUESTION_MARK) + queryString + EQUALS_SYMBOL;
        }

        #region constants
        private const string PAGE_QUERYSTRING = "page";
        private const string EMBED_QUERYSTRING = "_embed";        
        private const string PER_PAGE_QUERYSTRING = "per_page";        
        private const string OFFSET_QUERYSTRING = "offset";        
        private const string ORDER_BY_QUERYSTRING = "orderby";
        private const string QUESTION_MARK = "?";
        private const string AMPERSAND = "&";
        private const string EQUALS_SYMBOL = "=";
        #endregion

    }
}
