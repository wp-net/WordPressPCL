# Contributing to WordPressPCL

Thank you for your interest in contributing! This guide explains how to set up a development environment, run the tests, and submit changes.

## Repository layout

```
WordPressPCL/
├── src/
│   └── WordPressPCL/          # Library source code
├── tests/
│   ├── WordPressPCL.Tests.Selfhosted/   # Integration tests against a local Docker WordPress
│   └── WordPressPCL.Tests.Hosted/       # Smoke tests against a live WordPress instance
├── docs/
│   ├── v3/                    # Reference docs for version 3.x
│   ├── v2/                    # Reference docs for version 2.x
│   └── v1/                    # Reference docs for version 1.x
├── dev/                       # Docker Compose setup for local WordPress
├── WordPressPCL.sln
├── Directory.Build.props
├── Directory.Packages.props
├── CHANGELOG.md
└── mkdocs.yml
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker and Docker Compose](https://docs.docker.com/get-docker/) (for self-hosted integration tests)

## Building

```bash
dotnet restore WordPressPCL.sln --disable-parallel
dotnet build WordPressPCL.sln -c Release --no-restore
```

## Running tests

### Self-hosted integration tests (recommended for most changes)

The self-hosted tests spin up a local WordPress instance via Docker Compose and run the full test suite against it.

```bash
# Start the test environment
cd dev
docker compose up -d

# Wait for WordPress to be ready (the container creates .wp-tests-ready when done)
# Then run the tests from the repository root
cd ..
dotnet test tests/WordPressPCL.Tests.Selfhosted/WordPressPCL.Tests.Selfhosted.csproj \
  -s tests/WordPressPCL.Tests.Selfhosted/jwtauth.runsettings \
  -l "console;verbosity=detailed"
```

For full setup instructions, including plugin requirements and run-settings options, see [`dev/install.md`](dev/install.md).

### Hosted tests

The hosted tests run against a live WordPress site. They depend on specific content and credentials and may fail for environment reasons unrelated to your change. They are mainly used in CI and are not required for local development.

## Code style

Please follow the [Microsoft C# coding guidelines](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions). Nullable reference types are enabled solution-wide — do not suppress nullable warnings without a clear justification.

## Submitting a pull request

1. **Open an issue first.** Describe the feature or bug so we can discuss whether it is in scope and avoid duplicate work.
2. Fork the repository and create a branch from `main`.
3. Make your changes. Add or update tests to cover any new or modified behaviour.
4. Run the build and self-hosted tests to verify nothing is broken.
5. Open a pull request against `main`. Reference the related issue in the description.

## Reporting bugs

Please [open an issue](https://github.com/wp-net/WordPressPCL/issues) with a clear description, the version of WordPressPCL you are using, and a minimal reproduction.

## Code of conduct

This project follows the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). By participating you agree to abide by its terms.
