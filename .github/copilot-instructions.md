# Repository instructions

## Build and test

- This solution requires the .NET 10 SDK and uses the XML solution format `SemanticKernel.slnx`.
- Restore dependencies: `dotnet restore SemanticKernel.slnx`
- Build the full solution: `dotnet build SemanticKernel.slnx`
- Run all tests: `dotnet test SemanticKernel.slnx`
- Run the API: `dotnet run --project Source\Api\Api.csproj`
- Run one test by fully qualified name:
  `dotnet test Tests\Api.Test\Api.Test.csproj --filter "FullyQualifiedName=Api.Test.UnitTest1.Test1"`
- Run all tests in a class by changing the filter to:
  `dotnet test Tests\Api.Test\Api.Test.csproj --filter "FullyQualifiedName~Api.Test.UnitTest1"`

There is no repository-specific lint or formatting command configured.

## Architecture

### Project hierarchy

Project references must point inward toward the business rules:

## Repository conventions

- All projects target `net10.0`, enable nullable reference types, and enable implicit global usings.
- NuGet versions are managed centrally in the root `Directory.Packages.props`. Add or update package versions there and omit `Version` from individual `PackageReference` items. Existing `VersionOverride` elements are per-project exceptions.
- Keep source projects under `Source\<Layer>` and test projects under `Tests\<Project>.Test`; add new projects to the corresponding solution folder in `SemanticKernel.slnx`.
- Infrastructure abstractions are colocated with their implementations and use matching `I...` interface names, such as `IMemoryCacheStore`/`MemoryCacheStore`.
- Validate public string keys and prompts with `ArgumentNullException.ThrowIfNull` or `ThrowIfNullOrWhiteSpace`, following the existing cache and Semantic Kernel adapters.
- Preserve cancellation tokens and asynchronous streaming for long-lived HTTP or Semantic Kernel streams. The health stream uses `text/event-stream`, flushes after each event, and stops when the request cancellation token is signaled.
- Test projects provide `Xunit` through a project-level `<Using Include="Xunit" />`, so test files do not need an explicit `using Xunit;`.
