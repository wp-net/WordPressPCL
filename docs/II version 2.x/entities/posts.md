# Posts

Here is a list of methods and examples of working with Posts

## Get All

```C#
// returns all posts
var posts = await client.Posts.GetAllAsync();```

## Get By ID

```C#
// returns post by ID
var post = await client.Posts.GetByIDAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new PostsQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Categories = new int[]{1,2,3};
var posts = await client.Posts.QueryAsync(queryBuilder);
```

## Create new Post

```C#
// returns created post
var post = new Post()
{
    Title = new Title("Title 1"),
    Content = new Content("Content PostCreate")
};
if (await client.IsValidJWTokenAsync())
{
    var createdPost = await client.Posts.CreateAsync(post);
}
```

## Update Post

```C#
// returns updated post
var post = client.Posts.GetByIDAsync(123);
post.Content.Raw = "New Content";
if (await client.IsValidJWTokenAsync())
{
    var updatedPost = await client.Posts.UpdateAsync(post);
}
```

## Update Custom Fields

```C#
var post = new Post
{
    Id = 123,
    Meta = new Dictionary<string, string>
    {
        ["my-custom-key"] = "some value"
    },
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

## Delete Post

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    var result = await client.Posts.DeleteAsync(123);
}
```

## Get Post Revisions

```C#
// returns revisions of post
var revisions = await client.Posts.RevisionsAsync(123);
```