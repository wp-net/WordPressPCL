# Users

Here is a list of methods and examples of working with Users

## Get All

```C#
// execute request users without credentials - returns only you
var users = await client.Users.GetAllAsync();

// send credentials - list of all users
var users = await client.Users.GetAllAsync(useAuth:true);
```

## Get By ID

```C#
// returns user by ID
var user = await client.Users.GetByIDAsync(123);
```

## Get Current User

```C#
// returns current user
var user = await client.Users.GetCurrentUserAsync();
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new UsersQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var users = await client.Users.QueryAsync(queryBuilder);
```

## Create new User

```C#
// returns created user
var user = new User("username","email","password")
{
    NickName= "nickname"
};
if (await client.IsValidJWTokenAsync())
{
    var user = await client.Users.CreateAsync(user);
}
```

## Update User

```C#
// returns updated user
var user = client.Users.GetByIDAsync(123);
user.Name = "New Name";
if (await client.IsValidJWTokenAsync())
{
    var updatedUser = await client.Users.UpdateAsync(user);
}
```

## Delete User

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    //second param - user to reassign all content
    var result = await client.Users.DeleteAsync(123,321);
}
```