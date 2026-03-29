# Media

Here is a list of methods and examples of working with Media

## GetAll()

```C#
// returns all media
List<MediaItem> media = await client.Media.GetAllAsync();
```

## GetByIdAsync

```C#
// returns media by ID
MediaItem media = await client.Media.GetByIdAsync(123);
```

## Query
Create parametrized request
```C#
// returns result of query
MediaQueryBuilder queryBuilder = new MediaQueryBuilder();
queryBuilder.PerPage = 40;
queryBuilder.Page = 2;
queryBuilder.Before = DateTime.Now;
List<MediaItem> media = await client.Media.QueryAsync(queryBuilder);
```

## Create new Media
### Create from Stream

```C#
// returns created media
// for create media item you must read them to Stream. Media items can be audio, video, image, pdf ot any othe type supported by wordpress
Stream s = File.OpenRead("pathToMedia/media.jpg");
if (await client.IsValidJWTokenAsync())
{
    MediaItem createdMedia = await client.Media.CreateAsync(s,"media.jpg");
}
```
### Create from file path

```C#
// returns created media
// for create media item you must read them to Stream. Media items can be audio, video, image, pdf ot any othe type supported by wordpress
if (await client.IsValidJWTokenAsync())
{
    MediaItem createdMedia = await client.Media.CreateAsync(@"C:\pathToFile\media.jpg","media.jpg");
}
```

## Update Media

```C#
// returns updated media
MediaItem media = await client.Media.GetByIdAsync(123);
media.Title.Raw = "New Title";

if (await client.IsValidJWTokenAsync())
{
    MediaItem updatedMedia = await client.Media.UpdateAsync(media);
}
```

## Delete Media

```C#
// returns result of deletion
if (await client.IsValidJWTokenAsync())
{
    bool result = await client.Media.DeleteAsync(123);
}
```
