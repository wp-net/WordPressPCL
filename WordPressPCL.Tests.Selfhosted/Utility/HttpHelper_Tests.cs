using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted.Utility
{
    [TestClass]
    public class HttpHelper_Tests
    {
        [TestMethod]
        public async Task HttpHelper_InvalidPreProcessing(TestContext testContext)
        {
            var clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
            // Create a random tag , must works:
            var random = new Random();
            var tagname = $"Test {random.Next(0, 1000)}";
            var tag = await clientAuth.Tags.Create(new Tag()
            {
                Name = tagname,
                Description = "Test Description"
            });
            Assert.IsTrue(tag.Id > 0);
            Assert.IsNotNull(tag);
            Assert.AreEqual(tagname, tag.Name);
            Assert.AreEqual("Test Description", tag.Description);

            // We call Get tag list without pre processing
            var tags = await clientAuth.Tags.GetAll();
            Assert.IsNotNull(tags);
            Assert.AreNotEqual(tags.Count(), 0);
            CollectionAssert.AllItemsAreUnique(tags.Select(e => e.Id).ToList());

            // Now we add a PreProcessing task
            clientAuth.HttpResponsePreProcessing = (response) =>
            {
                throw new InvalidOperationException("PreProcessing must fail");
            };
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
            {
                await clientAuth.Tags.GetAll();
            });
        }

        [TestMethod]
        public async Task HttpHelper_ValidPreProcessing(TestContext testContext)
        {
            var clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);

            // Create a random tag
            var random = new Random();
            var tagname = $"Test {random.Next(0, 1000)}";
            var tag = await clientAuth.Tags.Create(new Tag()
            {
                Name = tagname,
                Description = "Test Description"
            });
            Assert.IsTrue(tag.Id > 0);
            Assert.IsNotNull(tag);
            Assert.AreEqual(tagname, tag.Name);
            Assert.AreEqual("Test Description", tag.Description);

            // We call Get tag list without pre processing
            var tags = await clientAuth.Tags.GetAll();
            Assert.IsNotNull(tags);
            Assert.AreNotEqual(tags.Count(), 0);
            CollectionAssert.AllItemsAreUnique(tags.Select(e => e.Id).ToList());

            // Now we add a PreProcessing task
            clientAuth.HttpResponsePreProcessing = (response) =>
            {
                return response;
            };

            tags = await clientAuth.Tags.GetAll();
            Assert.IsNotNull(tags);
            Assert.AreNotEqual(tags.Count(), 0);
            CollectionAssert.AllItemsAreUnique(tags.Select(e => e.Id).ToList());
        }
    }
}
