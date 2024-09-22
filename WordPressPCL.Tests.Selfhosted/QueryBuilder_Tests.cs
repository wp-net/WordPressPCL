using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class QueryBuilder_Tests
{
    [TestMethod]
    public void Multi_Parameter_Query_Works_Test()
    {
        // Initialize
        PostsQueryBuilder builder = new() {
            Page = 1,
            PerPage = 15,
            OrderBy = PostsOrderBy.Title,
            Order = Order.ASC,
            Statuses = new List<Status> { Status.Publish },
            Embed = true
        };
        Console.WriteLine(builder.BuildQuery());
        Assert.AreEqual("?page=1&per_page=15&orderby=title&status=publish&order=asc&_embed=true&context=view", builder.BuildQuery());
    }
}


