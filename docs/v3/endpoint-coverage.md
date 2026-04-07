# Endpoint coverage

WordPressPCL focuses on the most commonly used WordPress REST API endpoints and exposes them as strongly typed clients on `WordPressClient`.

## Endpoints with dedicated clients

| WordPress endpoint | WordPressPCL entry point | Notes |
|--------------------|--------------------------|-------|
| `wp/v2/posts` | `client.Posts` | CRUD, count, sticky/filter helpers |
| `wp/v2/posts/<id>/revisions` | `client.Posts.Revisions(postId)` | Post revisions only |
| `wp/v2/pages` | `client.Pages` | CRUD |
| `wp/v2/comments` | `client.Comments` | CRUD |
| `wp/v2/categories` | `client.Categories` | CRUD |
| `wp/v2/tags` | `client.Tags` | CRUD |
| `wp/v2/users` | `client.Users` | CRUD, current user, application passwords |
| `wp/v2/media` | `client.Media` | CRUD plus uploads |
| `wp/v2/taxonomies` | `client.Taxonomies` | Read/query only |
| `wp/v2/types` | `client.PostTypes` | Read only |
| `wp/v2/statuses` | `client.PostStatuses` | Read only |
| `wp/v2/settings` | `client.Settings` | Read/update only |
| `wp/v2/plugins` | `client.Plugins` | Install, activate, deactivate, delete |
| `wp/v2/themes` | `client.Themes` | Read/query only |

## Standard endpoints without dedicated wrappers

The official WordPress REST API reference includes additional standard endpoints that currently do not have dedicated WordPressPCL clients. The main gaps are:

- `wp/v2/search`
- block editor endpoints such as `wp/v2/block-types`, `wp/v2/blocks`, `wp/v2/block-renderer`, `wp/v2/templates`, `wp/v2/template-parts` and `wp/v2/global-styles`
- navigation and site editing endpoints such as `wp/v2/navigation` and `wp/v2/navigation-fallback`
- `wp/v2/sidebars`, `wp/v2/widgets` and `wp/v2/widget-types`
- `wp/v2/url-details`
- autosaves and page revisions

As WordPress adds more standard endpoints, the authoritative way to see what a site exposes is its API index at `/wp-json/`.

## Using discovery and CustomRequest

Use the site index to discover available namespaces and routes:

```csharp
dynamic apiIndex = await client.CustomRequest.GetAsync<dynamic>("", ignoreDefaultPath: true);
```

These examples use `dynamic` for brevity. If you already know the response shape, prefer a DTO or a JSON type that matches your project style.

Use `CustomRequest` for standard endpoints that do not yet have dedicated wrappers:

```csharp
dynamic searchResults = await client.CustomRequest.GetAsync<dynamic>("search?search=hello", ignoreDefaultPath: false);
dynamic pageRevisions = await client.CustomRequest.GetAsync<dynamic>("pages/42/revisions", useAuth: true, ignoreDefaultPath: false);
```

Use `CustomRequest` for plugin namespaces and custom site routes:

```csharp
dynamic products = await client.CustomRequest.GetAsync<dynamic>("wc/v3/products", useAuth: true);
```

## Notes

- Themes are available through `client.Themes`, but they are read/query only.
- Post revisions are available through `client.Posts.Revisions(postId)`.
- Pages do not currently have a dedicated revisions or autosaves helper.
