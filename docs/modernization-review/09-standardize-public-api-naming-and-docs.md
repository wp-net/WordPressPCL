# Standardize public API naming and documentation examples

## Summary

Normalize public API naming around current .NET conventions and ensure the docs match the actual surface area.

## Why this matters

- Consistent naming improves discoverability and makes the library feel more idiomatic to .NET consumers.
- The current codebase mixes `ID` and `Id` conventions.
- The README already advertises `GetByIdAsync`, while much of the actual API surface is still `GetByIDAsync`.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Interfaces/IReadOperation.cs:21-27`
  - the interface uses `GetByIDAsync(object ID, ...)`.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Client/CRUDOperation.cs:117-126`
  - the base implementation also uses `GetByIDAsync`.
- `/home/runner/work/WordPressPCL/WordPressPCL/README.md:67-74`
  - quickstart examples use `GetByIdAsync`.
- Repository search shows `GetByIDAsync` in implementation and tests, but `GetByIdAsync` in the README.

## Suggested outcome

- Pick one naming convention, ideally `Id`, and apply it consistently.
- Update docs and examples to match the supported API.
- If a rename is made, consider compatibility shims or a major-version boundary.

## Acceptance criteria

- Public API naming follows a documented convention consistently.
- README and docs compile conceptually against the actual API.
- Migration guidance exists if any public member is renamed.

## Breaking change considerations

Renaming public methods is breaking unless a compatibility layer is added.
