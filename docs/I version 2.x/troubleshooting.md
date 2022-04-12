# Troubleshooting

## WordPressClient throws WPException
This happens when the rest api has received your request but for various reasons can not process it. You can catch this exception and in the additional information you can find out exactly what problems are with your request.
To do this, you can access the **BadRequest** property, which contains the fields:
* Name - the service name of the problem
* Message - description of the problem
* Data is a dynamic object that contains additional information about the exception
Example:
```C#
try
{
   //create empty post is not allowed
   var post = await client.Posts.CreateAsync(new Post());   
}
catch (WPException wpex)
{
   wpex.BadRequest.Name //system name
   wpex.BadRequest.Message //description
   wpex.BadRequest.Data //dynamic object with any non-structured additional info
}
```

### WordPressPCL.Models.Exceptions.WPException: No route was found matching the URL and request method
This usually happens when a serverside redirect to retreive a new JWT Token changes the POST request into a GET request (e.g. on www-to-non-www or http-to-https redirects).

## WordPressClient returns `null`
* Check your WordPress URL. It should look like this: `https://wordpress-site.com/wp-json/`
* Check your request. If you're making a post query that requires the edit context, or for whatever reason requires auth headers, you need to tell the query method to include the auth headers using an additional parameter, as it will *not* by default. Ex: `_client.Posts.QueryAsync(query, true);`