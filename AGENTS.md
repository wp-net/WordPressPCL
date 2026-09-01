# WordPressPCL Agent Guidance

## Development

- Use the .NET 10 SDK. Restore and build the solution in Release configuration:

  ```bash
  dotnet restore WordPressPCL.sln --disable-parallel
  dotnet build WordPressPCL.sln -c Release --no-restore
  ```

- Follow `.editorconfig`; nullable reference types are enabled throughout the solution, so do not suppress nullable warnings without clear justification.
- Manage dependency versions centrally in `Directory.Packages.props`. Do not add versions to individual `PackageReference` items.

## Tests and Documentation

- Add or update tests for every behavior change. For most changes, run the self-hosted integration tests against the Docker WordPress environment described in `dev/install.md`.
- Treat hosted tests as CI smoke tests: they require external content and credentials and are not required for local development.
- Update the version 3 documentation under `docs/v3/` for public API or behavior changes. Document migration requirements in `docs/v3/breaking-changes.md` for breaking changes, and add new pages to `mkdocs.yml`.
- Do not manually publish documentation. The Docs workflow deploys it after a merge to `main`.

## Pull Requests

- Target `main`, reference the related issue in the PR description, and use the repository's existing PR conventions.
- Update `CHANGELOG.md` for every user-visible change. Add entries under `Unreleased` in the appropriate `Added`, `Changed`, or `Fixed` section.
- Use PR labels that match the GitHub release-note categories: `breaking-change`, `enhancement` or `feature`, `bug` or `bugfix`, and `chore`, `dependencies`, or `documentation`. Apply `skip-changelog` only when the change should not appear in generated release notes.
- Treat breaking changes as a major-version concern and document their migration impact in the changelog and PR description.
- Do not change `VersionPrefix` or publish a NuGet package as part of a normal PR. Package versions are determined by the published GitHub Release tag.

## Releases

After the release PR is merged, update `CHANGELOG.md` so the released version has its finalized notes, then create and publish a GitHub Release from the intended commit:

- Use a stable tag in the form `v<major>.<minor>.<patch>`, such as `v3.0.1`.
- Use `v<major>.<minor>.<patch>-<suffix>` for prereleases, such as `v3.1.0-rc.1`, and mark the GitHub Release as a prerelease when appropriate.
- Generate GitHub release notes; `.github/release.yml` groups them by the PR labels above.

Publishing the GitHub Release triggers `.github/workflows/publish-nuget.yml`. The workflow validates the tag, strips the leading `v`, builds and packs with that exact version, then pushes the package to nuget.org. The GitHub Release is the source of truth for the published NuGet version.
