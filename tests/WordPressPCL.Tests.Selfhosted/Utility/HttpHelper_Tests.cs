using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted.Utility;

[TestClass]
public class HttpHelper_Tests
{
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }


    [TestMethod]
    public async Task HttpHelper_InvalidPreProcessing()
    {
        // Create a random tag , must works:
        string tagname = $"Test {System.Guid.NewGuid()}";
        Tag tag = await _clientAuth.Tags.CreateAsync(new Tag()
        {
            Name = tagname,
            Description = "Test Description"
        }, TestContext.CancellationToken);
        Assert.IsGreaterThan(0, tag.Id);
        Assert.IsNotNull(tag);
        Assert.AreEqual(tagname, tag.Name);
        Assert.AreEqual("Test Description", tag.Description);

        // We call Get tag list without pre processing
        List<Tag> tags = await _clientAuth.Tags.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(tags);
        Assert.AreNotEqual(0, tags.Count);
        CollectionAssert.AllItemsAreUnique(tags.Select(e => e.Id).ToList());

        // Now we add a PreProcessing task
        _clientAuth.HttpResponsePreProcessing = (response) =>
        {
            throw new InvalidOperationException("PreProcessing must fail");
        };
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(async () =>
        {
            await _clientAuth.Tags.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        });
        _clientAuth.HttpResponsePreProcessing = null;
    }

    [TestMethod]
    public async Task HttpHelper_ValidPreProcessing()
    {
        _clientAuth.HttpResponsePreProcessing = null;

        // Create a random tag
        Random random = new();
        string tagname = $"Test {System.Guid.NewGuid()}";
        Tag tag = await _clientAuth.Tags.CreateAsync(new Tag()
        {
            Name = tagname,
            Description = "Test Description"
        }, TestContext.CancellationToken);
        Assert.IsGreaterThan(0, tag.Id);
        Assert.IsNotNull(tag);
        Assert.AreEqual(tagname, tag.Name);
        Assert.AreEqual("Test Description", tag.Description);

        // We call Get tag list without pre processing
        List<Tag> tags = await _clientAuth.Tags.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(tags);
        Assert.AreNotEqual(0, tags.Count);
        CollectionAssert.AllItemsAreUnique(tags.Select(e => e.Id).ToList());

        // Now we add a PreProcessing task
        _clientAuth.HttpResponsePreProcessing = (response) =>
        {
            return response;
        };

        tags = await _clientAuth.Tags.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(tags);
        Assert.AreNotEqual(0, tags.Count);
        CollectionAssert.AllItemsAreUnique(tags.Select(e => e.Id).ToList());
        _clientAuth.HttpResponsePreProcessing = null;
    }

    public TestContext TestContext { get; set; } = null!;
}
