using System;
using System.Text;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    public class QueryBuilder
    {
        private string url;
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Offset { get; set; }
        public WordPressPCL.Models.OrderBy OrderBy { get; set; }
        public bool Embed { get; set; }
        public QueryBuilder(string url = null)
        {
            this.url = url;
            this.Page = 1;
            this.Per_Page = 10;
            this.Offset = 0;
            this.OrderBy = OrderBy.date;
            this.Embed = false;
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
                if (Page > 1)
                {
                    sb.Append(appendQuery(this.url, PAGE_QUERYSTRING));
                    sb.Append(this.Page);
                }
                if (Embed)
                {
                    sb.Append(appendQuery(sb.ToString(), EMBED_QUERYSTRING));
                }
                if (Per_Page != 10)
                {
                    sb.Append(appendQuery(sb.ToString(), PER_PAGE_QUERYSTRING));
                    sb.Append(this.Per_Page);
                }
                if (Offset > 0)
                {
                    sb.Append(appendQuery(sb.ToString(), OFFSET_QUERYSTRING));
                    sb.Append(this.Offset);
                }
                if (OrderBy != OrderBy.date)
                {
                    sb.Append(appendQuery(sb.ToString(), ORDER_BY_QUERYSTRING));
                    sb.Append(Convert.ToInt32(this.OrderBy));
                }
                //Console.WriteLine(sb.ToString());
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
