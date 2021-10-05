using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted
{
    [TestClass]
    public class QueryBuilder_Tests
    {
        [TestMethod]
        public void Multi_Parameter_Query_Works_Test()
        {
            // Initialize
            var builder = new PostsQueryBuilder() {
                Page = 2,
                Embed = true
            };
            var query = builder.BuildQuery();
            Assert.AreEqual("page=2&orderby=date&order=desc&_embed=true&context=view", query);
        }
    }


}