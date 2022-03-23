# Media

Here is a list of methods and examples of working with Media

## GetAll()

```C#
// returns all media
var media = await client.Media.GetAll();
```

## GetByID

```C#
// returns media by ID
var media = await client.Media.GetByID(123);
```

## Query
Create parametrized request
```C#
// returns result of query
var queryBuilder = new MediaQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
var media = await client.Pages.Query(queryBuilder);
```

## Create new Media
### Create for .Net Standard 1.1+
.Net Standard 1.1-1.3 doesn`t support file manipulation (read or write). So only way to send file content is to create Stream with file content manually.
Applicable for .netcore 1.0 apps
```C#
// returns created media
// for create media item you must read them to Stream. Media items can be audio, video, image, pdf ot any othe type supported by wordpress
Stream s = File.OpenRead("pathToMedia/media.jpg");
if (await client.IsValidJWToken())
{
    var createdMedia = await client.Media.Create(s,"media.jpg");
}
```
### Create for .Net Standard 2.0+
.Net Standard 2.0 supports files manipulation. You can send media files by passing its full path to file.
Applicable for .netcore 2.0 apps
```C#
// returns created media
// for create media item you must read them to Stream. Media items can be audio, video, image, pdf ot any othe type supported by wordpress
if (await client.IsValidJWToken())
{
    var createdMedia = await client.Media.Create(@"C:\pathToFile\media.jpg","media.jpg");
}
```

## Update Media

```C#
// returns updated media
var media= client.Media.GetByID(123);
media.Title.Raw = "New Title";

if (await client.IsValidJWToken())
{
    var updatedMedia = await client.Media.Update(media);
}
```

## Delete Media

```C#
// returns result of deletion
if (await client.IsValidJWToken())
{
    var result = await client.Media.Delete(123);
}
```