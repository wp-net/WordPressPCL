# Changelog

All notable changes to this project will be documented in this file.

The project now uses GitHub Releases as the source of truth for versioned release notes. Create releases with tags like `v3.0.1` or `v3.1.0-rc.1` and keep this changelog in sync with the published release.

## [Unreleased]

### Changed

- Placeholder for upcoming changes before the next GitHub release is published.

## [3.0.0]

### Added

- `CancellationToken` support across the public async API surface.
- `PagedResult<T>` plus paged query/get helpers for exposing pagination metadata without parsing headers manually.
- First-class `IServiceCollection` registration helpers for `WordPressClient`.
- Opt-in structured logging diagnostics for the HTTP request/response lifecycle.
- Versioned reference docs for 3.x, while restoring dedicated 2.x and 1.x reference sets.
- Expanded documentation for endpoint coverage, custom fields, and updated HTTPS quickstart/auth examples.

### Changed

- Target .NET 10.
- Dropped previous .NET Standard and .NET 6 test targets.
- Retargeted the library, tests, and CI/tooling to .NET 10.
- Centralized shared .NET configuration via `Directory.Build.props` and `Directory.Packages.props`.
- Enabled nullable reference types across the public API and the full solution.
- Migrated serialization from `Newtonsoft.Json` to `System.Text.Json`.
- Standardized `GetByIdAsync` naming across the API surface and aligned remaining public async method names/signatures.
- Modernized `HttpClient` ownership so DI-managed clients can be injected cleanly.
- Cleaned build warnings and aligned analyzer usage with the .NET 10 SDK.
- Adopted GitHub Release-based publishing with tag-driven versioning and changelog-driven release notes.
- Updated GitHub Actions workflows for the `main` branch, Docker Compose v2, and newer action runtimes.

### Fixed

- Comment status updates now serialize correctly.
- Comment updates skip null-only meta payloads that should not be sent.
- Docker-based self-hosted integration test setup is more reliable.
- Documentation and examples were refreshed to match current API names and behavior.

## [2.1.0]

### Changed

- Upgraded dependencies.
- Fixed `GetCurrentUserAsync()` naming.
- Allowed `HttpClient` injection without a base address.
- Added optional `ignoreDefaultPath` on `CustomRequest` methods.

## [2.0.2]

### Added

- Support for plugins.
- Support for statuses in `UsersQueryBuilder`.

### Changed

- Upgraded `Newtonsoft.Json` to 13.0.3.

## [2.0.1]

### Changed

- Upgraded `Newtonsoft.Json` to 13.0.1.

## [2.0.0]

### Added

- `WordPressClient` accepts the endpoint as a `Uri`.
- Support for getting the total post count without fetching the posts.
- Generic JWT and Basic Auth support.

### Changed

- Dropped `netstandard1.x` support.
- Added the `Async` suffix to all public async methods.
- Moved auth-related functionality to the auth sub-client.
- Moved settings functionality to the settings sub-client.

### Fixed

- Improved error handling for `WP_DEBUG_DISPLAY`.

## [1.9.0]

### Added

- Support for application passwords.

### Changed

- Raised the minimum target from `netstandard1.1` to `netstandard1.3`.

## [1.8.5]

### Fixed

- Query builder now appends default enum values to the query string for easier debugging.

## [1.8.4]

### Fixed

- `HttpClient` injection.

## [1.8.2]

### Added

- Support for the JWT Auth plugin.

## [1.7.2]

### Added

- Support for posts trash status.
- Support for providing an `HttpClient` to `WordPressClient`.

## [1.7.1]

### Added

- Package icon.

### Changed

- Updated the license expression.

## [1.7.0]

### Added

- Optional MIME type override for media upload.

### Changed

- Improved handling of `HttpClient` headers.
- Downgraded JSON.NET to 11.0.1.
- Refactored exceptions.

## [1.6.2]

### Added

- `.kmz` and `.kml` MIME types (#162).

### Fixed

- Cleaned up the file stream after upload (#166).

## [1.6.1]

### Fixed

- `Capabilities` can now contain strings instead of only booleans (#147).

## [1.6.0-beta1]

### Changed

- Marked all `Meta` properties as dynamic because the structure can be volatile.

## [1.5.1]

### Fixed

- `MediaSizes` height and width are now optional (#143).

## [1.5.0]

### Fixed

- Enhanced error handling.
- Addressed issue [#138](https://github.com/wp-net/WordPressPCL/issues/138).

## [1.4.6]

### Fixed

- Added `DefaultValueHandling` to the comment status property.

## [1.4.5]

### Fixed

- Added `NullValueHandling` to `FeaturedMedia`.

## [1.4.4]

### Added

- Yoast taxonomy terms.

## [1.4.3]

### Fixed

- Added default serializer settings with `MissingMemberHandling.Ignore`.

## [1.4.2]

### Fixed

- Delete requests now return `bool` instead of `HttpResponseMessage`.

## [1.4.1-alpha]

### Added

- Experimental WordPress.com support (read-only).
- Experimental descending threaded comments support.

## [1.4.0]

### Changed

- Made `HttpClient` static.

### Fixed

- Exceptions are now thrown instead of being hidden.
- Stopped auto-appending `/wp-json` to the WordPress URI.

## [1.3.3]

### Fixed

- Updating `Comment.Status`.

## [1.3.2]

### Fixed

- `Comment.Status` handling.

## [1.3.1]

### Added

- `maxDepth` option for threaded comments.

## [1.3.0]

### Added

- `ToThreaded` method for transforming comments.
- `.NET Standard 2.0` support.
- Uploading media directly from a file path (`.NET Standard 2.0` only).

### Changed

- Passed deserialization settings into `HttpHelper`.

## [1.2.1]

### Added

- JWT getter and setter methods.

## [1.2.0]

### Added

- Helper method to sort comments for a threaded view.
- Method to get all comments for a post id.
- Logout method.

### Fixed

- Improved async performance in several code paths.
