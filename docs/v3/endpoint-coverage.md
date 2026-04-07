# Endpoint coverage

WordPressPCL focuses on the most commonly used WordPress REST API endpoints and exposes them as strongly typed clients on `WordPressClient`.

## Endpoints with dedicated clients

| WordPress endpoint | WordPressPCL entry point | Notes |
|--------------------|--------------------------|-------|
| `wp/v2/posts` | `client.Posts` | CRUD, count, sticky/filter helpers |
| `wp/v2/posts/<id>/revisions` | `client.Posts.Revisions(postId)` | Post revisions |
| `wp/v2/posts/<id>/autosaves` | `client.Posts.Autosaves(postId)` | Post autosaves |
| `wp/v2/pages` | `client.Pages` | CRUD |
| `wp/v2/pages/<id>/revisions` | `client.Pages.Revisions(pageId)` | Page revisions |
| `wp/v2/pages/<id>/autosaves` | `client.Pages.Autosaves(pageId)` | Page autosaves |
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
| `wp/v2/block-types` | `client.BlockTypes` | Read only |
| `wp/v2/blocks` | `client.Blocks` | CRUD (reusable blocks) |
| `wp/v2/templates` | `client.Templates` | CRUD (requires auth) |
| `wp/v2/template-parts` | `client.TemplateParts` | CRUD (requires auth) |
| `wp/v2/global-styles/<id>` | `client.GlobalStyles` | Read/update by ID or theme |
| `wp/v2/navigation` | `client.Navigation` | CRUD |
| `wp/v2/sidebars` | `client.Sidebars` | Read/update only |
| `wp/v2/widgets` | `client.Widgets` | CRUD |
| `wp/v2/widget-types` | `client.WidgetTypes` | Read only |
| `wp/v2/url-details` | `client.UrlDetails` | Read by URL (requires auth) |

## Standard endpoints without dedicated wrappers

The following standard WordPress REST API endpoints do not yet have dedicated WordPressPCL clients:

- `wp/v2/block-renderer` (POST to render a block server-side)
- `wp/v2/navigation-fallback` (GET the navigation fallback menu)
- `wp/v2/global-styles/<id>/revisions` (global styles revision history)
- font families and font faces (`wp/v2/font-families`, `wp/v2/font-faces`)
- pattern directory (`wp/v2/pattern-directory/patterns`)

As WordPress adds more standard endpoints, the authoritative way to see what a site exposes is its API index at `/wp-json/`.

## Using discovery and CustomRequest

Use the site index to discover available namespaces and routes:

```csharp
dynamic apiIndex = await client.CustomRequest.GetAsync<dynamic>("", ignoreDefaultPath: true);
```

These examples use `dynamic` for brevity. If you already know the response shape, prefer a DTO or a JSON type that matches your project style.

Use `CustomRequest` for standard endpoints that do not yet have dedicated wrappers:

```csharp
dynamic rendered = await client.CustomRequest.PostAsync<dynamic>("block-renderer/core%2Fparagraph", body, ignoreDefaultPath: false);
dynamic navFallback = await client.CustomRequest.GetAsync<dynamic>("navigation-fallback", useAuth: true, ignoreDefaultPath: false);
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
- Templates and template parts use compound string IDs (e.g. `"twentytwentyfour//index"`).
- Sidebars, widgets and widget types use string IDs (e.g. `"sidebar-1"`, `"block-3"`).
- `client.GlobalStyles` exposes `GetByIdAsync(id)`, `GetThemeStylesAsync(stylesheet)` and `UpdateAsync(entity)`.
