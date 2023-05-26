# WordPressPCL
![Integration Tests](https://github.com/wp-net/WordPressPCL/workflows/Integration%20Tests/badge.svg?branch=master) [![NuGet](https://img.shields.io/nuget/vpre/WordPressPCL)](https://www.nuget.org/packages/WordPressPCL/)

This is a portable library for consuming the WordPress REST-API in (almost) any C# application.
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

## Supported Plattforms
WordPressPCL is built on top of the [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0) targeting netstandard version 2.0 - therefore it should work on the following plaforms:


| .NET implementation |	Version support |
|---------------------|------------------|
|.NET and .NET Core |	2.0, 2.1, 2.2, 3.0, 3.1, 5.0, 6.0 |
|.NET Framework | 4.6.1 2, 4.6.2, 4.7, 4.7.1, 4.7.2, 4.8 |
| Mono | 5.4, 6.4 |
| Xamarin.iOS | 10.14, 12.16 |
| Xamarin.Mac | 3.8, 5.16 |
| Xamarin.Android | 8.0, 10.0 |
| Universal Windows Platform | 10.0.16299, TBD |
| Unity | 2018.1 |

## Quickstart: Using the API Wrapper

```c#
// Client construction

//pass the Wordpress REST API base address as string
var client = new WordPressClient("http://demo.wp-api.org/wp-json/");

//or pass the base address as strongly typed Uri
const wpBaseAddress = new Uri("http://demo.wp-api.org/wp-json/");
var client = new WordpressClient(wpBaseAddress);

//or to reuse an HttpClient pass the HttpClient with base address set to api's base address
httpClient.BaseAddress = new Uri("http://demo.wp-api.org/wp-json/")
var client = new WordpressClient(httpClient);

// Posts
var posts = await client.Posts.GetAllAsync();
var postbyid = await client.Posts.GetByIdAsync(id);
var postsCount = await client.Posts.GetCountAsync();

// Comments
var comments = await client.Comments.GetAllAsync();
var commentbyid = await client.Comments.GetByIdAsync(id);
var commentsbypost = await client.Comments.GetCommentsForPostAsync(postid, true, false);

// Plugins
var plugins = await client.Plugins.GetAllAsync();
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


## Additional Features

- Authentication using [JSON Web Tokens (JWT)](https://jwt.io/)
- [HttpResponsePreProcessing](https://github.com/wp-net/WordPressPCL/wiki/HttpResponsePreProcessing): manipulate the API response before deserializing it

## Contribution Guidelines
We're very happy to get input from the community on this project! To keep the code clean we ask you to follow a few simple contribution guidelines.

First, create an issue describing what feature you want to add or what problem you're trying to solve, just to make sure no one is already working on that. That also gives us a chance to debate whether a feature is within the scope of this project.

Second, please try to stick to the official C# coding guidelines. https://msdn.microsoft.com/en-us/library/ms229002(v=vs.110).aspx

Also, make sure to write some tests covering your new or modified code.

In order to run the tests on local machine please refer to the **install.md** file in the dev directory of the repository. Docker along with docker-compose cli will be required to run the tests.
