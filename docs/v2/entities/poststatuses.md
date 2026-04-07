# PostStatuses

Here is a list of methods and examples of working with PostStatuses

## Get All

```C#
// returns all poststatuses
var poststatuses = await client.PostStatuses.GetAllAsync();
```

## Get By ID

```C#
// returns poststatus by ID
var poststatus = await client.PostStatuses.GetByIDAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new PostStatusesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var poststatuses = await client.PostStatuses.QueryAsync(queryBuilder);
```