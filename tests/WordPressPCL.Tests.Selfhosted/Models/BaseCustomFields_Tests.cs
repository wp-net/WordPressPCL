using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.Json;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted.Models;

[TestClass]
public class BaseCustomFields_Tests
{
    private const string PostWithUnmappedFields = """
    {
        "id": 7,
        "slug": "a-post",
        "checksum": "123",
        "gallery": [1, 2, 3],
        "_links": { "self": [{ "href": "https://example.org/wp-json/wp/v2/posts/7" }] }
    }
    """;

    [TestMethod]
    public void CustomFields_CollectsUnmappedProperties()
    {
        Post? post = JsonSerializer.Deserialize<Post>(PostWithUnmappedFields);

        Assert.IsNotNull(post);
        Assert.AreEqual(7, post.Id);
        Assert.AreEqual("a-post", post.Slug);
        Assert.IsNotNull(post.CustomFields);
        CollectionAssert.AreEquivalent(new[] { "checksum", "gallery" }, new List<string>(post.CustomFields.Keys));
    }

    [TestMethod]
    public void CustomFields_LeavesMappedPropertiesAlone()
    {
        Post? post = JsonSerializer.Deserialize<Post>(PostWithUnmappedFields);

        Assert.IsNotNull(post);
        Assert.IsNotNull(post.Links);
        Assert.IsNotNull(post.CustomFields);
        Assert.IsFalse(post.CustomFields.ContainsKey("_links"));
        Assert.IsFalse(post.CustomFields.ContainsKey("id"));
        Assert.IsFalse(post.CustomFields.ContainsKey("slug"));
    }

    [TestMethod]
    public void CustomFields_IsNullWhenEverythingIsMapped()
    {
        Post? post = JsonSerializer.Deserialize<Post>("""{"id":7,"slug":"a-post"}""");

        Assert.IsNotNull(post);
        Assert.IsNull(post.CustomFields);
    }

    [TestMethod]
    public void CustomFields_AreWrittenAsTopLevelProperties()
    {
        Post post = new()
        {
            Slug = "a-post",
            CustomFields = new Dictionary<string, object>
            {
                ["checksum"] = "123",
                ["gallery"] = new[] { 1, 2, 3 },
            },
        };

        string json = JsonSerializer.Serialize(post);

        using JsonDocument document = JsonDocument.Parse(json);
        JsonElement root = document.RootElement;
        Assert.AreEqual("123", root.GetProperty("checksum").GetString());
        Assert.AreEqual(3, root.GetProperty("gallery").GetArrayLength());
        Assert.IsFalse(root.TryGetProperty("custom_fields", out _), "Entries must not be nested under a wrapper property.");
    }

    [TestMethod]
    public void CustomFields_AddNothingToPayloadWhenUnset()
    {
        string fromNull = JsonSerializer.Serialize(new Post { Slug = "a-post" });
        string fromEmpty = JsonSerializer.Serialize(new Post
        {
            Slug = "a-post",
            CustomFields = new Dictionary<string, object>(),
        });

        Assert.AreEqual(fromNull, fromEmpty);
    }

    [TestMethod]
    public void CustomFields_SurviveARoundTrip()
    {
        Post? post = JsonSerializer.Deserialize<Post>(PostWithUnmappedFields);
        Assert.IsNotNull(post);

        Post? roundTripped = JsonSerializer.Deserialize<Post>(JsonSerializer.Serialize(post));

        Assert.IsNotNull(roundTripped);
        Assert.IsNotNull(roundTripped.CustomFields);
        Assert.AreEqual("123", ((JsonElement)roundTripped.CustomFields["checksum"]).GetString());
    }
}
