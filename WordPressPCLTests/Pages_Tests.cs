using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCLTests.Utility;
using System.Linq;
using WordPressPCL;
using WordPressPCL.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class Pages_Tests
    {
        [TestMethod]
        public async Task Pages_Create()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var post = new Page()
            {
                Title = new Title()
                {
                    Raw = "New Title"
                },
                Content = new Content()
                {
                    Raw = "Test Raw Content"
                },
                Date = DateTime.Now,
                DateGmt = DateTime.UtcNow
            };
            var post2 = new Page()
            {
                Title = new Title("Title 1"),
                Content = new Content("Content PostCreate")
            };
            var createdPost = await client.Pages.Create(post2);


            Assert.AreEqual(post2.Content.Raw, createdPost.Content.Raw);
            Assert.IsTrue(createdPost.Content.Rendered.Contains(post2.Content.Rendered));
        }

        [TestMethod]
        public async Task Pages_Read()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var pages = await client.Pages.Query(new PagesQueryBuilder());
            Assert.IsNotNull(pages);
            Assert.AreNotEqual(pages.Count(), 0);
        }


        [TestMethod]
        public async Task Pages_Update()
        {
            var testContent = "Test" + new Random().Next();
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var pages = await client.Pages.GetAll();
            Assert.IsTrue(pages.Count() > 0);

            // edit first post and update it
            var post = await client.Pages.GetByID(pages.First().Id);
            post.Content.Raw = testContent;
            var updatedPost = await client.Pages.Update(post);
            Assert.AreEqual(updatedPost.Content.Raw, testContent);
            Assert.IsTrue(updatedPost.Content.Rendered.Contains(testContent));
        }

        [TestMethod]
        public async Task Pages_Delete()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public async Task Pages_Query()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var queryBuilder = new PagesQueryBuilder()
            {
                Page = 1,
                PerPage = 15,
                OrderBy = PagesOrderBy.Title,
                Order = Order.DESC,
                Statuses = new Status[] { Status.Publish },
                Embed=true
            };
            var queryresult = await client.Pages.Query(queryBuilder);
            Assert.AreEqual(queryBuilder.BuildQueryURL(), "?page=1&per_page=15&orderby=title&status=publish&order=desc&_embed=true");
            Assert.IsNotNull(queryresult);
            Assert.AreNotSame(queryresult.Count(), 0);
        }
    }
}
