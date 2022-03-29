# Pages

Here is a list of methods and examples of working with Pages

## GetAll()

```C#
// returns all pages
var pages = await client.Pages.GetAll();
```

## GetByID

```C#
// returns page by ID
var page = await client.Pages.GetByID(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new PagesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var pages = await client.Pages.Query(queryBuilder);
```

## Create new Page

```C#
// returns created page
var page = new Page()
{
    Title = new Title("Title 1"),
    Content = new Content("Content PageCreate")
};
if (await client.IsValidJWToken())
{
    var createdPage = await client.Pages.Create(page);
}
```

## Update Page

```C#
// returns updated page
var page= client.Pages.GetByID(123);
page.Content.Raw = "New Content";
if (await client.IsValidJWToken())
{
    var updatedPage = await client.Pages.Update(page);
}
```

## Delete Page

```C#
// returns result of deletion
if (await client.IsValidJWToken())
{
    var result = await client.Pages.Delete(123);
}
```