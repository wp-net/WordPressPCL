# Target .NET 10 in the next major version

## Summary

Move the library and its test projects to .NET 10 in the next major version so the codebase can use current platform APIs and tooling consistently.

## Why this matters

- The library currently targets only `netstandard2.0`, which limits access to newer platform features and keeps the package tied to older compatibility trade-offs.
- The test projects already target `net6.0`, and the CI workflow uses the .NET 8 SDK, so the repository is already carrying multiple generations of .NET decisions.
- Moving to .NET 10 aligns the support matrix with the project's stated modernization goal and makes it easier to adopt current BCL, diagnostics, and serialization features.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/WordPressPCL.csproj:5-20`
  - the package targets only `netstandard2.0`.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL.Tests.Hosted/WordPressPCL.Tests.Hosted.csproj:3-7`
  - hosted tests target `net6.0`.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL.Tests.Selfhosted/WordPressPCL.Tests.Selfhosted.csproj:3-5`
  - self-hosted tests target `net6.0`.
- `/home/runner/work/WordPressPCL/WordPressPCL/.github/workflows/integration-tests.yml:25-32`
  - CI currently installs the .NET 8 SDK for restore and build.

## Suggested outcome

- Retarget the library and test projects to `.NET 10`.
- Update CI to install the .NET 10 SDK and execute restore, build, and tests on that toolchain.
- Refresh packaging and docs to state the new runtime requirement clearly.

## Acceptance criteria

- The main library targets `.NET 10`.
- CI builds and tests the solution with the .NET 10 SDK.
- Documentation explains the new runtime requirement and migration expectations.

## Breaking change considerations

Because this is explicitly planned for a new major version, dropping older target frameworks in favor of `.NET 10` is acceptable if the migration impact is documented clearly.
