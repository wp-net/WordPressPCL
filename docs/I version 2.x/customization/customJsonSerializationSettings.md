
# Custom JsonSerializerOptions

In some cases it may be useful to change the default serialization / deserialization behaviour.
You can do this by providing a custom `JsonSerializerOptions` instance:

```c#
using System.Text.Json;

var client = new WordPressClient("https://site.com/wp-json/");
client.JsonSerializerOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    // add any other options here
};
// working with library
```

For detailed information on the available options, see the [System.Text.Json documentation](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions).

> **Migration note (v3):** The `JsonSerializerSettings` property (Newtonsoft.Json) has been replaced by
> `JsonSerializerOptions` (System.Text.Json). The library no longer depends on Newtonsoft.Json.
