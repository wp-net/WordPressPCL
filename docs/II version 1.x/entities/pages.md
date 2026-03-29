# Pages

Here is a list of methods and examples of working with Pages

## GetAll()

```C#
// returns all pages
List<Page> pages = await client.Pages.GetAll();
```

## GetByID

```C#
// returns page by ID
Page page = await client.Pages.GetByID(123);
```

## Query
Create parametrized request
```C#
// returns result of query
PagesQueryBuilder queryBuilder = new PagesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<Page> pages = await client.Pages.Query(queryBuilder);
```

## Create new Page

```C#
// returns created page
Page page = new Page()
{
    Title = new Title("Title 1"),
    Content = new Content("Content PageCreate")
};
if (await client.IsValidJWToken())
{
    Page createdPage = await client.Pages.Create(page);
}
```

## Update Page

```C#
// returns updated page
Page page= client.Pages.GetByID(123);
page.Content.Raw = "New Content";
if (await client.IsValidJWToken())
{
    Page updatedPage = await client.Pages.Update(page);
}
```

## Delete Page

```C#
// returns result of deletion
if (await client.IsValidJWToken())
{
    bool result = await client.Pages.Delete(123);
}
```