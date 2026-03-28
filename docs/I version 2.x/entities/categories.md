# Categories

Here is a list of methods and examples of working with Categories

## Get All 

```C#
// returns all categories
List<Category> categories = await client.Categories.GetAllAsync();
```

## Get by ID

```C#
// returns category by ID
Category category = await client.Categories.GetByIdAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
CategoriesQueryBuilder queryBuilder = new CategoriesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<Category> categories = await client.Categories.QueryAsync(queryBuilder);
```

## Create new Category

```C#
// returns created category
Category category = new Category()
{
    Name = "Title 1",
    Description = "Content"
};
if (await client.IsValidJWTokenAsync())
{
    Category createdCategory = await client.Categories.CreateAsync(category);
}
```

## Update Category

```C#
// returns updated category
Category category = await client.Categories.GetByIdAsync(123);
category.Name = "New Name";
if (await client.IsValidJWTokenAsync())
{
    Category updatedCategory = await client.Categories.UpdateAsync(category);
}
```

## Delete Category

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    bool result = await client.Categories.DeleteAsync(123);
}
```
