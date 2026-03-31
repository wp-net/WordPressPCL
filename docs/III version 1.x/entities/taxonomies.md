# Taxonomies

Here is a list of methods and examples of working with Taxonomies

## GetAll()

```C#
// returns all taxonomies
List<Taxonomy> taxonomies = await client.Taxonomies.GetAll();
```

## GetByID

```C#
// returns taxonomy by ID
Taxonomy taxonomy = await client.Taxonomies.GetByID(123);
```

## Query
Create parametrized request
```C#
// returns result of query
TaxonomiesQueryBuilder queryBuilder = new TaxonomiesQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<Taxonomy> taxonomies = await client.Taxonomies.Query(queryBuilder);
```