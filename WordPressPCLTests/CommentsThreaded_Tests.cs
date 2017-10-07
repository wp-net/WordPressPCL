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
using System.Diagnostics;

namespace WordPressPCLTests
{
    [TestClass]
    public class CommentsThreaded_Tests
    {
        [TestMethod]
        public async Task CommentsThreaded_Sort()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var IsValidToken = await client.IsValidJWToken();
            Assert.IsTrue(IsValidToken);

            var post = await client.Posts.Create(new Post()
            {
                Title = new Title("Title 1"),
                Content = new Content("Content PostCreate")
            });
            await Task.Delay(1000);
            var comment0 = await client.Comments.Create(new Comment()
            {
                PostId = post.Id,
                Content = new Content("orem ipsum dolor sit amet")
            });

            var comment00 = await client.Comments.Create(new Comment()
            {
                PostId = post.Id,
                Content = new Content("r sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam non")
            });

            var comment1 = await client.Comments.Create(new Comment()
            {
                PostId = post.Id,
                ParentId = comment0.Id,
                Content = new Content("onsetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna ali")
            });
            var comment2 = await client.Comments.Create(new Comment()
            {
                PostId = post.Id,
                ParentId = comment1.Id,
                Content = new Content("ro eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem i")
            });
            var comment3 = await client.Comments.Create(new Comment()
            {
                PostId = post.Id,
                ParentId = comment2.Id,
                Content = new Content("tetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam e")
            });
            var comment4 = await client.Comments.Create(new Comment()
            {
                PostId = post.Id,
                ParentId = comment1.Id,
                Content = new Content("t ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum do")
            });

            var allComments = await client.Comments.GetAllCommentsForPost(post.Id);

            var threaded = ThreadedCommentsHelper.GetThreadedComments(allComments);
            Debug.WriteLine($"threaded count: {threaded.Count}");
            Assert.IsNotNull(threaded);
            var ct0 = threaded.Find(x => x.Id == comment0.Id);
            Assert.AreEqual(ct0.Depth, 0);
            Debug.WriteLine(threaded.IndexOf(ct0));
            var ct1 = threaded.Find(x => x.Id == comment1.Id);
            Assert.AreEqual(ct1.Depth, 1);
            Debug.WriteLine(threaded.IndexOf(ct1));
            var ct2 = threaded.Find(x => x.Id == comment2.Id);
            Assert.AreEqual(ct2.Depth, 2);
            Debug.WriteLine(threaded.IndexOf(ct2));
            var ct3 = threaded.Find(x => x.Id == comment3.Id);
            Assert.AreEqual(ct3.Depth, 3);
            Debug.WriteLine(threaded.IndexOf(ct3));
            var ct4 = threaded.Find(x => x.Id == comment4.Id);
            Assert.AreEqual(ct4.Depth, 2);
            Debug.WriteLine(threaded.IndexOf(ct4));

            var ct00 = threaded.Find(x => x.Id == comment00.Id);
            Assert.AreEqual(ct00.Depth, 0);
            //Assert.AreEqual(threaded.Count, threaded.IndexOf(ct00) + 1);

            for (int i = 0; i < threaded.Count - 1; i++)
            {
                // The following comment depth has to be the lower, equal or +1
                var ni = i + 1;
                var id = threaded[i].Depth;
                var nid = threaded[ni].Depth;
                var validDepth = (id >= nid || id + 1 == nid);
                Assert.IsTrue(validDepth);

                var idate = threaded[i].Date;
                var nidate = threaded[ni].Date;

                // The following comment date has to be newer or older with a lower depth 
                var validDate = (idate <= nidate || (idate > nidate && id > nid));
                Assert.IsTrue(validDate);
            }

            // cleanup
            await client.Posts.Delete(post.Id);
        }


    }

}