# PostStatuses

Here is a list of methods and examples of working with PostStatuses

## Get All

```C#
// returns all poststatuses
List<PostStatus> poststatuses = await client.PostStatuses.GetAllAsync();
```

## Get By ID

```C#
// returns poststatus by ID
PostStatus poststatus = await client.PostStatuses.GetByIdAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
PostStatusesQueryBuilder queryBuilder = new PostStatusesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<PostStatus> poststatuses = await client.PostStatuses.QueryAsync(queryBuilder);
```