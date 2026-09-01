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

## Audio metadata

For audio attachments, `MediaItem.MediaDetails` maps the optional metadata returned by
WordPress. Values depend on the uploaded file and the server's metadata extraction.

| Property | WordPress field |
| --- | --- |
| `DataFormat` | `dataformat` |
| `Channels` | `channels` |
| `SampleRate` | `sample_rate` |
| `Bitrate` | `bitrate` |
| `ChannelMode` | `channelmode` |
| `BitrateMode` | `bitrate_mode` |
| `Lossless` | `lossless` |
| `EncoderOptions` | `encoder_options` |
| `CompressionRatio` | `compression_ratio` |
| `FileFormat` | `fileformat` |
| `FileSize` | `filesize` |
| `MimeType` | `mime_type` |
| `Length` | `length` |
| `LengthFormatted` | `length_formatted` |
| `Genre`, `Year`, `Date`, `Text` | ID3 tag fields |
| `Title`, `Artist`, `Album` | ID3 tag fields |
| `CreatedTimestamp` | `created_timestamp` |

## Create new Media
### Create from Stream

```C#
// returns created media
// Read the media file into a stream before uploading it. Media items can be audio, video, images, PDFs, or any other type supported by WordPress.
Stream s = File.OpenRead("pathToMedia/media.jpg");
if (await client.IsValidJWTokenAsync())
{
    MediaItem createdMedia = await client.Media.CreateAsync(s,"media.jpg");
}
```

Filenames may contain Unicode characters, such as `"中文.webp"`. For Unicode filenames
(or names containing quotes or backslashes), uploads send an RFC 5987 `filename*` value
with a safe ASCII fallback so WordPress receives a valid `Content-Disposition` header.
Plain ASCII filenames use the standard `filename` parameter.

### Create from file path

```C#
// returns created media
// Media items can be audio, video, images, PDFs, or any other type supported by WordPress.
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
