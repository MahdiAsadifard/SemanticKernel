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

```text
SemanticKernel.slnx
├── Source/
│   ├── Api/                 ASP.NET Core Web API host, controllers, app startup, and HTTP-facing extensions
│   ├── Application/         Application use cases and service contracts, currently chat orchestration
│   ├── Infrastructure/      Semantic Kernel integrations, provider registration, options, and memory cache store
│   └── Domain/              Domain layer reserved for core business rules and shared abstractions
├── Tests/                   Solution folder exists; no test projects are currently included
├── Directory.Packages.props Central NuGet package versions
└── .github/copilot-instructions.md
```

Current project references:

```text
Api -> Application
Application -> Domain, Infrastructure
Infrastructure -> Domain
Domain -> no project references
```

### Layer responsibilities

- `Source\Api` owns HTTP endpoints and composition root setup. `Program.cs` wires CORS, options, logging, Semantic Kernel chat completion, application services, and controllers. Controllers should stay thin and delegate behavior to application services.
- `Source\Application` owns use-case orchestration and public application contracts. Chat behavior is exposed through `IChatService` and implemented by `ChatService`.
- `Source\Infrastructure` owns external-provider and framework integrations, including Semantic Kernel chat clients, Ollama/OpenAI registration extensions, strongly typed options, and `IMemoryCacheStore`/`MemoryCacheStore`.
- `Source\Domain` is the innermost project for core business rules. Keep it independent from API, Application, and Infrastructure concerns.
- Place new tests under `Tests\<Project>.Test` and add them to the `/Tests/` solution folder in `SemanticKernel.slnx`.

### HTTP surface

- `ChatController` exposes `POST /api/chat/streaming/{conversationId}` for streaming chat and `POST /api/chat/async/{conversationId}` for non-streaming chat responses.
- `HealthController` exposes `GET /api/health/sync` and `GET /api/health/stream`; keep the stream endpoint as `text/event-stream` and cancellation-aware.

## Repository conventions

- All projects target `net10.0`, enable nullable reference types, and enable implicit global usings.
- NuGet versions are managed centrally in the root `Directory.Packages.props`. Add or update package versions there and omit `Version` from individual `PackageReference` items. Existing `VersionOverride` elements are per-project exceptions.
- Keep source projects under `Source\<Layer>` and test projects under `Tests\<Project>.Test`; add new projects to the corresponding solution folder in `SemanticKernel.slnx`.
- Keep API controllers thin; put business flow in `Application` services and provider/framework details in `Infrastructure`.
- Register application services in `Source\Api\Extensions\ServicesExtension.cs`; register Semantic Kernel providers in `Source\Infrastructure\SemanticKernel\Extensions`.
- Infrastructure abstractions are colocated with their implementations and use matching `I...` interface names, such as `IMemoryCacheStore`/`MemoryCacheStore`.
- Validate public string keys and prompts with `ArgumentNullException.ThrowIfNull` or `ThrowIfNullOrWhiteSpace`, following the existing cache and Semantic Kernel adapters.
- Preserve cancellation tokens and asynchronous streaming for long-lived HTTP or Semantic Kernel streams. The health stream uses `text/event-stream`, flushes after each event, and stops when the request cancellation token is signaled.
- When adding test projects, provide `Xunit` through a project-level `<Using Include="Xunit" />`, so test files do not need an explicit `using Xunit;`.
