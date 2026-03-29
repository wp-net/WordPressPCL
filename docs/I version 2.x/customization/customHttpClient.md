# Custom HttpClient and DI

You can inject your own instance of an `HttpClient` into the `WordPressClient`. This allows you to re-use an existing instance, set desired headers, and hand ownership of the HTTP transport to your application.

- `new WordPressClient(uri)` creates and owns its internal `HttpClient`.
- `new WordPressClient(httpClient)` reuses an externally managed `HttpClient` and does **not** dispose it.

```c#
HttpClient httpClient = new HttpClient
{
    BaseAddress = new Uri(ApiCredentials.WordPressUri)
};
httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0");
httpClient.DefaultRequestHeaders.Add("Referer", "https://github.com/wp-net/WordPressPCL");

WordPressClient client = new WordPressClient(httpClient);
List<Post> posts = await client.Posts.GetAllAsync();
```

For ASP.NET Core or worker services, prefer `IHttpClientFactory` integration:

```csharp
using Microsoft.Extensions.DependencyInjection;
using WordPressPCL;

builder.Services
    .AddWordPressClient(
        (services, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(builder.Configuration["WordPress:BaseUrl"]!);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
        },
        (services, client) =>
        {
            client.Auth.UseBasicAuth(
                builder.Configuration["WordPress:Username"]!,
                builder.Configuration["WordPress:ApplicationPassword"]!);
        });
```

The returned `IHttpClientBuilder` can still be customized with standard factory features such as `ConfigurePrimaryHttpMessageHandler`, policies, and additional handlers.
