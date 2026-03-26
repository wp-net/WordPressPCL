# Clean up build warnings and align analyzer usage with the SDK

## Summary

Bring the build back to a clean baseline by removing avoidable warnings and deciding whether to keep or drop the explicit analyzer package.

## Why this matters

- A warning-clean build makes future regressions easier to spot.
- The current build already surfaces actionable quality issues.
- Analyzer drift between the package and the installed SDK adds noise to every build.

## Evidence

- `dotnet build /home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL.sln -c Release --no-restore`
  - succeeds with warnings.
- `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/WordPressPCL.csproj:147-151`
  - references `Microsoft.CodeAnalysis.NetAnalyzers` version `8.0.0`.
- Build output reports:
  - newer SDK analyzers are available than the package provides,
  - missing XML comments in `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Models/Subclasses.cs`,
  - missing XML comments in `/home/runner/work/WordPressPCL/WordPressPCL/WordPressPCL/Utility/UsersQueryBuilder.cs`.

## Suggested outcome

- Decide whether to rely on SDK analyzers or a pinned analyzer package.
- Fix or intentionally suppress existing warnings.
- Treat build cleanliness as part of the modernization baseline.

## Acceptance criteria

- The solution build is warning-clean, or suppressions are documented and justified.
- Analyzer configuration is intentional and current.
- Public API additions either include XML docs or are explicitly exempted.

## Breaking change considerations

Because this cleanup is part of a new major-version effort, analyzer rules and warning policy can be tightened deliberately as long as the resulting standards are documented.
