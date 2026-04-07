# Posts

Here is a list of methods and examples of working with Posts

## Get All

```csharp
// returns all posts
List<Post> posts = await client.Posts.GetAllAsync();```

## Get By ID

```csharp
// returns post by ID
Post post = await client.Posts.GetByIdAsync(123);
```

## Query
Create parametrized request
```csharp
// returns result of query
PostsQueryBuilder queryBuilder = new PostsQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Categories = new int[]{1,2,3};
List<Post> posts = await client.Posts.QueryAsync(queryBuilder);
```

## Create new Post

```csharp
// returns created post
Post post = new Post()
{
    Title = new Title("Title 1"),
    Content = new Content("Content PostCreate")
};
if (await client.IsValidJWTokenAsync())
{
    Post createdPost = await client.Posts.CreateAsync(post);
}
```

## Update Post

```csharp
// returns updated post
Post post = await client.Posts.GetByIdAsync(123);
post.Content.Raw = "New Content";
if (await client.IsValidJWTokenAsync())
{
    Post updatedPost = await client.Posts.UpdateAsync(post);
}
```

## Update Custom Fields

```csharp
Post post = new Post
{
    Id = 123,
    Meta = JsonSerializer.SerializeToElement(new Dictionary<string, object?>
    {
        ["my-custom-key"] = "some value"
    }),
};

await client.Posts.UpdateAsync(post);
```

Please note that all meta keys need to be registered using [`register_post_meta()`](https://developer.wordpress.org/reference/functions/register_post_meta/) before you can use them, e.g. by using the following PHP snippet:

```php
register_post_meta('post', 'my-custom-key', [
	'type' => 'string',
	'single' => true,
	'show_in_rest' => true,
]);
```

For more information on reading, writing, and working with custom fields see the [Custom Fields](customFields.md) documentation.

## Delete Post

```csharp
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    bool result = await client.Posts.DeleteAsync(123);
}
```

## Get Post Revisions

```csharp
// returns revisions of post
PostRevisions revisions = client.Posts.Revisions(123);
```
