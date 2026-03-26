# Restructure the repository and GitHub-facing documentation

## Summary

Reorganize the repository layout and GitHub-facing documentation so the project is easier to understand, navigate, and contribute to.

## Why this matters

- The repository is the first thing contributors and consumers see, so unclear layout and outdated landing documentation create friction before anyone even reaches the library code.
- Modernizing the package for `.NET 10` is a good point to also modernize the repo structure, README content, and contributor-facing documentation.
- A cleaner layout reduces maintenance overhead and makes future docs, CI, and release work easier to reason about.

## Evidence

- `/home/runner/work/WordPressPCL/WordPressPCL/README.md:2`
  - the status badge still references the `master` branch.
- `/home/runner/work/WordPressPCL/WordPressPCL/README.md:35-48`
  - the supported-platform matrix is centered on older `.NET Standard` compatibility instead of the new major-version direction.
- `/home/runner/work/WordPressPCL/WordPressPCL/docs/I version 2.x`
  - documentation content is stored under unconventional folder names that are not immediately self-explanatory.
- `/home/runner/work/WordPressPCL/WordPressPCL/docs/II version 1.x`
  - versioned docs are present, but the directory naming does not follow a typical docs information architecture.
- `/home/runner/work/WordPressPCL/WordPressPCL/mkdocs.yml:1-3`
  - the docs site configuration is minimal and does not currently define a clearer navigation structure.

## Suggested outcome

- Rewrite the top-level `README.md` so it reflects the current project direction, support matrix, quickstart, and contribution entry points.
- Reorganize documentation folders into clearer names and groupings, for example by product area, version, or migration track.
- Add or refresh contributor-facing repo documents such as `CONTRIBUTING.md`, issue templates, or other GitHub metadata where helpful.
- Make the docs site navigation and repository layout line up so users can move between GitHub and published docs without guesswork.

## Acceptance criteria

- The top-level README accurately describes the project, current support targets, and common getting-started paths.
- Documentation folders use a clear naming scheme and are organized intentionally.
- Contributor-facing guidance and GitHub metadata are easy to find from the repository root.
- The repository structure is documented well enough that a new contributor can quickly find source, tests, docs, and development setup instructions.

## Breaking change considerations

This work is mostly organizational, but any renamed documentation paths or contributor workflows should be redirected or documented so existing links do not fail silently.
