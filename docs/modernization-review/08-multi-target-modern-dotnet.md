# Revisit target frameworks for the next major version

## Summary

Use the next major version to choose target frameworks intentionally and light up newer APIs and tooling.

## Why this matters

- The library currently targets only `netstandard2.0`, which maximizes reach but limits access to newer platform features.
- The next major version is a chance to re-evaluate whether `netstandard2.0` should remain, or whether the package should move fully to newer supported .NET targets.
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

- Decide whether the next major version should multi-target or drop older frameworks entirely.
- Use the chosen target framework strategy to adopt better platform features more aggressively.
- Update CI, packaging, and docs to reflect the new support matrix clearly.

## Acceptance criteria

- The project targets an explicit, documented support matrix appropriate for the next major version.
- CI builds and tests all supported TFMs intentionally.
- Documentation explains the new runtime requirements and migration expectations.

## Breaking change considerations

Because this is explicitly planned for a new major version, changing target framework support is acceptable if the migration impact is documented clearly.
