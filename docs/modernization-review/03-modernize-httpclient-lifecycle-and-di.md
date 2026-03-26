# Modernize HttpClient lifecycle and dependency injection integration

## Summary

Align the library with current .NET HTTP client guidance by improving default `HttpClient` ownership and adding first-class DI registration.

## Why this matters

- Modern .NET apps typically use `IHttpClientFactory` and DI.
- The library already allows `HttpClient` injection, but the default path still creates and owns its own client per `WordPressClient` instance.
- Better DI support would make the library easier to adopt in ASP.NET Core and worker services.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Utility/HttpHelper.cs:18-20`
  - `private readonly HttpClient _defaultHttpClient = new();`
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Utility/HttpHelper.cs:67-78`
  - the default constructor assigns `_httpClient = _defaultHttpClient`.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/WordPressClient.cs:123-156`
  - constructors support either a raw URI or an externally provided `HttpClient`, but there are no DI helpers.
- Repository search found no `IServiceCollection` or `IHttpClientFactory` usage.

## Suggested outcome

- Clarify `HttpClient` ownership and disposal semantics.
- Add an extensions package or built-in extension methods for `IServiceCollection`.
- Provide a recommended typed-client registration pattern for `WordPressClient`.

## Acceptance criteria

- A documented and tested DI registration path exists.
- `HttpClient` ownership is explicit and safe.
- Consumers can configure the base address and auth defaults through DI.
- Documentation shows both direct construction and DI-based usage.

## Breaking change considerations

Because this is planned for a new major version, disposal semantics and constructor behavior can be redesigned more directly as long as ownership rules are clearly documented.
