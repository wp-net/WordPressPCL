# WordPressPCL
This is a portable library for consuimg the WordPress REST-API in (almost) any C# application.
The project is still very much in *beta* so use at your own risk! 
If you find bugs or have any suggestions, feel free to create an issue.

## License
WordPressPCL is published under the [MIT License](https://github.com/wp-net/WordPressPCL/blob/master/LICENSE)

# Quickstart

## WordPress Requirements
Since WordPress 4.7 the REST API has been integrated into the core so there's no need for any plugins to get basic functionality. If you want to access protected endpoints, this library supports authentication through JSON Web Tokens (JWT) (plugin required).

* [WordPress 4.7 or newer](https://wordpress.org/)
* [JWT Authentication for WP REST API](https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/)

## Including WordPressPCL
The WordPressPCL API Wrapper is avaiable through nuget:

```
> Install-Package WordPressPCL
```

## Supported Plattforms
WordPressPCL is built on top of the new [.NET Platform Standard](https://github.com/dotnet/corefx/blob/master/Documentation/architecture/net-platform-standard.md) targeting netstandard version 1.1 - therefore it should work on the following plaforms:
* .NET Framework 4.5 and newer
* .NET Core
* Universal Windows Platform (uap)
* Windows 8.0 and newer
* Windows Phone (WinRT, not Silverlight)
* Mono / Xamarin

## Quickstart: Using the API Wrapper

```c#
// Initialize
var client = new WordPressClient("http://demo.wp-api.org/wp-json/");

// Posts
var posts = await client.Posts.GetAll();
var postbyid = await client.Posts.GetById(id);

// Comments
var comments = await client.Comments.GetAll();
var commentbyid = await client.Comments.GetById(id);
var commentsbypost = await client.Comments.GetCommentsForPost(postid, true, false);

// Users
// JWT authentication
var client = new WordPressClient(ApiCredentials.WordPressUri);
client.AuthMethod = AuthMethod.JWT;
await client.RequestJWToken(ApiCredentials.Username,ApiCredentials.Password);

// check if authentication has been successful
var isValidToken = await client.IsValidJWToken();

// now you can send requests that require authentication
var response = client.Posts.Delete(postid);
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

## Contribution Guidelines
We're very happy to get input from the community on this project! To keep the code clean we ask you to follow a few simple contribution guidelines.

First, create an issue describing what feature you want to add of what problem you're trying to solve, just to make sure no one is already working on that. That also gives us a chance to debate whether a feature is wihtin the scope of this project.

Second, please try to stick to the official C# coding guidelines. https://msdn.microsoft.com/en-us/library/ms229002(v=vs.110).aspx

Also, make sure to write some tests covering your new or modified code.
