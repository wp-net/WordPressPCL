using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class PostRevisions_Tests
{
    private static WordPressClient _clientAuth;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task PostRevisions_Read()
    {
        int id = await CreatePostWithRevision();
        Client.PostRevisions revisionsclient = _clientAuth.Posts.Revisions(id);
        List<PostRevision> revisions = await revisionsclient.GetAllAsync();
        Assert.AreNotEqual(revisions.Count, 0);
    }

    [TestMethod]
    public async Task PostRevisions_Get()
    {
        int id = await CreatePostWithRevision();
        Client.PostRevisions revisionsclient = _clientAuth.Posts.Revisions(id);
        List<PostRevision> revisions = await revisionsclient.GetAsync();
        Assert.AreNotEqual(revisions.Count, 0);
    }

    // TODO: check why revision can't be deleted
    //[TestMethod]
    public async Task PostRevisions_Delete()
    {
        int id = await CreatePostWithRevision();

        Client.PostRevisions revisionsclient = _clientAuth.Posts.Revisions(id);
        List<PostRevision> revisions = await revisionsclient.GetAllAsync();
        Assert.AreNotEqual(revisions.Count, 0);
        bool res = await revisionsclient.DeleteAsync(revisions.First().Id);
        Assert.IsTrue(res);
    }

    private async Task<int> CreatePostWithRevision()
    {
        Post post = new()
        {
            Title = new Title("Title 1"),
            Content = new Content("Content PostCreate"),
        };
        Post createdPost = await _clientAuth.Posts.CreateAsync(post);
        createdPost.Content.Raw = "Updated Content";
        Post updatedPost = await _clientAuth.Posts.UpdateAsync(createdPost);
        return updatedPost.Id;
    }
}
