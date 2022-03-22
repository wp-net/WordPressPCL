# WordPressPCL
![Integration Tests](https://github.com/wp-net/WordPressPCL/workflows/Integration%20Tests/badge.svg?branch=master)

This is a portable library for consuming the WordPress REST-API in (almost) any C# application.
If you find bugs or have any suggestions, feel free to create an issue.

## License
WordPressPCL is published under the [MIT License](https://github.com/wp-net/WordPressPCL/blob/master/LICENSE)

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
WordPressPCL is built on top of the new [.NET Standard](https://github.com/dotnet/standard) targeting netstandard version 2.0 - therefore it should work on the following plaforms:
* .NET Framework 4.5 and newer
* .NET Core
* Universal Windows Platform (uap)
* Windows 8.0 and newer
* Windows Phone (WinRT, not Silverlight)
* Mono / Xamarin

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
var postbyid = await client.Posts.GetById(id);
var postsCount = await client.Posts.GetCountAsync();

// Comments
var comments = await client.Comments.GetAllAsync();
var commentbyid = await client.Comments.GetById(id);
var commentsbypost = await client.Comments.GetCommentsForPost(postid, true, false);

// Authentication
var client = new WordPressClient(ApiCredentials.WordPressUri);

//Either Bearer Auth using JWT tokens
client.Auth.UseBearerAuth(JWTPlugin.JWTAuthByEnriqueChavez);
await client.Auth.RequestJWTokenAsync("username", "password");
var isValidToken = await client.IsValidJWTokenAsync();
  
//Or Basic Auth using Application Passwords
client.Auth.UseBasicAuth("username", "password");

// now you can send requests that require authentication
var response = client.Posts.Delete(postId);
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

## Additional Features

- Authentication using [JSON Web Tokens (JWT)](https://jwt.io/)
- [HttpResponsePreProcessing](https://github.com/wp-net/WordPressPCL/wiki/HttpResponsePreProcessing): manipulate the API response before deserializing it

## Contribution Guidelines
We're very happy to get input from the community on this project! To keep the code clean we ask you to follow a few simple contribution guidelines.

First, create an issue describing what feature you want to add or what problem you're trying to solve, just to make sure no one is already working on that. That also gives us a chance to debate whether a feature is within the scope of this project.

Second, please try to stick to the official C# coding guidelines. https://msdn.microsoft.com/en-us/library/ms229002(v=vs.110).aspx

Also, make sure to write some tests covering your new or modified code.

In order to run the tests on local machine please refer to the **install.md** file in the dev directory of the repository. Docker along with docker-compose cli will be required to run the tests.
