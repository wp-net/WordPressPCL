# PostTypes

Here is a list of methods and examples of working with PostTypes

## Get All

```C#
// returns all posttypes
var posttypes = await client.PostTypes.GetAllAsync();
```

## Get By ID

```C#
// returns posttype by ID
var posttype = await client.PostTypes.GetByIDAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new PostTypesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var posttypes = await client.PostTypes.QueryAsync(queryBuilder);
```