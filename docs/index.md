# Home

This is a .NET 10 library for consuming the WordPress REST API in C# applications.
If you find bugs or have any suggestions, feel free to create an issue.

## License
WordPressPCL is published under the [MIT License](https://github.com/wp-net/WordPressPCL/blob/master/LICENSE)

# Quickstart

## WordPress Requirements
Since WordPress 4.7 the REST API has been integrated into the core so there's no need for any plugins to get basic functionality. If you want to access protected endpoints, this library supports authentication through JSON Web Tokens (JWT) (plugin required).

* [WordPress 4.7 or newer](https://wordpress.org/)
* [JWT Authentication for WP REST API](https://wordpress.org/plugins/jwt-authentication-for-wp-rest-api/)

## Including WordPressPCL
The WordPressPCL API Wrapper is avaiable through [NuGet](https://www.nuget.org/packages/WordPressPCL/):

```
> Install-Package WordPressPCL
```

## Runtime Requirement
WordPressPCL 3.0 targets .NET 10 only. Upgrading from 2.x is a breaking change and requires applications and test environments to move to the .NET 10 SDK/runtime before restoring, building, or running tests.

## Supported Platforms
WordPressPCL 3.0 targets .NET 10 and is intended for applications running on the current .NET platform:
* .NET 10

## Quickstart: Using the API Wrapper

```c#
// Initialize
WordPressClient client = new WordPressClient("http://demo.wp-api.org/wp-json/");

// Posts
List<Post> posts = await client.Posts.GetAll();
Post postbyid = await client.Posts.GetById(id);

// Comments
List<Comment> comments = await client.Comments.GetAll();
Comment commentbyid = await client.Comments.GetById(id);
List<Comment> commentsbypost = await client.Comments.GetCommentsForPost(postid, true, false);

// Users
// JWT authentication
WordPressClient client = new WordPressClient(ApiCredentials.WordPressUri);
client.AuthMethod = AuthMethod.JWT;
await client.RequestJWToken(ApiCredentials.Username,ApiCredentials.Password);

// check if authentication has been successful
bool isValidToken = await client.IsValidJWToken();

// now you can send requests that require authentication
Task<bool> response = client.Posts.Delete(postid);
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
