Here is a list of methods and examples of working with Comments

## Get All

```C#
// returns all comments
var comments = await client.Comments.GetAllAsync();
```

## Get By ID

```C#
// returns comment by ID
var comment = await client.Comments.GetByIDAsync(123);
```

## Get Comments by Post ID

```C#
// returns comments from post
var comments = await client.Comments.GetCommentsForPostAsync(123)
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new CommentsQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var comments = await client.Comments.Query(queryBuilder);
```

## Get threaded comments
If your blog supports threaded comments (comments with direct answers) you can order and get the right depth for them with this handy extension method:

```c#
var comments = await client.Comments.GetCommentsForPostAsync(123)
var commentsThreaded = comments.ToThreaded();
```

## Create new Comment

```C#
// returns created comment
var comment = new Comment()
{
    Content = new Content("Comment"),
    PostId = 123,
    AuthorId = 1,
    AuthorEmail = "test@test.com"
};
if (await client.IsValidJWTokenAsync())
{
    var createdComment = await client.Comments.CreateAsync(comment);
}
```

## Update Comment

```C#
// returns updated comment
var comment= client.Comments.GetByIDAsync(123);
comment.Content.Raw = "New Content";
if (await client.IsValidJWTokenAsync())
{
    var updatedComment = await client.Comments.UpdateAsync(comment);
}
```

## Delete Comment

```C#
// returns result of deletion
if (await client.IsValidJWToken())
{
    var result = await client.Comments.DeleteAsync(123);
}
```