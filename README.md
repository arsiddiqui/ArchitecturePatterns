# ArchitecturePatternsDemo (.NET Web APIs)

This solution contains multiple small **ASP.NET Core Web API** projects, each demonstrating a common **architectural pattern** using the same simple "Order" domain.

## Prerequisites
- .NET SDK 8.0+
- Visual Studio 2022 / Rider / VS Code

## How to run
From the repo root:

```bash
dotnet restore
dotnet run --project src/patterns/LayeredDemo/LayeredDemo.Api
```

Then open Swagger at:
- `https://localhost:xxxx/swagger` (port printed in console)

## Included demos

### Layered (N-Tier)
- `LayeredDemo.Api` orchestrates
- `LayeredDemo.Application` (services)
- `LayeredDemo.Domain` (entities)
- `LayeredDemo.Infrastructure` (in-memory repo)

### Clean Architecture
- `CleanDemo.Domain` (entities)
- `CleanDemo.Application` (use cases + abstractions)
- `CleanDemo.Infrastructure` (implements abstractions)
- `CleanDemo.Api` (delivery)

### Hexagonal (Ports & Adapters)
- `HexDemo.Core` defines ports + use cases
- `HexDemo.Adapters.Persistence.InMemory` implements ports
- `HexDemo.Adapters.Api` is an adapter exposing HTTP

### CQRS
- `CqrsDemo.Api` exposes `/commands/*` and `/queries/*`
- Separate command handlers vs query handlers + separate read/write stores

### Event Sourcing
- `EventSourcingDemo.Api` stores events and rebuilds aggregate state by replay

### Microkernel (Plugin Architecture)
- `MicrokernelDemo.Host.Api` loads pricing plugins via DI and applies them in order

### Pipe-and-Filter
- `PipeFilterDemo.Api` runs a request through a pipeline of filters

> Tip: Run any demo by changing the `--project` path.

## Notes
This repo is intentionally lightweight (in-memory persistence) so each pattern’s structure is easy to see.
