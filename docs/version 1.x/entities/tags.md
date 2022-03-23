# Tags

Here is a list of methods and examples of working with Tags

## GetAll()

```C#
// returns all tags
var tags = await client.Tags.GetAll();
```

## GetByID

```C#
// returns tag by ID
var tag = await client.Tags.GetByID(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new TagsQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var tags = await client.Tags.Query(queryBuilder);
```

## Create new Tag

```C#
// returns created tag
var tag = new Tag()
{
    Name = "Name",
    Description = "Tag"
};
if (await client.IsValidJWToken())
{
    var createdtag = await client.Tags.Create(tag);
}
```

## Update Tag

```C#
// returns updated tag
var tag = client.Tags.GetByID(123);
tag.Name = "New Name";
if (await client.IsValidJWToken())
{
    var updatedTag = await client.Tags.Update(tag);
}
```

## Delete Tag

```C#
// returns result of deletion
if (await client.IsValidJWToken())
{
    var result = await client.Tags.Delete(123);
}
```