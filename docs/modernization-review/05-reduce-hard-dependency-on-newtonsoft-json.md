# Migrate serialization to System.Text.Json

## Summary

Move the library off `Newtonsoft.Json` and standardize on `System.Text.Json` for the next major version.

## Why this matters

- The library is tightly coupled to `Newtonsoft.Json` in both API configuration and implementation.
- `System.Text.Json` is the default JSON stack for current .NET and aligns with the stated modernization direction for this project.
- Removing Json.NET-specific public types will simplify the API surface and reduce dependency baggage.

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

- Replace Json.NET-based serialization and deserialization paths with `System.Text.Json`.
- Remove `JsonSerializerSettings` and other Json.NET-specific types from the public configuration surface.
- Add any required custom converters or naming policies inside the library so WordPress payload handling remains correct.

## Acceptance criteria

- Runtime serialization and deserialization use `System.Text.Json`.
- The public API no longer exposes Json.NET-specific configuration types.
- Public docs explain any supported serializer customization and call out behavior differences that matter for migration.

## Breaking change considerations

Because this is planned for a new major version, the library can migrate directly to `System.Text.Json` as long as any observable behavior changes are documented clearly.
