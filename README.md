# WordPressPCL
This is a portable library for consuimg the WordPress REST-API in (almost) any C# application.
The project is still very much in beta so use at your own risk! 
If you find bugs or have any suggestions, feel free to create an issue.

#Quickstart

## WordPress Plugins
As the WP REST API (Version 2) Plugin is currently being integrated into WordPress core you'll still need to install the plugin on your site for this library to work. Also, there are two additional plugins for authentication.

* [WordPress REST API (Version 2)](https://wordpress.org/plugins/rest-api/)
* [Basic Authentication handler](https://github.com/WP-API/Basic-Auth)
* [WP REST API - OAuth 1.0a Server](https://github.com/WP-API/OAuth1)

## Including WordPressPCL
The WordPressPCL API Wrapper is avaiable through nuget:

```> Install-Package WordPressPCL```

## Supported Plattforms
WordPressPCL is built on top of the new [.NET Platform Standard](https://github.com/dotnet/corefx/blob/master/Documentation/architecture/net-platform-standard.md) targeting netstandard version 1.1 - therefore it should work on the following plaforms:
* .NET Framework 4.5 and newer
* .NET Core
* Universal Windows Platform (uap)
* Windows 8.0 and newer
* Windows Phone (WinRT, not Silverlight)
* Mono / Xamarin

## Using the API Wrapper

```c#
// Initialize
var client = new WordPressClient("http://demo.wp-api.org/wp-json/wp/v2/");

// Posts
var posts = await client.ListPosts();
var postbyid = await client.GetPost(id);

// Comments
var comments = await client.ListComments();
var commentbyid = await client.GetComment(id);

// Users
// Basic authentication - not recommended for production use
client.Username = "TheUserName";
client.Password = "TheUserPassword";
var currentuser = await client.GetCurrentUser();
```
    
