# SemanticKernel

SemanticKernel is a .NET 10 solution that hosts an ASP.NET Core Web API for chat interactions backed by Microsoft Semantic Kernel. The API exposes streaming and non-streaming chat endpoints, delegates chat orchestration to the application layer, and keeps provider/framework integrations in infrastructure.

## Project hierarchy

```text
SemanticKernel.slnx
├── Source/
│   ├── Api/                 ASP.NET Core Web API host, controllers, app startup, and HTTP-facing extensions
│   ├── Application/         Application use cases and service contracts, currently chat orchestration
│   ├── Infrastructure/      Semantic Kernel integrations, provider registration, options, and memory cache store
│   └── Domain/              Domain layer reserved for core business rules and shared abstractions
├── Tests/                   Solution folder exists; no test projects are currently included
├── Directory.Packages.props Central NuGet package versions
└── README.md
```

## Project references

Project references point inward toward business rules:

```text
Api -> Application
Application -> Domain, Infrastructure
Infrastructure -> Domain
Domain -> no project references
```

## Layer summary

- `Source/Api` contains the ASP.NET Core entry point, controllers, and service registration extensions.
- `Source/Application` contains application-level chat contracts and orchestration through `IChatService` and `ChatService`.
- `Source/Infrastructure` contains Semantic Kernel chat client integrations, provider registration extensions for Ollama/OpenAI, options, and memory caching.
- `Source/Domain` is reserved for core business rules and shared abstractions.

## HTTP endpoints

- `POST /api/chat/streaming/{conversationId}` streams chat responses.
- `POST /api/chat/async/{conversationId}` returns a non-streaming chat response.
- `GET /api/health/sync` returns a synchronous health response.
- `GET /api/health/stream` streams health messages as `text/event-stream`.

## Build and run

```powershell
dotnet restore SemanticKernel.slnx
dotnet build SemanticKernel.slnx
dotnet run --project Source\Api\Api.csproj
```
