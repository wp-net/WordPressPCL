using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted
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
            Assert.AreEqual(builder.BuildQueryURL(), "?page=2&_embed=true");
        }
    }


}