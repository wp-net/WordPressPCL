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
    public class User_Tests
    {
        [TestMethod]
        public async Task List_Users_Test()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var users = await client.ListUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count >= 1);
            var user = await client.GetUser(users[0].Id);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, users[0].Id);
        } 
        
    }
}
