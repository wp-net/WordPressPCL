# Users

Here is a list of methods and examples of working with Users

## GetAll()

```C#
// execute request users without credentials - returns only you
var users = await client.Users.GetAll();

// send credentials - list of all users
var users = await client.Users.GetAll(useAuth:true);
```

## GetByID

```C#
// returns user by ID
var user = await client.Users.GetByID(123);
```

## GetCurrentUser

```C#
// returns current user
var user = await client.Users.GetCurrentUser();
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new UsersQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var users = await client.Users.Query(queryBuilder);
```

## Create new User

```C#
// returns created user
var user = new User("username","email","password")
{
    NickName= "nickname"
};
if (await client.IsValidJWToken())
{
    var user = await client.Users.Create(user);
}
```

## Update User

```C#
// returns updated user
var user = client.Users.GetByID(123);
user.Name = "New Name";
if (await client.IsValidJWToken())
{
    var updatedUser = await client.Users.Update(user);
}
```

## Delete User

```C#
// returns result of deletion
if (await client.IsValidJWToken())
{
    //second param - user to reassign all content
    var result = await client.Users.Delete(123,321);
}
```