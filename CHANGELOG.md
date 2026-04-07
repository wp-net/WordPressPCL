# Changelog

All notable changes to this project will be documented in this file.

The project now uses GitHub Releases as the source of truth for versioned release notes. Create releases with tags like `v3.0.1` or `v3.1.0-rc.1` and keep this changelog in sync with the published release.

## [Unreleased]

### Changed

- Placeholder for upcoming changes before the next GitHub release is published.

## [3.0.0]

### Changed

- Target .NET 10.
- Dropped previous .NET Standard and .NET 6 test targets.

## [2.1.0]

### Changed

- Upgraded dependencies.
- Fixed `GetCurrentUserAsync()` naming.
- Allowed `HttpClient` injection without a base address.
- Added optional `ignoreDefaultPath` on `CustomRequest` methods.

## [2.0.2]

### Added

- Support for plugins.

### Changed

- Upgraded `Newtonsoft.Json` to 13.0.3.

## [2.0.1]

### Changed

- Upgraded `Newtonsoft.Json` to 13.0.1.
