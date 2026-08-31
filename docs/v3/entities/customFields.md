# Custom Fields (Meta)

Custom fields, also called metadata, let you attach key/value data to WordPress objects. The core REST API exposes registered metadata in a nested `meta` property, which WordPressPCL maps to each model's `Meta` property. Plugins and custom endpoints can also add fields at the top level of a response; WordPressPCL v3 captures those unmapped fields in the `Base.CustomFields` dictionary inherited by its models.

## How WordPress exposes custom fields via REST API

REST API access to metadata is **opt-in**: each meta key must be explicitly registered for REST exposure, either through core WordPress functions or through a plugin. When no registered values are available, WordPress commonly returns an empty `meta` object.

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

Fields registered this way appear at the top level of the JSON object, alongside `id`, `title`, etc. In WordPressPCL v3, built-in models collect these fields in `Base.CustomFields`.

### Approach 3 — Advanced Custom Fields (ACF) plugin

The popular [ACF plugin](https://www.advancedcustomfields.com/) can expose field groups through the REST API when the **Show in REST API** option is enabled on the field group. ACF returns the data under a top-level `acf` key, which is available through `Base.CustomFields` when using a built-in WordPressPCL model.

## Supported object types

The `Meta` property is available on the following WordPressPCL models:

| Model | WordPress endpoint |
|---|---|
| `Post` | `/wp/v2/posts` |
| `Page` | `/wp/v2/pages` |
| `Comment` | `/wp/v2/comments` |
| `MediaItem` | `/wp/v2/media` |
| `PostRevision` | `/wp/v2/posts/{postId}/revisions` |
| `User` | `/wp/v2/users` |
| `Category` | `/wp/v2/categories` |
| `Tag` | `/wp/v2/tags` |

Revisions are nested under their parent post, so their route includes the post ID.

## Reading registered meta

The `Meta` property is typed as `JsonElement?` so that it can accommodate any JSON shape returned by WordPress (string, number, array, or nested object).

```csharp
Post post = await client.Posts.GetByIdAsync(123);

if (post.Meta is JsonElement meta &&
    meta.TryGetProperty("my_color", out JsonElement colorProperty))
{
    string? color = colorProperty.GetString();
}
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

## Writing registered meta

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

> **Important:** Only meta keys registered with `show_in_rest: true` can be read or written through the REST API's `meta` property.

## Top-level REST fields (register_rest_field)

When a plugin uses `register_rest_field()` to add a field at the top level of a response, it will not appear in `Meta`. Entity models such as `Post` derive from `Base`, whose `CustomFields` dictionary contains these unmapped top-level values as `JsonElement` instances after deserialization.

```csharp
public class MyAcfFields
{
    [JsonPropertyName("my_color")]
    public string? Color { get; set; }
}

Post post = await client.Posts.GetByIdAsync(123);

if (post.CustomFields?.TryGetValue("acf", out object? rawAcf) == true &&
    rawAcf is JsonElement acfElement)
{
    MyAcfFields? acf = acfElement.Deserialize<MyAcfFields>();
    Console.WriteLine(acf?.Color);
}
```

Entries assigned to `CustomFields` are serialized as top-level properties, so the same API can send plugin fields when the endpoint supports updates:

```csharp
Post post = new()
{
    Id = 123,
    CustomFields = new Dictionary<string, object>
    {
        ["my_color"] = "blue"
    }
};

await client.Posts.UpdateAsync(post);
```

### Custom DTOs and endpoints

Use [Custom Request](../customization/customRequest.md) when an endpoint has no built-in client or when you want to deserialize the complete response into a custom DTO:

```csharp
public class PostWithAcf
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("acf")]
    public MyAcfFields? Acf { get; set; }
}

PostWithAcf post = await client.CustomRequest.GetAsync<PostWithAcf>(
    "wp/v2/posts/123",
    useAuth: true);
```

`CustomRequest` accepts the complete resource route and exposes `GetAsync<T>`, rather than a `GetByIdAsync` method.

## Further reading

- [WordPress REST API — Post meta](https://developer.wordpress.org/rest-api/reference/post-meta/)
- [`register_post_meta()` reference](https://developer.wordpress.org/reference/functions/register_post_meta/)
- [`register_rest_field()` reference](https://developer.wordpress.org/rest-api/extending-the-rest-api/modifying-responses/)
- [Custom Requests in WordPressPCL](../customization/customRequest.md)
