using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCLTests.Utility;
using System.Linq;
using WordPressPCL.Utility;
using WordPressPCL.Models;
using System.IO;

namespace WordPressPCLTests
{
    [TestClass]
    public class Media_Tests
    {
        
        [TestMethod]
        public async Task Media_Create()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            Stream s = File.OpenRead(@"1.jpg");
            var mediaitem = await client.Media.Create(s,"1.jpg");
            Assert.IsNotNull(mediaitem);
        }
        [TestMethod]
        public async Task Media_Read()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            Assert.IsNotNull(client);
            var media = await client.Media.GetAll();
            Assert.IsNotNull(media);
            Assert.AreNotEqual(media.Count(), 0);
        }
        [TestMethod]
        public async Task Media_Update()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public async Task Media_Delete()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public async Task Media_Query()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var queryBuilder = new MediaQueryBuilder()
            {
                Page = 1,
                PerPage = 15,
                OrderBy = MediaOrderBy.Date,
                Order = Order.DESC,
            };
            var queryresult = await client.Media.Query(queryBuilder);
            Assert.AreEqual(queryBuilder.BuildQueryURL(), "?page=1&per_page=15&order=desc");
            Assert.IsNotNull(queryresult);
            Assert.AreNotSame(queryresult.Count(), 0);
        }

        [TestMethod]
        public async Task MediaSizesInEmbeddedPost()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.Posts.GetAll(true);
            var i = 0;
            foreach (var post in posts) {
                if (post.Embedded.WpFeaturedmedia != null && post.Embedded.WpFeaturedmedia.Count() != 0)
                {
                    i++;
                    var img = post.Embedded.WpFeaturedmedia.First();
                    Assert.IsFalse(String.IsNullOrEmpty(img.MediaDetails.Sizes["full"].SourceUrl));
                }
                if(i == 0)
                {
                    Assert.Inconclusive("no images to test");
                }
            }
        }
    }
}
