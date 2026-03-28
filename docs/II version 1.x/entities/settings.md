# Settings

Here is a list of methods and examples of working with Settings

## GetSettings()

```C#
// returns current settings
Settings settings = await client.GetSettings();
```

## Update Settings
```C#
//update settings
Settings settings = await client.GetSettings();
settings.Description = "New Site Description";
Settings updatedSettings = await client.UpdateSettings(settings);
```