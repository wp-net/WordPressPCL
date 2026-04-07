# Pages

Here is a list of methods and examples of working with Pages

## Get All

```C#
// returns all pages
List<Page> pages = await client.Pages.GetAllAsync();
```

## Get By ID

```C#
// returns page by ID
Page page = await client.Pages.GetByIdAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
PagesQueryBuilder queryBuilder = new PagesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<Page> pages = await client.Pages.QueryAsync(queryBuilder);
```

## Create new Page

```C#
// returns created page
Page page = new Page()
{
    Title = new Title("Title 1"),
    Content = new Content("Content PageCreate")
};
if (await client.IsValidJWTokenAsync())
{
    Page createdPage = await client.Pages.CreateAsync(page);
}
```

## Update Page

```C#
// returns updated page
Page page = await client.Pages.GetByIdAsync(123);
page.Content.Raw = "New Content";
if (await client.IsValidJWTokenAsync())
{
    Page updatedPage = await client.Pages.UpdateAsync(page);
}
```

## Delete Page

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    bool result = await client.Pages.DeleteAsync(123);
}
```
