Here is a list of methods and examples of working with Comments

## Get All

```C#
// returns all comments
List<Comment> comments = await client.Comments.GetAllAsync();
```

## Get By ID

```C#
// returns comment by ID
Comment comment = await client.Comments.GetByIdAsync(123);
```

## Get Comments by Post ID

```C#
// returns comments from post
List<Comment> comments = await client.Comments.GetCommentsForPostAsync(123)
```

## Query
Create parametrized request
```C#
// returns result of query
CommentsQueryBuilder queryBuilder = new CommentsQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<Comment> comments = await client.Comments.QueryAsync(queryBuilder);
```

## Get threaded comments
If your blog supports threaded comments (comments with direct answers) you can order and get the right depth for them with this handy extension method:

```c#
List<Comment> comments = await client.Comments.GetCommentsForPostAsync(123)
List<CommentThreaded>? commentsThreaded = comments.ToThreaded();
```

## Create new Comment

```C#
// returns created comment
Comment comment = new Comment()
{
    Content = new Content("Comment"),
    PostId = 123,
    AuthorId = 1,
    AuthorEmail = "test@test.com"
};
if (await client.IsValidJWTokenAsync())
{
    Comment createdComment = await client.Comments.CreateAsync(comment);
}
```

## Update Comment

```C#
// returns updated comment
Comment comment = await client.Comments.GetByIdAsync(123);
comment.Content.Raw = "New Content";
if (await client.IsValidJWTokenAsync())
{
    Comment updatedComment = await client.Comments.UpdateAsync(comment);
}
```

## Delete Comment

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    bool result = await client.Comments.DeleteAsync(123);
}
```
