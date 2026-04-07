# WordPressPCL
![Integration Tests](https://github.com/wp-net/WordPressPCL/workflows/Integration%20Tests/badge.svg?branch=main) [![NuGet](https://img.shields.io/nuget/vpre/WordPressPCL)](https://www.nuget.org/packages/WordPressPCL/)

This is a .NET 10 library for consuming the WordPress REST API in C# applications.
If you find bugs or have any suggestions, feel free to create an issue.

## Documentation
https://wp-net.github.io/WordPressPCL/
# Quickstart

## WordPress Requirements
Since WordPress 4.7 the REST API has been integrated into the core so there's no need for any plugins to get basic functionality.

* [WordPress 4.7 or newer](https://wordpress.org/)

If you want to access protected endpoints, there are two authentication options:
* Authentication using JSON Web Tokens (JWT) (plugin required)
* Basic Authentication using Application Passwords

Supported JWT Authentication Plugins (install either of the following):
* [JWT Authentication for WP REST API](https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/)
* [JWT Auth By Useful Team](https://wordpress.org/plugins/jwt-auth/)

To use Application Passwords for authentication read through:

https://make.wordpress.org/core/2020/11/05/application-passwords-integration-guide/

## Including WordPressPCL
The WordPressPCL API Wrapper is avaiable through [NuGet](https://www.nuget.org/packages/WordPressPCL/):

```
> Install-Package WordPressPCL
```

## Release Process

WordPressPCL uses GitHub Releases as the source of truth for publishing new package versions.

1. Update [CHANGELOG.md](CHANGELOG.md) with the release highlights.
2. Create a GitHub release with a `vX.Y.Z` tag (or `vX.Y.Z-suffix` for pre-releases) and GitHub-generated release notes.
3. Publishing that release triggers the NuGet workflow, which builds and publishes the same version to NuGet.

## Runtime Requirement
WordPressPCL 3.0 targets .NET 10 only. Upgrading from 2.x is a breaking change and requires applications and test environments to move to the .NET 10 SDK/runtime before restoring, building, or running tests.

## Versioned Reference Docs
- [Version 3.x reference docs](docs/v3/index.md)
- [Version 2.x reference docs](docs/v2/index.md)
- [Version 1.x reference docs](docs/v1/index.md)

## Supported Platforms
WordPressPCL 3.0 targets .NET 10 and is intended for applications running on the current .NET platform:


| .NET implementation |	Version support |
|---------------------|------------------|
| .NET | 10.0 |

## Quickstart: Using the API Wrapper

```c#
// Client construction

//pass the Wordpress REST API base address as string
var client = new WordPressClient("https://demo.wp-api.org/wp-json/");

//or pass the base address as strongly typed Uri
const wpBaseAddress = new Uri("https://demo.wp-api.org/wp-json/");
var client = new WordPressClient(wpBaseAddress);

//or to reuse an HttpClient pass the HttpClient with base address set to api's base address
httpClient.BaseAddress = new Uri("https://demo.wp-api.org/wp-json/")
var client = new WordPressClient(httpClient);

// direct-construction ownership:
// - WordPressClient(Uri) creates and owns its internal HttpClient
// - WordPressClient(HttpClient) reuses an external HttpClient and never disposes it

// Posts
var posts = await client.Posts.GetAllAsync();
var postbyid = await client.Posts.GetByIdAsync(id);
var postsCount = await client.Posts.GetCountAsync();

// Comments
var comments = await client.Comments.GetAllAsync();
var commentbyid = await client.Comments.GetByIdAsync(id);
var commentsbypost = await client.Comments.GetCommentsForPostAsync(postid, true, false);

// Plugins
var plugins = await client.Plugins.GetAllAsync(useAuth:true);
var installedplugin = await client.Plugins.InstallAsync("akismet");
var activateplugin = await client.Plugins.ActivateAsync(installedplugin);
var deactivateplugin = await client.Plugins.DeactivateAsync(installedplugin);
var deleteplugin = await client.Plugins.DeleteAsync(installedplugin);

// Authentication
var client = new WordPressClient(ApiCredentials.WordPressUri);

//Either Bearer Auth using JWT tokens
client.Auth.UseBearerAuth(JWTPlugin.JWTAuthByEnriqueChavez);
await client.Auth.RequestJWTokenAsync("username", "password");
var isValidToken = await client.IsValidJWTokenAsync();
  
//Or Basic Auth using Application Passwords
client.Auth.UseBasicAuth("username", "password");

// now you can send requests that require authentication
var response = client.Posts.DeleteAsync(postId);
```

## Dependency injection and IHttpClientFactory

For ASP.NET Core and worker services, prefer registering `WordPressClient` through DI so that `HttpClient` pooling is managed by `IHttpClientFactory`.

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

You can then request `WordPressClient` anywhere DI is available:

```csharp
public sealed class PublishWorker(WordPressClient client)
{
    public Task<IReadOnlyList<Post>> GetPostsAsync()
        => client.Posts.GetAllAsync();
}
```

## Supported REST Methods

|                    | Create  | Read    | Update  | Delete  |
|--------------------|---------|---------|---------|---------|
| **Posts**          | yes     | yes     | yes     | yes     |
| **Pages**          | yes     | yes     | yes     | yes     |
| **Comments**       | yes     | yes     | yes     | yes     |
| **Categories**     | yes     | yes     | yes     | yes     |
| **Tags**           | yes     | yes     | yes     | yes     |
| **Users**          | yes     | yes     | yes     | yes     |
| **Media**          | yes     | yes     | yes     | yes     |
| **Post Revisions** | ---     | yes     | ---     | yes     |
| **Taxonomies**     | ---     | yes     | ---     | ---     |
| **Post Types**     | ---     | yes     | ---     | ---     |
| **Post Statuses**  | ---     | yes     | ---     | ---     |
| **Settings**       | ---     | yes     | yes     | ---     |
| **Plugins**        | yes     | yes     | yes     | yes     |
| **Themes**         | ---     | yes     | ---     | ---     |

Notes:

- Post revisions are available through `client.Posts.Revisions(postId)`.
- WordPressPCL does not currently provide a dedicated wrapper for page revisions or autosaves.

## Endpoint Coverage and Gaps

WordPressPCL provides dedicated clients for the standard `wp/v2` endpoints:

- Posts, Pages, Comments, Categories, Tags, Users and Media
- Taxonomies, Post Types, Post Statuses and Settings
- Plugins and Themes
- Search (`client.Search`)
- Block types (`client.BlockTypes`), reusable blocks (`client.Blocks`), templates (`client.Templates`), template parts (`client.TemplateParts`) and global styles (`client.GlobalStyles`)
- Navigation (`client.Navigation`), sidebars (`client.Sidebars`), widgets (`client.Widgets`) and widget types (`client.WidgetTypes`)
- URL details (`client.UrlDetails`)
- Post revisions via `client.Posts.Revisions(postId)` and page revisions via `client.Pages.Revisions(pageId)`
- Post autosaves via `client.Posts.Autosaves(postId)` and page autosaves via `client.Pages.Autosaves(pageId)`

The following endpoints do not yet have first-class wrappers but can be called via `CustomRequest`:

- `wp/v2/block-renderer` (server-side block rendering)
- `wp/v2/navigation-fallback`
- font families, font faces and pattern directory endpoints

You can also work with plugin namespaces and site-specific custom endpoints using `CustomRequest`.

```csharp
// Discover the namespaces and routes exposed by a site
dynamic apiIndex = await client.CustomRequest.GetAsync<dynamic>("", ignoreDefaultPath: true);

// Call a standard wp/v2 endpoint that does not have a dedicated client
dynamic rendered = await client.CustomRequest.PostAsync<dynamic>("block-renderer/core%2Fparagraph", body, ignoreDefaultPath: false);

// Call a plugin or custom namespace route
dynamic customEndpoint = await client.CustomRequest.GetAsync<dynamic>("wc/v3/products", useAuth: true);
```

For a fuller endpoint-by-endpoint summary, see [docs/v3/endpoint-coverage.md](docs/v3/endpoint-coverage.md).


## Additional Features

- Authentication using [JSON Web Tokens (JWT)](https://jwt.io/)
- [HttpResponsePreProcessing](https://github.com/wp-net/WordPressPCL/wiki/HttpResponsePreProcessing): manipulate the API response before deserializing it

## Contribution Guidelines
We're very happy to get input from the community on this project! See [CONTRIBUTING.md](CONTRIBUTING.md) for full details on setting up a development environment, running tests, and submitting pull requests.

In brief:
- Open an issue before starting work so we can discuss scope and avoid duplicate effort.
- Follow the [Microsoft C# coding guidelines](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions).
- Add or update tests to cover your change.
- In order to run the tests locally, refer to the [`dev/install.md`](dev/install.md) file and use Docker Compose.
