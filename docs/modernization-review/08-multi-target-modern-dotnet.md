# Multi-target modern .NET while preserving compatibility

## Summary

Keep broad compatibility, but add a modern target framework so the library can light up newer APIs and tooling.

## Why this matters

- The library currently targets only `netstandard2.0`, which maximizes reach but limits access to newer platform features.
- Adding a modern TFM can unlock better serialization, diagnostics, analyzer defaults, and performance improvements without dropping compatibility immediately.
- The test projects already target `net6.0`, and the CI workflow uses the .NET 8 SDK.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/WordPressPCL.csproj:5-20`
  - the package targets only `netstandard2.0`.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL.Tests.Hosted/WordPressPCL.Tests.Hosted.csproj:3-7`
  - hosted tests target `net6.0`.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL.Tests.Selfhosted/WordPressPCL.Tests.Selfhosted.csproj:3-5`
  - self-hosted tests target `net6.0`.
- `/home/runner/work/WordPressPCL/WordPressPCL/.github/workflows/integration-tests.yml:25-32`
  - CI installs the .NET 8 SDK for restore and build.

## Suggested outcome

- Evaluate multi-targeting, such as `netstandard2.0` plus a current .NET target.
- Use the modern target to adopt better platform features incrementally.
- Keep compatibility-focused behavior on the `netstandard2.0` target until a major-version decision is made.

## Acceptance criteria

- The project multi-targets with clear compatibility expectations.
- CI builds and tests all supported TFMs intentionally.
- Documentation explains consumer guidance for older versus newer runtimes.

## Breaking change considerations

This can be additive if `netstandard2.0` remains supported.
