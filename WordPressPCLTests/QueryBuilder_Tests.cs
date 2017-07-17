using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCLTests.Utility;
using WordPressPCL;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using System.Net;
using System.Linq;
using Newtonsoft.Json;

namespace WordPressPCLTests
{
    [TestClass]
    public class QueryBuilder_Tests
    {
        private const int TAG_ID = 2;

        [TestMethod]
        public void Multi_Parameter_Query_Works_Test()
        {
            // Initialize
            var builder = new PostsQueryBuilder() {
                Page = 2,
                Embed = true
            };
            Console.WriteLine(builder.BuildQueryURL());
            Assert.AreEqual(builder.ToString(), $"{ApiCredentials.WordPressUri}?page=2&_embed=");
        }
       
        [TestMethod]
        public void NewQueryBuilder()
        {
            PostsQueryBuilder p = new PostsQueryBuilder();
            p.Page = 1;
            p.Authors = new int[] { 1, 2, 3 };
            p.OrderBy = PostsOrderBy.Date;
            p.Order = Order.ASC;
            p.Search = "sdfsdfsdf";
            p.Categories = new int[] { 1, 2, 3 };
            p.Statuses = new Status[] { Status.Draft, Status.Pending };
            p.After = DateTime.Now;
            var s = p.BuildQueryURL();
        }
    }


}