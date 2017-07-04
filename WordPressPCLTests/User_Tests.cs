using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCLTests.Utility;
using System.Linq;

namespace WordPressPCLTests
{
    [TestClass]
    public class User_Tests
    {
        [TestMethod]
        public async Task List_Users_Test()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var users = await client.Users.GetAll();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count() >= 1);
            var user = await client.Users.GetByID(users.First().Id);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, users.First().Id);
        } 
        
    }
}
