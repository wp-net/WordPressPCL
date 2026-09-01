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
| `wp/v2/templates` | `client.Templates` | CRUD and collection filters in WordPress 5.8+; fallback lookup requires WordPress 6.1+; authentication required |
| `wp/v2/template-parts` | `client.TemplateParts` | CRUD and collection filters; WordPress 5.8+ and authentication required |
| `wp/v2/global-styles/<id>` | `client.GlobalStyles` | Read/update a stored record by ID; WordPress 5.9+ and authentication required |
| `wp/v2/global-styles/themes/<stylesheet>` | `client.GlobalStyles` | Read merged settings and styles for the active theme; authentication required |
| `wp/v2/sidebars` | `client.Sidebars` | Public reads when a sidebar has `show_in_rest`; authenticated widget-assignment updates; WordPress 5.8+ |
| `wp/v2/widgets` | `client.Widgets` | Public reads when a sidebar has `show_in_rest`; authenticated CRUD and sidebar filtering with string IDs; WordPress 5.8+ |
| `wp/v2/widget-types` | `client.WidgetTypes` | Read only; WordPress 5.8+ and `edit_theme_options` required |

## Standard endpoints without dedicated wrappers

The official WordPress REST API reference includes additional standard endpoints that currently do not have dedicated WordPressPCL clients. The main gaps are:

- block rendering via `wp/v2/block-renderer/<name>`
- navigation fallback via `wp/v2/navigation-fallback`
- widget form-data encoding via `wp/v2/widget-types/<id>/encode`

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
- Templates and template parts use compound string IDs such as `twentytwentyfour//index`. Their clients preserve the slash separators and URL-encode each identifier segment.
- Template collection queries expose the core-supported `wp_id` and `post_type` filters; template parts additionally expose `area`.
- `client.Templates.GetFallbackAsync` uses the fallback lookup route introduced in WordPress 6.1.
- Global styles exposes only the core-supported single-record read/update operations and active-theme style retrieval. Core does not expose a global styles collection create or delete route.
- Sidebars and widgets use string IDs. Updating a sidebar replaces its ordered widget assignment, while widget create/update accepts either raw instance settings or encoded settings and a hash. WordPress generates IDs for newly created widgets.
- Widget-type reads and all writes require authentication and the `edit_theme_options` capability. Core exposes sidebar and widget reads without authentication when the relevant sidebar is registered with `show_in_rest`.
- The API index at `/wp-json/` is authoritative for route availability on a particular site.
