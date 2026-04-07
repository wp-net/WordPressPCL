# Settings

Here is a list of methods and examples of working with Settings

## Get Settings

```C#
// returns current settings
Settings settings = await client.Settings.GetSettingsAsync();
```

## Update Settings
```C#
//update settings
Settings settings = await client.Settings.GetSettingsAsync();
settings.Description = "New Site Description";
Settings updatedSettings = await client.Settings.UpdateSettingsAsync(settings);
```