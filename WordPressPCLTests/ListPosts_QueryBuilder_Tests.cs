using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCLTests.Utility;
using WordPressPCL;
using System.Threading.Tasks;
using WordPressPCL.Utility;
using System.Linq;

namespace WordPressPCLTests
{
    [TestClass]
    public class ListPosts_QueryBuilder_Tests
    {
        private const int CATEGORY_ID = 1;
        private const int TAG_ID = 7;
        private const int AUTHOR_ID = 3;
        private const string SEARCH_TERM = "Artikel";

        [TestMethod]
        public async Task List_Posts_QueryBuilder_Test_Pagination()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var postsA = await client.Posts.Query(new PostsQueryBuilder() {
                Page = 1
            });
            var postsB = await client.Posts.Query(new PostsQueryBuilder() {
                Page = 2
            });
            Assert.IsNotNull(postsA);
            Assert.IsNotNull(postsB);
            Assert.AreNotEqual(postsA.Count(), 0);
            Assert.AreNotEqual(postsB.Count(), 0);
            CollectionAssert.AreNotEqual(postsA.Select(post => post.Id).ToList(), postsB.Select(post => post.Id).ToList());
        }
        

        [TestMethod]
        [Description("Test that the ListPosts method with the QueryBuilder set to list posts published After a specific date works.")]
        public async Task List_Posts_QueryBuilder_After()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var posts = await client.Posts.Query(new PostsQueryBuilder { After = System.DateTime.Parse("2017-05-22T13:41:09") });
            Assert.IsNotNull(posts);
        }
    }
}