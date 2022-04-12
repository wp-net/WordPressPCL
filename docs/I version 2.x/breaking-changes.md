# Breaking Changes in Version 2.x

- Separate sub client for Authentication accessed via `client.Auth`
- Separate sub client for Settings accessed via `client.Settings`
- Default MIME type for media is **application/octet-stream** instead of text/plain
- Async methods have an Async suffix as per naming C# naming guidelines e.g. `client.Posts.GetAllAsync()`
- MediaQueryBuilder by default will return all types of media instead of just images.
- Model properties that returned arrays are changed to List type e.g. `Tags` in `Post` class is now of type `List<int>` instead of `int[]` type