# Tags

Here is a list of methods and examples of working with Tags

## Get All

```C#
// returns all tags
List<Tag> tags = await client.Tags.GetAllAsync();
```

## Get By ID

```C#
// returns tag by ID
Tag tag = await client.Tags.GetByIdAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
TagsQueryBuilder queryBuilder = new TagsQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<Tag> tags = await client.Tags.QueryAsync(queryBuilder);
```

## Create new Tag

```C#
// returns created tag
Tag tag = new Tag()
{
    Name = "Name",
    Description = "Tag"
};
if (await client.IsValidJWTokenAsync())
{
    Tag createdtag = await client.Tags.CreateAsync(tag);
}
```

## Update Tag

```C#
// returns updated tag
Tag tag = await client.Tags.GetByIdAsync(123);
tag.Name = "New Name";
if (await client.IsValidJWTokenAsync())
{
    Tag updatedTag = await client.Tags.UpdateAsync(tag);
}
```

## Delete Tag

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    bool result = await client.Tags.DeleteAsync(123);
}
```
