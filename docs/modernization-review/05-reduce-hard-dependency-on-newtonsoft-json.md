# Reduce hard dependency on Newtonsoft.Json

## Summary

Introduce a serializer abstraction and evaluate a migration path toward `System.Text.Json` for modern target frameworks.

## Why this matters

- The library is tightly coupled to `Newtonsoft.Json` in both API configuration and implementation.
- Modern .NET libraries often prefer `System.Text.Json` where possible, or at least avoid hardwiring serializer details into the public surface.
- A serializer abstraction would make the library easier to evolve without forcing a single implementation forever.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/WordPressPCL.csproj:147-154`
  - the library references `Newtonsoft.Json` directly.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/WordPressClient.cs:32-40`
  - the public configuration surface exposes `JsonSerializerSettings`, which ties consumers to Json.NET.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Client/CRUDOperation.cs:64-68`
  - create operations serialize directly with `JsonConvert.SerializeObject`.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Utility/HttpHelper.cs:247-305`
  - deserialization and error parsing are implemented directly with `JsonConvert`.

## Suggested outcome

- Introduce an internal serializer abstraction first.
- Keep Json.NET support for compatibility while evaluating multi-targeted `System.Text.Json` support on newer TFMs.
- Decouple the public configuration surface from Json.NET-specific types over time.

## Acceptance criteria

- Serialization and deserialization go through a library-owned abstraction.
- A migration plan exists for preserving current behavior where Json.NET quirks matter.
- Public docs explain serializer customization in library terms, not only Json.NET terms.

## Breaking change considerations

Removing Json.NET outright may be breaking; a staged migration is safer.
