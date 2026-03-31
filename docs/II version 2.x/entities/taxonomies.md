# Taxonomies

Here is a list of methods and examples of working with Taxonomies

## Get All

```C#
// returns all taxonomies
var taxonomies = await client.Taxonomies.GetAllAsync();
```

## Get By ID

```C#
// returns taxonomy by ID
var taxonomy = await client.Taxonomies.GetByIDAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new TaxonomiesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var taxonomies = await client.Taxonomies.QueryAsync(queryBuilder);
```