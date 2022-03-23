# Categories

Here is a list of methods and examples of working with Categories

## GetAll()

```C#
// returns all categories
var categories = await client.Categories.GetAll();
```

## GetByID

```C#
// returns category by ID
var category = await client.Categories.GetByID(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new CategoriesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var categories = await client.Categories.Query(queryBuilder);
```

## Create new Category

```C#
// returns created category
var category = new Category()
{
    Name = "Title 1",
    Description = "Content"
};
if (await client.IsValidJWToken())
{
    var createdCategory = await client.Categories.Create(category);
}
```

## Update Category

```C#
// returns updated category
var category = client.Categories.GetByID(123);
category.Name = "New Name";
if (await client.IsValidJWToken())
{
    var updatedCategory = await client.Categories.Update(category);
}
```

## Delete Category

```C#
// returns result of deletion
if (await client.IsValidJWToken())
{
    var result = await client.Categories.Delete(123);
}
```