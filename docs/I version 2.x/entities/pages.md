# Pages

Here is a list of methods and examples of working with Pages

## Get All

```C#
// returns all pages
var pages = await client.Pages.GetAllAsync();
```

## Get By ID

```C#
// returns page by ID
var page = await client.Pages.GetByIDAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new PagesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var pages = await client.Pages.QueryAsync(queryBuilder);
```

## Create new Page

```C#
// returns created page
var page = new Page()
{
    Title = new Title("Title 1"),
    Content = new Content("Content PageCreate")
};
if (await client.IsValidJWTokenAsync())
{
    var createdPage = await client.Pages.CreateAsync(page);
}
```

## Update Page

```C#
// returns updated page
var page= client.Pages.GetByIDAsync(123);
page.Content.Raw = "New Content";
if (await client.IsValidJWTokenAsync())
{
    var updatedPage = await client.Pages.UpdateAsync(page);
}
```

## Delete Page

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    var result = await client.Pages.DeleteAsync(123);
}
```