using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL.Models;
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
            var id = await CreatePostWithRevision();
            var revisionsclient = client.Posts.Revisions(id);
            var revisions = await revisionsclient.GetAll();
            Assert.AreNotEqual(revisions.Count(), 0);
        }

        [TestMethod]
        public async Task PostRevisions_Get()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var id = await CreatePostWithRevision();
            var revisionsclient = client.Posts.Revisions(id);
            var revisions = await revisionsclient.Get();
            Assert.AreNotEqual(revisions.Count(), 0);
        }

        [TestMethod]
        public async Task PostRevisions_Delete()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var id = await CreatePostWithRevision();

            var revisionsclient = client.Posts.Revisions(id);
            var revisions = await revisionsclient.GetAll();
            Assert.AreNotEqual(revisions.Count(), 0);
            var res = await revisionsclient.Delete(revisions.First().Id);
            Assert.IsTrue(res.IsSuccessStatusCode);
        }

        private async Task<int> CreatePostWithRevision()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();

            var post = new Post()
            {
                Title = new Title("Title 1"),
                Content = new Content("Content PostCreate"),
            };
            var createdPost = await client.Posts.Create(post);
            createdPost.Content.Raw = "Updated Content";
            var updatedPost = await client.Posts.Update(createdPost);
            return updatedPost.Id;
        }
    }
}