# Taxonomies

Here is a list of methods and examples of working with Taxonomies

## Get All

```C#
// returns all taxonomies
List<Taxonomy> taxonomies = await client.Taxonomies.GetAllAsync();
```

## Get By ID

```C#
// returns taxonomy by ID
Taxonomy taxonomy = await client.Taxonomies.GetByIdAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
TaxonomiesQueryBuilder queryBuilder = new TaxonomiesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<Taxonomy> taxonomies = await client.Taxonomies.QueryAsync(queryBuilder);
```