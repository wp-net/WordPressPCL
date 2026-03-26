# Expose pagination and response metadata directly in the API

## Summary

Add first-class access to pagination and response metadata instead of requiring callers or internal loops to depend on raw headers.

## Why this matters

- WordPress REST APIs expose useful metadata such as `X-WP-Total` and `X-WP-TotalPages`.
- The library already reads response headers internally, but that data is not part of the primary public result model.
- A richer result type would improve large-list scenarios and reduce repeated ad hoc pagination logic.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Utility/HttpHelper.cs:57-60`
  - `LastResponseHeaders` is stored as mutable state on the helper.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Client/CRUDOperation.cs:99-113`
  - `GetAllAsync` loops by reading `X-WP-TotalPages` from `LastResponseHeaders`.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Client/Media.cs:132-145`
  - media duplicates similar pagination logic.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Client/Posts.cs:105-107`
  - `GetCountAsync` relies on a separate HEAD request instead of a richer list result.

## Suggested outcome

- Introduce a paged result model that carries items plus pagination metadata.
- Reduce duplicated pagination loops across clients.
- Expose metadata without requiring callers to inspect mutable helper state.

## Acceptance criteria

- A typed pagination/result model is available for list endpoints.
- Existing simple APIs stay usable, with additive overloads if needed.
- Repeated header-parsing logic is consolidated.
- Documentation shows how to retrieve item counts and total pages from the library API.

## Breaking change considerations

Prefer additive APIs or overloads to avoid breaking current `List<T>` callers immediately.
