using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCLTests.Utility;
using System.Linq;
using WordPressPCL.Models;
using WordPressPCL;
using WordPressPCL.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class User_Tests
    {
        [TestMethod]
        public async Task Users_Create()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var user = await client.Users.Create(new User("test", "test@test.test", "test")
            {
                NickName = "test",
                Name = "test",
                FirstName = "First",
                LastName = "Last"
            });
            Assert.IsNotNull(user);
            Assert.AreEqual("test", user.NickName);
            Assert.AreEqual("test", user.Name);
            Assert.AreEqual("First", user.FirstName);
            Assert.AreEqual("Last", user.LastName);
            Assert.AreEqual("test", user.UserName);
            Assert.AreEqual("test@test.test", user.Email);

        }
        [TestMethod]
        public async Task Users_Read()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var users = await client.Users.GetAll();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count() >= 1);
            var user = await client.Users.GetByID(users.First().Id);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, users.First().Id);
        }
        [TestMethod]
        public async Task Users_Update()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public async Task Users_Delete()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public async Task Users_Query()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var queryBuilder = new UsersQueryBuilder()
            {
                Page = 1,
                PerPage = 15,
                OrderBy = UsersOrderBy.Name,
                Order = Order.DESC,
            };
            var queryresult = await client.Users.Query(queryBuilder);
            Assert.AreEqual(queryBuilder.BuildQueryURL(), "?page=1&per_page=15&order=desc");
            Assert.IsNotNull(queryresult);
            Assert.AreNotSame(queryresult.Count(), 0);
        }

    }
}
