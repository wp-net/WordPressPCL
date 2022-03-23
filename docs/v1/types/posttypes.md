# PostTypes

Here is a list of methods and examples of working with PostTypes

## GetAll()

```C#
// returns all posttypes
var posttypes = await client.PostTypes.GetAll();
```

## GetByID

```C#
// returns posttype by ID
var posttype = await client.PostTypes.GetByID(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new PostTypesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var posttypes = await client.PostTypes.Query(queryBuilder);
```