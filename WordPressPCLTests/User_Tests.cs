using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;
using WordPressPCL.Utility;
using WordPressPCLTests.Utility;

namespace WordPressPCLTests
{
    [TestClass]
    public class User_Tests
    {
        [TestMethod]
        public async Task Users_Create()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();

            var random = new Random();
            var r = random.Next(0, 1000);
            var username = $"Testuser{r}";
            var nickname = $"Nickname{r}";
            var email = $"testuser{r}@test.com";
            var firstname = $"Firstname{r}";
            var lastname = $"Lastname{r}";
            var password = $"testpassword{r}";
            var name = $"{firstname} {lastname}";

            var user = await client.Users.Create(new User(username, email, password)
            {
                NickName = nickname,
                Name = name,
                FirstName = firstname,
                LastName = lastname
            });
            Assert.IsNotNull(user);
            Assert.AreEqual(nickname, user.NickName);
            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(firstname, user.FirstName);
            Assert.AreEqual(lastname, user.LastName);
            Assert.AreEqual(username, user.UserName);
            Assert.AreEqual(email, user.Email);
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
        public async Task Users_Get()
        {
            var client = new WordPressClient(ApiCredentials.WordPressUri);
            var users = await client.Users.Get();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count() >= 1);
            var user = await client.Users.GetByID(users.First().Id);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, users.First().Id);
        }

        [TestMethod]
        public async Task Users_Update()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();
            var user = await CreateRandomUser(client);
            Assert.IsNotNull(user);

            var random = new Random();
            var r = random.Next(0, 1000);
            var name = $"Testuser{r}";
            user.Name = name;
            user.NickName = name;
            user.FirstName = name;
            user.LastName = name;

            var updatedUser = await client.Users.Update(user);
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(updatedUser.Name, name);
            Assert.AreEqual(updatedUser.NickName, name);
            Assert.AreEqual(updatedUser.FirstName, name);
            Assert.AreEqual(updatedUser.LastName, name);
        }

        [TestMethod]
        public async Task Users_Delete()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();

            var random = new Random();
            var r = random.Next(0, 1000);
            var username = $"Testuser{r}";
            var email = $"testuser{r}@test.com";
            var password = $"testpassword{r}";

            var user = await client.Users.Create(new User(username, email, password));
            Assert.IsNotNull(user);
            var me = await client.Users.GetCurrentUser();
            var response = await client.Users.Delete(user.Id, me.Id);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Users_Delete_And_Reassign_Posts()
        {
            var client = await ClientHelper.GetAuthenticatedWordPressClient();

            var random = new Random();

            // Create new user
            var user1 = await CreateRandomUser(client);
            Assert.IsNotNull(user1);
            var user2 = await CreateRandomUser(client);
            Assert.IsNotNull(user2);

            // Create post for user
            var post = new Post()
            {
                Title = new Title("Title 1"),
                Content = new Content("Content PostCreate"),
                Author = user1.Id
            };
            var postCreated = await client.Posts.Create(post);
            Assert.IsNotNull(post);
            Assert.AreEqual(postCreated.Author, user1.Id);

            // Delete User1 and reassign posts to user2
            var response = await client.Users.Delete(user1.Id, user2.Id);
            Assert.IsTrue(response.IsSuccessStatusCode);

            // Get posts for user 2 and check if ID of postCreated is in there
            var postsOfUser2 = await client.Posts.GetPostsByAuthor(user2.Id);
            var postsById = postsOfUser2.Where(x => x.Id == postCreated.Id).ToList();
            Assert.AreEqual(postsById.Count, 1);
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

        #region Utils

        private async Task<User> CreateRandomUser(WordPressClient client)
        {
            var random = new Random();
            var r = random.Next(0, 1000);
            var username = $"Testuser{r}";
            var email = $"testuser{r}@test.com";
            var password = $"testpassword{r}";
            return await client.Users.Create(new User(username, email, password));
        }

        #endregion Utils
    }
}