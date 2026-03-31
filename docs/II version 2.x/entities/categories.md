# Categories

Here is a list of methods and examples of working with Categories

## Get All 

```C#
// returns all categories
var categories = await client.Categories.GetAllAsync();
```

## Get by ID

```C#
// returns category by ID
var category = await client.Categories.GetByIDAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new CategoriesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var categories = await client.Categories.QueryAsync(queryBuilder);
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
    var createdCategory = await client.Categories.CreateAsync(category);
}
```

## Update Category

```C#
// returns updated category
var category = client.Categories.GetByIDAsync(123);
category.Name = "New Name";
if (await client.IsValidJWTokenAsync())
{
    var updatedCategory = await client.Categories.UpdateAsync(category);
}
```

## Delete Category

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    var result = await client.Categories.DeleteAsync(123);
}
```