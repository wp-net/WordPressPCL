# Endpoint coverage

WordPressPCL focuses on the most commonly used WordPress REST API endpoints and exposes them as strongly typed clients on `WordPressClient`.

## Endpoints with dedicated clients

| WordPress endpoint | WordPressPCL entry point | Notes |
|--------------------|--------------------------|-------|
| `wp/v2/posts` | `client.Posts` | CRUD, count, sticky/filter helpers |
| `wp/v2/posts/<id>/revisions` | `client.Posts.Revisions(postId)` | Post revisions |
| `wp/v2/posts/<id>/autosaves` | `client.Posts.Autosaves(postId)` | Create and read autosaves |
| `wp/v2/pages` | `client.Pages` | CRUD |
| `wp/v2/pages/<id>/revisions` | `client.Pages.Revisions(pageId)` | Page revisions |
| `wp/v2/pages/<id>/autosaves` | `client.Pages.Autosaves(pageId)` | Create and read autosaves |
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
| `wp/v2/search` | `client.Search` | Read/query only |
| `wp-block-editor/v1/url-details` | `client.UrlDetails` | Read by URL (requires auth) |
| `wp/v2/block-types` | `client.BlockTypes` | Read/query only; WordPress 5.5+ and authentication required |
| `wp/v2/blocks` | `client.Blocks` | CRUD for reusable blocks; WordPress 5.0+ and authentication required |
| `wp/v2/navigation` | `client.Navigation` | CRUD for `wp_navigation`; WordPress 5.9+ and authentication required |

## Standard endpoints without dedicated wrappers

The official WordPress REST API reference includes additional standard endpoints that currently do not have dedicated WordPressPCL clients. The main gaps are:

- block editor endpoints such as `wp/v2/block-renderer`, `wp/v2/templates`, `wp/v2/template-parts` and `wp/v2/global-styles`
- navigation and site editing endpoints such as `wp/v2/navigation-fallback`
- `wp/v2/sidebars`, `wp/v2/widgets` and `wp/v2/widget-types`

As WordPress adds more standard endpoints, the authoritative way to see what a site exposes is its API index at `/wp-json/`.

## Using discovery and CustomRequest

Use the site index to discover available namespaces and routes:

```csharp
dynamic apiIndex = await client.CustomRequest.GetAsync<dynamic>("", ignoreDefaultPath: true);
```

These examples use `dynamic` for brevity. If you already know the response shape, prefer a DTO or a JSON type that matches your project style.

Use `CustomRequest` for standard endpoints that do not yet have dedicated wrappers:

```csharp
dynamic renderedBlock = await client.CustomRequest.PostAsync<dynamic>("block-renderer/core%2Fparagraph", body, ignoreDefaultPath: false);
```

Use `CustomRequest` for plugin namespaces and custom site routes:

```csharp
dynamic products = await client.CustomRequest.GetAsync<dynamic>("wc/v3/products", useAuth: true);
```

## Notes

- Themes are available through `client.Themes`, but they are read/query only.
- Post revisions are available through `client.Posts.Revisions(postId)`.
- Page revisions are available through `client.Pages.Revisions(pageId)`.
- Post autosaves are available through `client.Posts.Autosaves(postId)`.
- Page autosaves are available through `client.Pages.Autosaves(pageId)`.
- URL details are available through `client.UrlDetails` and require authentication.
- Block types were added in WordPress 5.5 and require a user who can edit at least one REST-enabled post type.
- Reusable blocks were added in WordPress 5.0 and are stored as `wp_block` posts. Core requires suitable post-editing permissions for reads and writes, so `client.Blocks` authenticates read requests by default. Current core responses expose raw block markup and may omit rendered title/content.
- Navigation posts were added in WordPress 5.9. Core maps `wp_navigation` editing capabilities to `edit_theme_options`; `client.Navigation` therefore authenticates reads by default, as well as writes.
- The API index at `/wp-json/` is authoritative for route availability on a particular site.
