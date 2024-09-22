using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Tests.Selfhosted.Utility;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using System.Linq;
using System.Collections.Generic;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class Tag_Tests
{
    private static WordPressClient _client;
    private static WordPressClient _clientAuth;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _client = ClientHelper.GetWordPressClient();
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);

        string tagname = $"Test {System.Guid.NewGuid()}";
        await _clientAuth.Tags.CreateAsync(new Tag()
        {
            Name = tagname,
            Description = "Test Description"
        });
    }

    [TestMethod]
    public async Task Tags_Create()
    {
        string tagname = $"Test {System.Guid.NewGuid()}";
        Tag tag = await _clientAuth.Tags.CreateAsync(new Tag()
        {
            Name = tagname,
            Description = "Test Description"
        });
        Assert.IsNotNull(tag);
        Assert.AreEqual(tagname, tag.Name);
        Assert.AreEqual("Test Description", tag.Description);
    }

    [TestMethod]
    public async Task Tags_Read()
    {
        List<Tag> tags = await _clientAuth.Tags.GetAllAsync();
        Assert.IsNotNull(tags);
        Assert.AreNotEqual(tags.Count, 0);
        CollectionAssert.AllItemsAreUnique(tags.Select(tag => tag.Id).ToList());
    }

    [TestMethod]
    public async Task Tags_Get()
    {
        List<Tag> tags = await _client.Tags.GetAsync();
        Assert.IsNotNull(tags);
        Assert.AreNotEqual(tags.Count, 0);
        CollectionAssert.AllItemsAreUnique(tags.Select(tag => tag.Id).ToList());
    }

    [TestMethod]
    public async Task Tags_Update()
    {
        List<Tag> tags = await _clientAuth.Tags.GetAllAsync();
        Tag tag = tags.FirstOrDefault();
        if(tag == null)
        {
            Assert.Inconclusive();
        }
        string tagname = $"Testname {System.Guid.NewGuid()}";
        string tagdesc = "Test Description";
        tag.Name = tagname;
        tag.Description = tagdesc;
        Tag tagUpdated = await _clientAuth.Tags.UpdateAsync(tag);
        Assert.AreEqual(tagname, tagUpdated.Name);
        Assert.AreEqual(tagdesc, tagUpdated.Description);
    }

    [TestMethod]
    public async Task Tags_Delete()
    {
        List<Tag> tags = await _clientAuth.Tags.GetAllAsync();
        Tag tag = tags.FirstOrDefault();
        if (tag == null)
        {
            Assert.Inconclusive();
        }
        int tagId = tag.Id;
        bool response = await _clientAuth.Tags.DeleteAsync(tagId);
        Assert.IsTrue(response);
        tags = await _clientAuth.Tags.GetAllAsync();
        List<Tag> tagsWithId = tags.Where(x => x.Id == tagId).ToList();
        Assert.AreEqual(tagsWithId.Count, 0);
    }
    [TestMethod]
    public async Task Tags_Query()
    {
        TagsQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 15,
            OrderBy = TermsOrderBy.Id,
            Order = Order.DESC,
        };
        List<Tag> queryresult = await _clientAuth.Tags.QueryAsync(queryBuilder);
        Assert.AreEqual("?page=1&per_page=15&orderby=id&order=desc&context=view", queryBuilder.BuildQuery());
        Assert.IsNotNull(queryresult);
        Assert.AreNotSame(queryresult.Count, 0);
    }

}

