using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCLTests.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class PostRevisions_Tests
    {
        [TestMethod]
        public async Task PostRevisions_Read()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var posts = await client.Posts.GetAll();
            Assert.AreNotEqual(posts.Count(), 0);
            var revisionsclient = client.Posts.Revisions(posts.First().Id);
            var revisions = await revisionsclient.GetAll();
            Assert.AreNotEqual(revisions.Count(), 0);
        }

        [TestMethod]
        public async Task PostRevisions_Delete()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var posts = await client.Posts.GetAll();
            Assert.AreNotEqual(posts.Count(), 0);
            var revisionsclient = client.Posts.Revisions(posts.First().Id);
            var revisions = await revisionsclient.GetAll();
            Assert.AreNotEqual(revisions.Count(), 0);
            var res = await revisionsclient.Delete(revisions.First().Id);
            Assert.IsTrue(res.IsSuccessStatusCode);
        }
    }
}