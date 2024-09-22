using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WordPressPCL.Tests.Selfhosted.Utility;
using System.Linq;
using WordPressPCL.Utility;
using WordPressPCL.Models;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Media_Tests
{
    private static WordPressClient _client;
    private static WordPressClient _clientAuth;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _client = ClientHelper.GetWordPressClient();
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Media_Create()
    {
        string path = Directory.GetCurrentDirectory() + "/Assets/cat.jpg";
        Stream s = File.OpenRead(path);
        MediaItem mediaitem = await _clientAuth.Media.CreateAsync(s,"cat.jpg");
        Assert.IsNotNull(mediaitem);
    }

    [TestMethod]
    public async Task Media_Create_2_0()
    {
        string path = Directory.GetCurrentDirectory() + "/Assets/cat.jpg";

        MediaItem mediaitem = await _clientAuth.Media.CreateAsync(path, "cat.jpg");
        Assert.IsNotNull(mediaitem);

        // Create a new post with media item as featured image
        Post post = new()
        {
            Title = new Title("Post with Featured Image"),
            Content = new Content("Content PostCreate"),
            FeaturedMedia = mediaitem.Id
        };
        Post createdPost = await _clientAuth.Posts.CreateAsync(post);

        Assert.AreEqual(createdPost.FeaturedMedia, mediaitem.Id);
    }

    [TestMethod]
    public async Task Media_With_Exif_Error_Should_Deserialize_Without_Exception() 
    {
        string path = Directory.GetCurrentDirectory() + "/Assets/img_exif_error.jpg";
        MediaItem mediaItem = null;
        try {
            mediaItem = await _clientAuth.Media.CreateAsync(path, "img_exif_error.jpg");
        } catch (JsonReaderException) {
            Assert.Fail("Could not deserialize created Media due to JsonReaderException");
        }
        Assert.IsNotNull(mediaItem);
    }

    [TestMethod]
    public async Task Media_Read()
    {
        List<MediaItem> media = await _client.Media.GetAllAsync();
        Assert.IsNotNull(media);
        Assert.AreNotEqual(media.Count, 0);
    }

    [TestMethod]
    public async Task Media_Get()
    {
        List<MediaItem> media = await _client.Media.GetAsync();
        Assert.IsNotNull(media);
        Assert.AreNotEqual(media.Count, 0);
    }

    [TestMethod]
    public async Task Media_Update()
    {
        List<MediaItem> media = await _clientAuth.Media.GetAllAsync();
        MediaItem file = media.FirstOrDefault();
        Assert.IsNotNull(file);

        string title = $"New Title {System.Guid.NewGuid()}";
        Assert.AreNotEqual(title, file.Title.Raw);
        file.Title.Raw = title;

        string desc = $"This is a nice cat! {System.Guid.NewGuid()}";
        file.Description.Raw = desc;

        MediaItem fileUpdated = await _clientAuth.Media.UpdateAsync(file);
        Assert.IsNotNull(fileUpdated);
        Assert.AreEqual(fileUpdated.Title.Raw, title);
        Assert.AreEqual(fileUpdated.Description.Raw, desc);
    }

    [TestMethod]
    public async Task Media_Delete()
    {
        // Create file
        string path = Directory.GetCurrentDirectory() + "/Assets/cat.jpg";
        Stream s = File.OpenRead(path);
        MediaItem mediaitem = await _clientAuth.Media.CreateAsync(s, "cat.jpg");
        Assert.IsNotNull(mediaitem);

        // Delete file
        bool response = await _clientAuth.Media.DeleteAsync(mediaitem.Id);
        Assert.IsTrue(response);
    }

    [TestMethod]
    public async Task Media_Query()
    {
        MediaQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 15,
            OrderBy = MediaOrderBy.Date,
            Order = Order.ASC,
        };
        List<MediaItem> queryresult = await _clientAuth.Media.QueryAsync(queryBuilder);
        Assert.AreEqual("?page=1&per_page=15&orderby=date&order=asc&context=view", queryBuilder.BuildQuery());
        Assert.IsNotNull(queryresult);
        Assert.AreNotSame(queryresult.Count, 0);
    }

    [TestMethod]
    public async Task MediaSizesInEmbeddedPost()
    {
        List<Post> posts = await _client.Posts.GetAllAsync(true);
        int i = 0;
        foreach (Post post in posts) {
            if (post.Embedded.WpFeaturedmedia != null && post.Embedded.WpFeaturedmedia.Any())
            {
                i++;
                MediaItem img = post.Embedded.WpFeaturedmedia.First();
                Assert.IsFalse(string.IsNullOrEmpty(img.MediaDetails.Sizes["full"].SourceUrl));
            }
        }
        if (i == 0)
        {
            Assert.Inconclusive("no images to test");
        }
    }
}
