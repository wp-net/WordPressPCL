# Add diagnostics and logging hooks for HTTP requests

## Summary

Add optional diagnostics and logging hooks so consumers can inspect requests, responses, failures, and pagination behavior.

## Why this matters

- REST wrappers are much easier to operate when callers can observe request behavior.
- Today the library throws exceptions and stores some response state, but it does not expose structured diagnostics.
- Logging support would help troubleshoot auth failures, plugin-specific WordPress responses, and malformed payload handling.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Utility/HttpHelper.cs:117-207`
  - request/response flow is implemented centrally, but no diagnostic events or logging calls are present.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Utility/HttpHelper.cs:247-305`
  - deserialization fallbacks and unexpected-response handling happen silently unless an exception is thrown.
- Repository search found no `ILogger` or `Microsoft.Extensions.Logging` usage.

## Suggested outcome

- Expose optional logging or callback hooks around request execution.
- Make correlation-friendly metadata available when exceptions are raised.
- Keep logging opt-in so the core package remains lightweight.

## Acceptance criteria

- Consumers can enable request/response diagnostics without modifying library code.
- Failures include enough context for troubleshooting without forcing raw-body logging by default.
- Documentation explains how to wire up diagnostics in a host application.

## Breaking change considerations

This should be additive if diagnostics are optional.
