using System;
using System.Text;
using WordPressPCL.Models;

namespace WordPressPCL.Utility
{
    public class QueryBuilder
    {
        private string _url;
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int Offset { get; set; }
        public OrderBy OrderBy { get; set; }
        public bool Embed { get; set; }
        public Context Context { get; set; }



        public QueryBuilder(string wordpressUrl = null)
        {
            _url = wordpressUrl;
            Page = 1;
            Per_Page = 10;
            Offset = 0;
            OrderBy = OrderBy.date;
            Embed = false;
            Context = Context.View;
        }

        internal QueryBuilder SetRootUrl(string url) 
        {
            this._url = url;
            return this;
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(_url)) return string.Empty;
            else
            {
                StringBuilder sb = new StringBuilder(_url);
                if (Page > 1)
                {
                    sb.Append(appendQuery(_url, PAGE_QUERYSTRING));
                    sb.Append(Page);
                }
                if (Embed)
                {
                    sb.Append(appendQuery(sb.ToString(), EMBED_QUERYSTRING));
                }
                if (Per_Page != 10)
                {
                    sb.Append(appendQuery(sb.ToString(), PER_PAGE_QUERYSTRING));
                    sb.Append(Per_Page);
                }
                if (Offset > 0)
                {
                    sb.Append(appendQuery(sb.ToString(), OFFSET_QUERYSTRING));
                    sb.Append(Offset);
                }
                if (OrderBy != OrderBy.date)
                {
                    sb.Append(appendQuery(sb.ToString(), ORDER_BY_QUERYSTRING));
                    sb.Append(Convert.ToInt32(OrderBy));
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
