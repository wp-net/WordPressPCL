# Custom Fields (Meta)

Custom fields — also called post meta or metadata — let you attach arbitrary key/value data to WordPress objects (posts, pages, comments, users, and media). WordPress exposes them through the REST API via the `meta` property that is present on every supported object type.

## How WordPress exposes custom fields via REST API

By default the `meta` property in a REST API response is an **empty object** (`{}`). Custom fields are **opt-in**: each meta key must be explicitly registered for REST API exposure, either through core WordPress functions or through plugins.

### Approach 1 — `register_post_meta()` (recommended)

Register a meta key server-side so that it appears in the `meta` object of post/page responses:

```php
add_action('rest_api_init', function () {
    register_post_meta('post', 'my_color', [
        'type'         => 'string',
        'description'  => 'A color value for the post.',
        'single'       => true,
        'show_in_rest' => true,
    ]);
});
```

> **Tip:** Use `register_term_meta()`, `register_comment_meta()`, and `register_user_meta()` for the corresponding object types.

Once registered, the field appears in responses like this:

```json
{
  "id": 42,
  "title": { "rendered": "Hello world" },
  "meta": {
    "my_color": "blue"
  }
}
```

### Approach 2 — `register_rest_field()`

`register_rest_field()` adds a *top-level* field to REST responses (not nested under `meta`). This is useful for computed values or when you need full control over serialisation:

```php
add_action('rest_api_init', function () {
    register_rest_field('post', 'my_color', [
        'get_callback'    => function ($post) {
            return get_post_meta($post['id'], 'my_color', true);
        },
        'update_callback' => function ($value, $post) {
            update_post_meta($post->ID, 'my_color', sanitize_text_field($value));
        },
        'schema'          => [
            'type'        => 'string',
            'description' => 'A color value for the post.',
        ],
    ]);
});
```

Fields registered this way appear at the top level of the JSON object, alongside `id`, `title`, etc.  
Use the `CustomRequest` approach described below to map them.

### Approach 3 — Advanced Custom Fields (ACF) plugin

The popular [ACF plugin](https://www.advancedcustomfields.com/) can expose field groups through the REST API when the **Show in REST API** option is enabled on the field group. ACF exposes the data under an `acf` top-level key. Use the [Custom Request](../customization/customRequest.md) approach to work with ACF data.

## Supported object types

The `Meta` property is available on the following WordPressPCL models:

| Model | WordPress endpoint |
|---|---|
| `Post` | `/wp/v2/posts` |
| `Page` | `/wp/v2/pages` |
| `Comment` | `/wp/v2/comments` |
| `MediaItem` | `/wp/v2/media` |
| `PostRevision` | `/wp/v2/revisions` |
| `User` | `/wp/v2/users` |

## Reading custom fields

The `Meta` property is typed as `JsonElement?` so that it can accommodate any JSON shape returned by WordPress (string, number, array, or nested object).

```csharp
Post post = await client.Posts.GetByIdAsync(123);

// Read a simple string value
string? color = post.Meta?.GetProperty("my_color").GetString();

// Read an integer value
int? count = post.Meta?.GetProperty("view_count").GetInt32();
```

### Deserialize to a strongly-typed class

Define a class that mirrors the shape of your meta fields and deserialize the element directly:

```csharp
public class PostMeta
{
    [JsonPropertyName("my_color")]
    public string? Color { get; set; }

    [JsonPropertyName("view_count")]
    public int ViewCount { get; set; }
}

Post post = await client.Posts.GetByIdAsync(123);
PostMeta? meta = post.Meta?.Deserialize<PostMeta>();
Console.WriteLine(meta?.Color); // e.g. "blue"
```

## Writing / updating custom fields

Serialize your data into a `JsonElement` and assign it to the `Meta` property before calling `UpdateAsync`.

### Update a single meta key

```csharp
Post post = new Post
{
    Id = 123,
    Meta = JsonSerializer.SerializeToElement(new Dictionary<string, object?>
    {
        ["my_color"] = "blue"
    }),
};

await client.Posts.UpdateAsync(post);
```

### Update multiple meta keys

```csharp
Post post = new Post
{
    Id = 123,
    Meta = JsonSerializer.SerializeToElement(new Dictionary<string, object?>
    {
        ["my_color"]   = "blue",
        ["view_count"] = 42,
    }),
};

await client.Posts.UpdateAsync(post);
```

### Update with a strongly-typed class

```csharp
PostMeta meta = new PostMeta { Color = "red", ViewCount = 10 };

Post post = new Post
{
    Id = 123,
    Meta = JsonSerializer.SerializeToElement(meta),
};

await client.Posts.UpdateAsync(post);
```

> **Important:** Only meta keys that have been registered with `show_in_rest: true` (see above) can be written through the REST API. Attempting to update an unregistered key will silently ignore the value.

## Top-level REST fields (register_rest_field)

When a plugin uses `register_rest_field()` to add a field at the top level of the response — for example `acf` — it will not appear in `Meta`. Use the [Custom Request](../customization/customRequest.md) feature to work with these fields by defining a custom model:

```csharp
public class PostWithAcf
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public WordPressPCL.Models.Title? Title { get; set; }

    [JsonPropertyName("acf")]
    public MyAcfFields? Acf { get; set; }
}

public class MyAcfFields
{
    [JsonPropertyName("my_color")]
    public string? Color { get; set; }
}

// Fetch a post with ACF fields
PostWithAcf? post = await client.CustomRequest.GetByIdAsync<PostWithAcf>("wp/v2/posts", 123);
Console.WriteLine(post?.Acf?.Color);
```

## Further reading

- [WordPress REST API — Post meta](https://developer.wordpress.org/rest-api/reference/post-meta/)
- [`register_post_meta()` reference](https://developer.wordpress.org/reference/functions/register_post_meta/)
- [`register_rest_field()` reference](https://developer.wordpress.org/rest-api/extending-the-rest-api/modifying-responses/)
- [Custom Requests in WordPressPCL](../customization/customRequest.md)
