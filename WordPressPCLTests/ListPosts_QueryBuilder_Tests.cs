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
    public class ListPosts_QueryBuilder_Tests
    {
        private const int CATEGORY_ID = 7;
        private const int TAG_ID = 1;
        private const int AUTHOR_ID = 1;
        private const string SEARCH_TERM = "demo";

        [TestInitialize]
        public void Setup() {
            ApiCredentials.WordPressUri = "https://demo.wp-api.org/wp-json/";
        }

        [TestMethod]
        public async Task List_Posts_QueryBuilder_Test_Pagination()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var postsA = await client.ListPosts(new QueryBuilder() {
                Page = 1
            });
            var postsB = await client.ListPosts(new QueryBuilder() {
                Page = 2
            });
            Assert.IsNotNull(postsA);
            Assert.IsNotNull(postsB);
            Assert.AreNotEqual(postsA.Count, 0);
            Assert.AreNotEqual(postsB.Count, 0);
            CollectionAssert.AreNotEqual(postsA.Select(post => post.Id).ToList(), postsB.Select(post => post.Id).ToList());
        }

        [TestMethod]
        [Description("Test that the ListPosts method with the QueryBuilder override works the same way as before when `QueryBuilder` is set with default values")]
        public async Task List_Posts_QueryBuilder_Works_The_Same_As_Before()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var postsA = await client.ListPosts(new QueryBuilder());
            var postsB = await client.ListPosts();
            Assert.IsNotNull(postsA);
            Assert.IsNotNull(postsB);
            Assert.AreNotEqual(postsA.Count, 0);
            Assert.AreNotEqual(postsB.Count, 0);
            CollectionAssert.AreEqual(postsA.Select(post => post.Id).ToList(), postsB.Select(post => post.Id).ToList());
        }

        [TestMethod]
        [Description("Test that the ListStickyPosts method with the QueryBuilder override works the same way as before when `QueryBuilder` is set with default values")]
        public async Task List_Sticky_Posts_QueryBuilder_Works_The_Same_As_Before()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var postsA = await client.ListStickyPosts(new QueryBuilder());
            var postsB = await client.ListStickyPosts();
            Assert.IsNotNull(postsA);
            Assert.IsNotNull(postsB);
            CollectionAssert.AreEqual(postsA.Select(post => post.Id).ToList(), postsB.Select(post => post.Id).ToList());
        }

        [TestMethod]
        [Description("Test that the ListPostsByCategory method with the QueryBuilder override works the same way as before when `QueryBuilder` is set with default values")]
        public async Task List_Posts_By_Category_QueryBuilder_Works_The_Same_As_Before()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var postsA = await client.ListPostsByCategory(CATEGORY_ID, new QueryBuilder());
            var postsB = await client.ListPostsByCategory(CATEGORY_ID);
            Assert.IsNotNull(postsA);
            Assert.IsNotNull(postsB);
            Assert.AreNotEqual(postsA.Count, 0);
            Assert.AreNotEqual(postsB.Count, 0);
            CollectionAssert.AreEqual(postsA.Select(post => post.Id).ToList(), postsB.Select(post => post.Id).ToList());
        }

        [TestMethod]
        [Description("Test that the ListPostsByTag method with the QueryBuilder override works the same way as before when `QueryBuilder` is set with default values")]
        public async Task List_Posts_By_Tag_QueryBuilder_Works_The_Same_As_Before()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var postsA = await client.ListPostsByTag(TAG_ID, new QueryBuilder());
            var postsB = await client.ListPostsByTag(TAG_ID);
            Assert.IsNotNull(postsA);
            Assert.IsNotNull(postsB);
            CollectionAssert.AreEqual(postsA.Select(post => post.Id).ToList(), postsB.Select(post => post.Id).ToList());
        }

        [TestMethod]
        [Description("Test that the ListPostsByAuthor method with the QueryBuilder override works the same way as before when `QueryBuilder` is set with default values")]
        public async Task List_Posts_By_Author_QueryBuilder_Works_The_Same_As_Before()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var postsA = await client.ListPostsByAuthor(AUTHOR_ID, new QueryBuilder());
            var postsB = await client.ListPostsByAuthor(AUTHOR_ID);
            Assert.IsNotNull(postsA);
            Assert.IsNotNull(postsB);
            Assert.AreNotEqual(postsA.Count, 0);
            Assert.AreNotEqual(postsB.Count, 0);
            CollectionAssert.AreEqual(postsA.Select(post => post.Id).ToList(), postsB.Select(post => post.Id).ToList());
        }

        [TestMethod]
        [Description("Test that the ListPostsBySearch method with the QueryBuilder override works the same way as before when `QueryBuilder` is set with default values")]
        public async Task List_Posts_By_Search_QueryBuilder_Works_The_Same_As_Before()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            // Posts
            var postsA = await client.ListPostsBySearch(SEARCH_TERM, new QueryBuilder());
            var postsB = await client.ListPostsBySearch(SEARCH_TERM);
            Assert.IsNotNull(postsA);
            Assert.IsNotNull(postsB);
            Assert.AreNotEqual(postsA.Count, 0);
            Assert.AreNotEqual(postsB.Count, 0);
            CollectionAssert.AreEqual(postsA.Select(post => post.Id).ToList(), postsB.Select(post => post.Id).ToList());
        }
    }
}