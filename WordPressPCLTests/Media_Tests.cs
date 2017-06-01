using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCLTests.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class Media_Tests
    {
        

        [TestMethod]
        public async Task MediaSizesInEmbeddedPost()
        {
            // Initialize
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var posts = await client.ListPosts(true);
            var i = 0;
            foreach (var post in posts) {
                if (post.Embedded.WpFeaturedmedia != null && post.Embedded.WpFeaturedmedia.Count != 0)
                {
                    i++;
                    var img = post.Embedded.WpFeaturedmedia[0];
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
