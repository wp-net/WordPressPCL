
# Custom JsonSerializationSettings

In some cases, it may be useful to change the default settings for the serialization / deserialization process of the Json.NET library
You can do this in the following way:
```c#
var client = new WordPressClient("https://site.com/wp-json/");
client.JsonSerializationSettings = new JsonSerializationSettings()
{
        DateFormatHandling=DateFormatHandling.IsoDateFormat,
        DateFormatString = "d MMMM YYYY"
};
// working with library
```
For detailed information on the available settings, see the Json.NET documentation https://www.newtonsoft.com/json/help/html/SerializationSettings.htm