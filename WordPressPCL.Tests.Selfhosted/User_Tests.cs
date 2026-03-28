using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;
using WordPressPCL.Tests.Selfhosted.Utility;
using WordPressPCL.Utility;
using Guid = System.Guid;

namespace WordPressPCL.Tests.Selfhosted;

[TestClass]
public class User_Tests
{
    private static WordPressClient _client = null!;
    private static WordPressClient _clientAuth = null!;

    [ClassInitialize]
    public static async Task Init(TestContext testContext)
    {
        _client = ClientHelper.GetWordPressClient();
        _clientAuth = await ClientHelper.GetAuthenticatedWordPressClient(testContext);
    }

    [TestMethod]
    public async Task Users_Create()
    {
        string username = Guid.NewGuid().ToString();
        string nickname = $"Nickname{username}";
        string email = $"{username}@test.com";
        string firstname = $"Firstname{username}";
        string lastname = $"Lastname{username}";
        string password = $"testpassword{username}";
        string name = $"{firstname} {lastname}";

        User user = await _clientAuth.Users.CreateAsync(new User(username, email, password)
        {
            NickName = nickname,
            Name = name,
            FirstName = firstname,
            LastName = lastname
        }, TestContext.CancellationToken);
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
        List<User> users = await _client.Users.GetAllAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(users);
        Assert.IsGreaterThanOrEqualTo(1, users.Count);
        User user = await _client.Users.GetByIdAsync(users.First().Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(user);
        Assert.AreEqual(user.Id, users.First().Id);
    }

    [TestMethod]
    public async Task Users_Get()
    {
        List<User> users = await _client.Users.GetAsync(cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(users);
        Assert.IsGreaterThanOrEqualTo(1, users.Count);
        User user = await _client.Users.GetByIdAsync(users.First().Id, cancellationToken: TestContext.CancellationToken);
        Assert.IsNotNull(user);
        Assert.AreEqual(user.Id, users.First().Id);
    }

    [TestMethod]
    public async Task Users_Update()
    {
        User user = await CreateRandomUser();
        Assert.IsNotNull(user);

        string name = $"TestuserUpdate";
        user.Name = name;
        user.NickName = name;
        user.FirstName = name;
        user.LastName = name;

        User updatedUser = await _clientAuth.Users.UpdateAsync(user, TestContext.CancellationToken);
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(updatedUser.Name, name);
        Assert.AreEqual(updatedUser.NickName, name);
        Assert.AreEqual(updatedUser.FirstName, name);
        Assert.AreEqual(updatedUser.LastName, name);
    }

    [TestMethod]
    public async Task Users_Delete()
    {
        User user = await CreateRandomUser();
        Assert.IsNotNull(user);
        User me = await _clientAuth.Users.GetCurrentUserAsync(TestContext.CancellationToken);
        bool response = await _clientAuth.Users.Delete(user.Id, me.Id, TestContext.CancellationToken);
        Assert.IsTrue(response);
    }

    [TestMethod]
    public async Task Users_Delete_And_Reassign_Posts()
    {
        // Create new user
        User user1 = await CreateRandomUser();
        Assert.IsNotNull(user1);
        User user2 = await CreateRandomUser();
        Assert.IsNotNull(user2);

        // Create post for user
        Post post = new()
        {
            Title = new Title("Title 1"),
            Content = new Content("Content PostCreate"),
            Author = user1.Id
        };
        Post postCreated = await _clientAuth.Posts.CreateAsync(post, TestContext.CancellationToken);
        Assert.IsNotNull(post);
        Assert.AreEqual(postCreated.Author, user1.Id);

        // Delete User1 and reassign posts to user2
        bool response = await _clientAuth.Users.Delete(user1.Id, user2.Id, TestContext.CancellationToken);
        Assert.IsTrue(response);

        // Get posts for user 2 and check if ID of postCreated is in there
        List<Post> postsOfUser2 = await _clientAuth.Posts.GetPostsByAuthorAsync(user2.Id, cancellationToken: TestContext.CancellationToken);
        List<Post> postsById = postsOfUser2.Where(x => x.Id == postCreated.Id).ToList();
        Assert.HasCount(1, postsById);
    }

    [TestMethod]
    public async Task Users_Query()
    {
        UsersQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 15,
            OrderBy = UsersOrderBy.RegisteredDate,
            Order = Order.DESC,
            Context = Context.Edit
        };
        Assert.AreEqual("?page=1&per_page=15&orderby=registered_date&order=desc&context=edit", queryBuilder.BuildQuery());

        List<User> queryresult = await _clientAuth.Users.QueryAsync(queryBuilder, true, TestContext.CancellationToken);
        Assert.IsNotNull(queryresult);
        Assert.AreNotEqual(0, queryresult.Count);
    }

    [TestMethod]
    public async Task Users_GetRoles()
    {
        UsersQueryBuilder queryBuilder = new()
        {
            // required for roles to be loaded
            Context = Context.Edit
        };

        List<User> users = await _clientAuth.Users.QueryAsync(queryBuilder, true, TestContext.CancellationToken);
        Assert.IsNotNull(users);
        Assert.IsGreaterThanOrEqualTo(1, users.Count);

        foreach (User user in users)
        {
            Assert.IsNotNull(user.Roles);
            Assert.IsGreaterThanOrEqualTo(1, user.Roles.Count);
        }
    }

    [TestMethod]
    public async Task Users_GetPaged_Returns_Metadata()
    {
        PagedResult<User> paged = await _clientAuth.Users.GetPagedAsync(page: 1, perPage: 5, useAuth: true, TestContext.CancellationToken);

        Assert.IsNotNull(paged);
        Assert.IsNotNull(paged.Items);
        Assert.IsLessThanOrEqualTo(5, paged.Items.Count);
        Assert.IsGreaterThan(0, paged.TotalCount, "TotalCount should be populated from X-WP-Total");
        Assert.IsGreaterThanOrEqualTo(1, paged.TotalPages, "TotalPages should be populated from X-WP-TotalPages");
    }

    [TestMethod]
    public async Task Users_QueryPaged_Returns_Metadata()
    {
        UsersQueryBuilder queryBuilder = new()
        {
            Page = 1,
            PerPage = 3,
        };
        PagedResult<User> paged = await _clientAuth.Users.QueryPagedAsync(queryBuilder, useAuth: true, TestContext.CancellationToken);

        Assert.IsNotNull(paged);
        Assert.IsNotNull(paged.Items);
        Assert.IsLessThanOrEqualTo(3, paged.Items.Count);
        Assert.IsGreaterThan(0, paged.TotalCount, "TotalCount should be populated from X-WP-Total");
        Assert.IsGreaterThanOrEqualTo(1, paged.TotalPages, "TotalPages should be populated from X-WP-TotalPages");
    }

    #region Utils

    private static Task<User> CreateRandomUser()
    {
        string username = Guid.NewGuid().ToString();
        string email = $"{username}@test.com";
        string password = $"testpassword{username}";
        return _clientAuth.Users.CreateAsync(new User(username, email, password));
    }

    public TestContext TestContext { get; set; }

    #endregion Utils
}
