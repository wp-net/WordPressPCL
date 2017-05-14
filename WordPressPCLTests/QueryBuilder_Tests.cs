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
        public async Task Multi_Parameter_Query_Works_Test()
        {
            // Initialize
            var builder = new QueryBuilder(ApiCredentials.WordPressUri) {
                Page = 2,
                Embed = true
            };
            Console.WriteLine(builder.ToString());
            Assert.AreEqual(builder.ToString(), $"{ApiCredentials.WordPressUri}?page=2&_embed=");
        }
    }

}