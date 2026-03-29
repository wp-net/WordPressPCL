# PostTypes

Here is a list of methods and examples of working with PostTypes

## Get All

```C#
// returns all posttypes
List<PostType> posttypes = await client.PostTypes.GetAllAsync();
```

## Get By ID

```C#
// returns posttype by ID
PostType posttype = await client.PostTypes.GetByIdAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
PostTypesQueryBuilder queryBuilder = new PostTypesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<PostType> posttypes = await client.PostTypes.QueryAsync(queryBuilder);
```