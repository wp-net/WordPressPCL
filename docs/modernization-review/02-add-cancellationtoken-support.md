# Add CancellationToken support to public async APIs

## Summary

Add optional `CancellationToken` parameters to public async methods and thread them through the HTTP stack.

## Why this matters

- Cancellation is a standard expectation for modern .NET HTTP libraries.
- Consumers need to cancel long-running requests, respect request timeouts, and stop work during shutdown.
- Without cancellation support, callers must rely on coarse global timeouts or abandon tasks.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Interfaces/IReadOperation.cs:18-35`
  - `GetAsync`, `GetByIDAsync`, and `GetAllAsync` do not accept a `CancellationToken`.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Interfaces/ICountOperation.cs:8-15`
  - `GetCountAsync()` has no token.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Utility/HttpHelper.cs:99-209`
  - `SendAsync` calls are made without a cancellation token.
- Repository search found no `CancellationToken` usage in the C# source tree.

## Suggested outcome

- Add optional `CancellationToken cancellationToken = default` to public async methods.
- Pass the token through `HttpHelper` into `HttpClient.SendAsync`.
- Update tests and docs to show cancellation-aware usage.

## Acceptance criteria

- Public async methods expose an optional `CancellationToken`.
- Internal request helpers accept and forward the token.
- Representative tests verify cancellation is honored.
- Documentation examples show the new overload pattern.

## Breaking change considerations

Because this work targets a new major version, the API can be updated consistently across all async methods without preserving the current signatures where that gets in the way of a cleaner design.
