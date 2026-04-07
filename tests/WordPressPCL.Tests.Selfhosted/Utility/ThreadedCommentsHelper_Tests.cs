using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Tests.Selfhosted.Utility;

[TestClass]
public class ThreadedCommentsHelper_Tests
{
    [TestMethod]
    public void ToThreaded_Descending_KeepsTopLevelCommentsInDescendingDateOrder()
    {
        DateTime olderRootDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime newerRootDate = new(2026, 1, 2, 0, 0, 0, DateTimeKind.Utc);
        DateTime newestChildDate = new(2026, 1, 3, 0, 0, 0, DateTimeKind.Utc);

        List<Comment> comments =
        [
            new Comment { Id = 1, Date = newerRootDate },
            new Comment { Id = 2, ParentId = 1, Date = newestChildDate },
            new Comment { Id = 3, Date = olderRootDate }
        ];

        List<CommentThreaded>? threaded = comments.ToThreaded(true);

        Assert.IsNotNull(threaded);
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, threaded.Select(comment => comment.Id).ToList());

        List<CommentThreaded> firstLevel = threaded.Where(comment => comment.Depth == 0).ToList();
        CollectionAssert.AreEqual(new[] { 1, 3 }, firstLevel.Select(comment => comment.Id).ToList());
        Assert.IsTrue(firstLevel[0].Date >= firstLevel[1].Date);
    }
}
