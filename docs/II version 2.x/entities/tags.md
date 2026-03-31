# Tags

Here is a list of methods and examples of working with Tags

## Get All

```C#
// returns all tags
var tags = await client.Tags.GetAllAsync();
```

## Get By ID

```C#
// returns tag by ID
var tag = await client.Tags.GetByIDAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new TagsQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var tags = await client.Tags.QueryAsync(queryBuilder);
```

## Create new Tag

```C#
// returns created tag
var tag = new Tag()
{
    Name = "Name",
    Description = "Tag"
};
if (await client.IsValidJWTokenAsync())
{
    var createdtag = await client.Tags.CreateAsync(tag);
}
```

## Update Tag

```C#
// returns updated tag
var tag = client.Tags.GetByIDAsync(123);
tag.Name = "New Name";
if (await client.IsValidJWTokenAsync())
{
    var updatedTag = await client.Tags.UpdateAsync(tag);
}
```

## Delete Tag

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    var result = await client.Tags.DeleteAsync(123);
}
```