# Users

Here is a list of methods and examples of working with Users

## Get All

```C#
// execute request users without credentials - returns only you
List<User> users = await client.Users.GetAllAsync();

// send credentials - list of all users
List<User> users = await client.Users.GetAllAsync(useAuth:true);
```

## Get By ID

```C#
// returns user by ID
User user = await client.Users.GetByIdAsync(123);
```

## Get Current User

```C#
// returns current user
User user = await client.Users.GetCurrentUserAsync();
```

## Query
Create parametrized request
```C#
// returns result of query
UsersQueryBuilder queryBuilder = new UsersQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<User> users = await client.Users.QueryAsync(queryBuilder);
```

## Query with Roles
To retreive users with roles, you'll need to set the `Context` property of the `UsersQueryBuilder` to `Context.Edit`.
```C#
UsersQueryBuilder queryBuilder = new()
{
    // required for roles to be loaded
    Context = Context.Edit
};

List<User> users = await _clientAuth.Users.QueryAsync(queryBuilder, true);
```

## Create new User

```C#
// returns created user
User user = new User("username","email","password")
{
    NickName= "nickname"
};
if (await client.IsValidJWTokenAsync())
{
    User createdUser = await client.Users.CreateAsync(user);
}
```

## Update User

```C#
// returns updated user
User user = await client.Users.GetByIdAsync(123);
user.Name = "New Name";
if (await client.IsValidJWTokenAsync())
{
    User updatedUser = await client.Users.UpdateAsync(user);
}
```

## Delete User

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    //second param - user to reassign all content
    bool result = await client.Users.DeleteAsync(123,321);
}
```

## Create Application Password

```C#
// Create an application password for the current user
ApplicationPassword password = await client.Users.CreateApplicationPasswordAsync("application-name");

// Create an application password for a specific user
ApplicationPassword password = await client.Users.CreateApplicationPasswordAsync("application-name", userId: "3");
```
