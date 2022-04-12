# Custom Post Type

To create post of custom post type you should do 2 things
1. Enable rest api support for custom post type [registering-a-custom-post-type-with-rest-api-support](https://developer.wordpress.org/rest-api/extending-the-rest-api/adding-rest-api-support-for-custom-content-types/#registering-a-custom-post-type-with-rest-api-support)
2. This is a one of the unintuitive things in WP REST API, but for creating post with custom type you should send requests to custom endpoint. For this task you can use our [Custom Requests](https://github.com/wp-net/WordPressPCL/wiki/CustomRequests) feature


Example:

```c#
var post = new Post()
{
       Title = new Title("Title 1"),
       Content = new Content("Content PostCreate"),
       Type = "portfolio" //your custom post type
};
//change portfolio to your custom post type
var createdPost = await _clientAuth.CustomRequest.CreateAsync<Post, Post>("wp/v2/portfolio", post); 
```