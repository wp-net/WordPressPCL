# PostStatuses

Here is a list of methods and examples of working with PostStatuses

## GetAll()

```C#
// returns all poststatuses
var poststatuses = await client.PostStatuses.GetAll();
```

## GetByID

```C#
// returns poststatus by ID
var poststatus = await client.PostStatuses.GetByID(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new PostStatusesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var poststatuses = await client.PostStatuses.Query(queryBuilder);
```