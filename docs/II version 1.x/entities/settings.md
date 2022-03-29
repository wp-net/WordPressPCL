# Settings

Here is a list of methods and examples of working with Settings

## GetSettings()

```C#
// returns current settings
var settings = await client.GetSettings();
```

## Update Settings
```C#
//update settings
var settings = await client.GetSettings();
settings.Description = "New Site Description";
var updatedSettings = await client.UpdateSettings(settings);
```