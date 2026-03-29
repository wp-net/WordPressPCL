# Posts

Here is a list of methods and examples of working with Posts

## GetAll()

```C#
// returns all posts
List<Post> posts = await client.Posts.GetAll();
```

## GetByID

```C#
// returns post by ID
Post post = await client.Posts.GetByID(123);
```

## Query
Create parametrized request
```C#
// returns result of query
PostsQueryBuilder queryBuilder = new PostsQueryBuilder();
queryBuilder.PerPage=40;
queryBuilder.Page=2;
queryBuilder.Categories= new int[]{1,2,3};
List<Post> posts = await client.Posts.Query(queryBuilder);
```

## Create new Post

```C#
// returns created post
Post post = new Post()
{
    Title = new Title("Title 1"),
    Content = new Content("Content PostCreate")
};
if (await client.IsValidJWToken())
{
    Post createdPost = await client.Posts.Create(post);
}
```

## Update Post

```C#
// returns updated post
Post post = client.Posts.GetByID(123);
post.Content.Raw = "New Content";
if (await client.IsValidJWToken())
{
    Post updatedPost = await client.Posts.Update(post);
}
```

## Delete Post

```C#
// returns result of deletion
if (await client.IsValidJWToken())
{
    bool result = await client.Posts.Delete(123);
}
```

## Get Post Revisions

```C#
// returns revisions of post
PostRevisions revisions = client.Posts.Revisions(123);
```