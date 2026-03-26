# Enable nullable reference types across the public API

## Summary

Enable nullable reference types and annotate the public API so consumers get compile-time feedback about null usage.

## Why this matters

- Modern C# libraries are expected to express nullability explicitly.
- The current API exposes many reference-type parameters and properties without nullable annotations.
- This makes it harder for consumers to know what can be `null`, and it increases the chance of runtime null-reference bugs.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/WordPressClient.cs:123-155`
  - public constructors accept reference types, including `string uri` and optional `Uri uri = null`, but nullability is not expressed through the type system.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Client/Auth.cs:65-71`
  - `UseBasicAuth(string username, string applicationPassword)` accepts required strings without annotations or argument validation.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Utility/HttpHelper.cs:27-59`
  - properties such as `JWToken`, `HttpResponsePreProcessing`, and `LastResponseHeaders` are nullable in practice but not annotated.

## Suggested outcome

- Enable nullable reference types project-wide.
- Annotate public APIs and DTOs deliberately.
- Add guard clauses for required inputs where null should be rejected.
- Preserve back-compat where practical, but make null behavior explicit.

## Acceptance criteria

- `<Nullable>enable</Nullable>` is set for the library project.
- Public constructors, methods, properties, and callbacks are annotated.
- Required string and object inputs validate arguments consistently.
- Tests cover representative null handling for public entry points.

## Breaking change considerations

This should be treated as a source-compatibility review item because consumers may see new compiler warnings after the change.
