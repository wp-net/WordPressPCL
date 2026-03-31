# Media

Here is a list of methods and examples of working with Media

## GetAll()

```C#
// returns all media
var media = await client.Media.GetAllAsync();
```

## GetByID

```C#
// returns media by ID
var media = await client.Media.GetByIDAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new MediaQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var media = await client.Pages.QueryAsync(queryBuilder);
```

## Create new Media
### Create from Stream

```C#
// returns created media
// for create media item you must read them to Stream. Media items can be audio, video, image, pdf ot any othe type supported by wordpress
Stream s = File.OpenRead("pathToMedia/media.jpg");
if (await client.IsValidJWTokenAsync())
{
    var createdMedia = await client.Media.CreateAsync(s,"media.jpg");
}
```
### Create from file path

```C#
// returns created media
// for create media item you must read them to Stream. Media items can be audio, video, image, pdf ot any othe type supported by wordpress
if (await client.IsValidJWToken())
{
    var createdMedia = await client.Media.CreateAsync(@"C:\pathToFile\media.jpg","media.jpg");
}
```

## Update Media

```C#
// returns updated media
var media= client.Media.GetByID(123);
media.Title.Raw = "New Title";

if (await client.IsValidJWTokenAsync())
{
    var updatedMedia = await client.Media.UpdateAsync(media);
}
```

## Delete Media

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    var result = await client.Media.DeleteAsync(123);
}
```