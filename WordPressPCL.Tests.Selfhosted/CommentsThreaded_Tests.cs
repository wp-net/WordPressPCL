using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using WordPressPCL.Tests.Selfhosted.Utility;
using System.Linq;
using System.Collections.Generic;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class CommentsThreaded_Tests
{
    private static int postid;
    private static int comment0id;
    private static int comment00id;
    private static int comment1id;
    private static int comment2id;
    private static int comment3id;
    private static int comment4id;
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task CommentsThreaded_SetupAsync(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
        bool IsValidToken = await _clientAuth.Auth.IsValidJWTokenAsync(testContext.CancellationToken);
        Assert.IsTrue(IsValidToken);

        Post post = await _clientAuth.Posts.CreateAsync(new Post()
        {
            Title = new Title("Title 1"),
            Content = new Content("Content PostCreate")
        }, testContext.CancellationToken);
        await Task.Delay(1000, testContext.CancellationToken);
        Comment comment0 = await _clientAuth.Comments.CreateAsync(new Comment()
        {
            PostId = post.Id,
            Content = new Content("orem ipsum dolor sit amet")
        }, testContext.CancellationToken);

        Comment comment00 = await _clientAuth.Comments.CreateAsync(new Comment()
        {
            PostId = post.Id,
            Content = new Content("r sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam non")
        }, testContext.CancellationToken);

        Comment comment1 = await _clientAuth.Comments.CreateAsync(new Comment()
        {
            PostId = post.Id,
            ParentId = comment0.Id,
            Content = new Content("onsetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna ali")
        }, testContext.CancellationToken);
        Comment comment2 = await _clientAuth.Comments.CreateAsync(new Comment()
        {
            PostId = post.Id,
            ParentId = comment1.Id,
            Content = new Content("ro eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem i")
        }, testContext.CancellationToken);
        Comment comment3 = await _clientAuth.Comments.CreateAsync(new Comment()
        {
            PostId = post.Id,
            ParentId = comment2.Id,
            Content = new Content("tetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam e")
        }, testContext.CancellationToken);
        Comment comment4 = await _clientAuth.Comments.CreateAsync(new Comment()
        {
            PostId = post.Id,
            ParentId = comment1.Id,
            Content = new Content("t ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum do")
        }, testContext.CancellationToken);
        postid = post.Id;
        comment0id = comment0.Id;
        comment00id = comment00.Id;
        comment1id = comment1.Id;
        comment2id = comment2.Id;
        comment3id = comment3.Id;
        comment4id = comment4.Id;
    }

    [TestMethod]
    public async Task CommentsThreaded_Sort()
    {
        List<Comment> allComments = await _clientAuth.Comments.GetAllCommentsForPostAsync(postid, cancellationToken: TestContext.CancellationToken);

        List<CommentThreaded>? threaded = ThreadedCommentsHelper.GetThreadedComments(allComments);
        Assert.IsNotNull(threaded);
        CommentThreaded ct0 = threaded.Find(x => x.Id == comment0id)!;
        Assert.AreEqual(0, ct0.Depth);
        CommentThreaded ct1 = threaded.Find(x => x.Id == comment1id)!;
        Assert.AreEqual(1, ct1.Depth);
        CommentThreaded ct2 = threaded.Find(x => x.Id == comment2id)!;
        Assert.AreEqual(2, ct2.Depth);
        CommentThreaded ct3 = threaded.Find(x => x.Id == comment3id)!;
        Assert.AreEqual(3, ct3.Depth);
        CommentThreaded ct4 = threaded.Find(x => x.Id == comment4id)!;
        Assert.AreEqual(2, ct4.Depth);

        CommentThreaded ct00 = threaded.Find(x => x.Id == comment00id)!;
        Assert.AreEqual(0, ct00.Depth);

        for (int i = 0; i < threaded.Count - 1; i++)
        {
            // The following comment depth has to be the lower, equal or +1
            int ni = i + 1;
            int id = threaded[i].Depth;
            int nid = threaded[ni].Depth;
            bool validDepth = (id >= nid || id + 1 == nid);
            Assert.IsTrue(validDepth);

            System.DateTime idate = threaded[i].Date;
            System.DateTime nidate = threaded[ni].Date;

            // The following comment date has to be
            bool validDate = (
                // newer
                idate <= nidate
                // or older and a child comment
                || (idate > nidate && id > nid));
            Assert.IsTrue(validDate);
        }
    }

    [TestMethod]
    public async Task CommentsThreaded_MaxDepth()
    {
        List<Comment> allComments = await _clientAuth.Comments.GetAllCommentsForPostAsync(postid, cancellationToken: TestContext.CancellationToken);

        List<CommentThreaded>? threaded = ThreadedCommentsHelper.GetThreadedComments(allComments, 1);
        Assert.IsNotNull(threaded);
        CommentThreaded ct0 = threaded.Find(x => x.Id == comment0id)!;
        Assert.AreEqual(0, ct0.Depth);
        CommentThreaded ct1 = threaded.Find(x => x.Id == comment1id)!;
        Assert.AreEqual(1, ct1.Depth);
        CommentThreaded ct2 = threaded.Find(x => x.Id == comment2id)!;
        Assert.AreEqual(1, ct2.Depth);
        CommentThreaded ct3 = threaded.Find(x => x.Id == comment3id)!;
        Assert.AreEqual(1, ct3.Depth);
        CommentThreaded ct4 = threaded.Find(x => x.Id == comment4id)!;
        Assert.AreEqual(1, ct4.Depth);

        CommentThreaded ct00 = threaded.Find(x => x.Id == comment00id)!;
        Assert.AreEqual(0, ct00.Depth);
    }

    [TestMethod]
    public async Task CommentsThreaded_Sort_Extension()
    {
        List<Comment> allComments = await _clientAuth.Comments.GetAllCommentsForPostAsync(postid, cancellationToken: TestContext.CancellationToken);

        Assert.IsTrue(allComments.Any());
        //ExtensionMethod
        List<CommentThreaded>? threaded = ThreadedCommentsHelper.ToThreaded(allComments);
        Assert.IsNotNull(threaded);
        CommentThreaded ct0 = threaded.Find(x => x.Id == comment0id)!;
        Assert.AreEqual(0, ct0.Depth);
        CommentThreaded ct1 = threaded.Find(x => x.Id == comment1id)!;
        Assert.AreEqual(1, ct1.Depth);
        CommentThreaded ct2 = threaded.Find(x => x.Id == comment2id)!;
        Assert.AreEqual(2, ct2.Depth);
        CommentThreaded ct3 = threaded.Find(x => x.Id == comment3id)!;
        Assert.AreEqual(3, ct3.Depth);
        CommentThreaded ct4 = threaded.Find(x => x.Id == comment4id)!;
        Assert.AreEqual(2, ct4.Depth);

        CommentThreaded ct00 = threaded.Find(x => x.Id == comment00id)!;
        Assert.AreEqual(0, ct00.Depth);
        //Assert.AreEqual(threaded.Count, threaded.IndexOf(ct00) + 1);

        for (int i = 0; i < threaded.Count - 1; i++)
        {
            // The following comment depth has to be the lower, equal or +1
            int ni = i + 1;
            int id = threaded[i].Depth;
            int nid = threaded[ni].Depth;
            bool validDepth = (id >= nid || id + 1 == nid);
            Assert.IsTrue(validDepth);

            System.DateTime idate = threaded[i].Date;
            System.DateTime nidate = threaded[ni].Date;

            // The following comment date has to be
            bool validDate = (
                // newer
                idate <= nidate
                // or older and a child comment
                || (idate > nidate && id > nid));
            Assert.IsTrue(validDate);
        }
    }

    [TestMethod]
    public async Task CommentsThreaded_Sort_Extension_Desc()
    {
        List<Comment> allComments = await _clientAuth.Comments.GetAllCommentsForPostAsync(postid, cancellationToken: TestContext.CancellationToken);
        Assert.IsTrue(allComments.Any());

        List<CommentThreaded>? threaded = ThreadedCommentsHelper.ToThreaded(allComments, true);

        // Depth should be the same regardless of desc or asc
        Assert.IsNotNull(threaded);
        CommentThreaded ct0 = threaded.Find(x => x.Id == comment0id)!;
        Assert.AreEqual(0, ct0.Depth);
        CommentThreaded ct1 = threaded.Find(x => x.Id == comment1id)!;
        Assert.AreEqual(1, ct1.Depth);
        CommentThreaded ct2 = threaded.Find(x => x.Id == comment2id)!;
        Assert.AreEqual(2, ct2.Depth);
        CommentThreaded ct3 = threaded.Find(x => x.Id == comment3id)!;
        Assert.AreEqual(3, ct3.Depth);
        CommentThreaded ct4 = threaded.Find(x => x.Id == comment4id)!;
        Assert.AreEqual(2, ct4.Depth);

        CommentThreaded ct00 = threaded.Find(x => x.Id == comment00id)!;
        Assert.AreEqual(0, ct00.Depth);

        for (int i = 0; i < threaded.Count - 1; i++)
        {
            // The following comment depth has to be the lower, equal or +1 at most
            int ni = i + 1;
            int idepth = threaded[i].Depth;
            int nidepth = threaded[ni].Depth;
            int niparent = threaded[ni].ParentId;
            bool validDepth = (idepth >= nidepth || idepth + 1 == nidepth);
            Assert.IsTrue(validDepth);

            System.DateTime idate = threaded[i].Date;
            System.DateTime nidate = threaded[ni].Date;

            // The following comment date has to be
            bool validDate = (
                    // older
                    idate >= nidate
                // or newer, if it's a direct child comment
                || (idate < nidate && threaded[ni].ParentId == threaded[i].Id)
                // or newer, if the comments share the same parent
                || (idate < nidate && nidepth != 0 && idepth >= nidepth));

            Assert.IsTrue(validDate);
        }

        // Comments with depth 0 must be ordered desc
        List<CommentThreaded> firstLvl = threaded.FindAll(x => x.Depth == 0);
        for (int i = 0; i < firstLvl.Count - 1; i++)
        {
            // The following comment depth has to be the lower, equal or +1
            int ni = i + 1;
            System.DateTime idate = threaded[i].Date;
            System.DateTime nidate = threaded[ni].Date;

            // The following comment date has to be older
            Assert.IsGreaterThan(threaded[ni].Id, threaded[i].Id);
            Assert.IsLessThanOrEqualTo(nidate, idate);
        }
    }

    [ClassCleanup]
    public async static Task CommentsThreaded_Cleanup(TestContext testContext)
    {
        await _clientAuth.Posts.DeleteAsync(postid, cancellationToken: testContext.CancellationToken);
    }

    public TestContext TestContext { get; set; }
}
