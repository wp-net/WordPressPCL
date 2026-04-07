# Custom HttpClient

You can inject your own instance of an HttpClient into the WordPressClient. This allows you to re-use an existing instance, set desired headers etc.

```c#
var httpClient = new HttpClient
{
    BaseAddress = new Uri(ApiCredentials.WordPressUri)
};
httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0");
httpClient.DefaultRequestHeaders.Add("Referer", "https://github.com/wp-net/WordPressPCL");

var client = new WordPressClient(httpClient);
var posts = await client.Posts.GetAllAsync();
```