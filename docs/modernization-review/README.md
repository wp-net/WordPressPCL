# WordPressPCL modernization review

This folder contains copy-pasteable GitHub issue drafts for modernizing the library.

## High-level code quality overview

### What is already working well

- The library has a clear client-oriented surface area centered around `WordPressClient`.
- Core REST operations are consistently async and already use `ConfigureAwait(false)`.
- The project includes integration-style test projects and GitHub Actions build coverage.
- The public API is documented extensively with XML comments and end-user docs.
- `HttpClient` injection is already supported, which is a good foundation for modernization.

### Current baseline observed in this review

- `dotnet restore /home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL.sln --disable-parallel` succeeds.
- `dotnet build /home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL.sln -c Release --no-restore` succeeds.
- The current build emits warnings, including:
  - analyzer package drift (`Microsoft.CodeAnalysis.NetAnalyzers` 8.0.0 vs newer SDK analyzers),
  - missing XML comments for newly added public surface in `Models/Subclasses.cs`,
  - missing XML comments in `Utility/UsersQueryBuilder.cs`.
- `dotnet test /home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL.Tests.Hosted/WordPressPCL.Tests.Hosted.csproj -c Release --no-restore` could not run in this environment because the test project targets `net6.0` and the required .NET 6 runtime is not installed here.

### Main modernization themes

1. Improve API ergonomics and safety.
2. Modernize HTTP client usage patterns.
3. Reduce coupling to legacy implementation choices.
4. Expose richer REST metadata and diagnostics.
5. Tighten the build quality baseline.

## Issue drafts

- [01 - Enable nullable reference types](./01-enable-nullable-reference-types.md)
- [02 - Add CancellationToken support to async APIs](./02-add-cancellationtoken-support.md)
- [03 - Modernize HttpClient lifecycle and DI integration](./03-modernize-httpclient-lifecycle-and-di.md)
- [04 - Add diagnostics and logging hooks](./04-add-diagnostics-and-logging-hooks.md)
- [05 - Reduce hard dependency on Newtonsoft.Json](./05-reduce-hard-dependency-on-newtonsoft-json.md)
- [06 - Expose pagination and response metadata](./06-expose-pagination-and-response-metadata.md)
- [07 - Clean up build warnings and analyzer drift](./07-clean-up-build-warnings-and-analyzer-drift.md)
- [08 - Revisit target frameworks for the next major version](./08-multi-target-modern-dotnet.md)
- [09 - Standardize public API naming and docs](./09-standardize-public-api-naming-and-docs.md)
