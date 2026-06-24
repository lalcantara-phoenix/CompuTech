# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```powershell
# Build
dotnet build CompuTech.slnx

# Run all tests
dotnet test tests/CompuTech.Application.Tests/CompuTech.Application.Tests.csproj

# Run a single test class
dotnet test tests/CompuTech.Application.Tests/CompuTech.Application.Tests.csproj --filter "FullyQualifiedName~CompleteServiceOrderCommandHandlerTests"

# Run a single test method
dotnet test tests/CompuTech.Application.Tests/CompuTech.Application.Tests.csproj --filter "FullyQualifiedName~Rule8_PreventiveOrder_CallsGenerateNextMaintenanceOrder"

# Run the API (port 5254)
dotnet run --project src/CompuTech.API/CompuTech.API.csproj

# Add EF migration
dotnet ef migrations add <MigrationName> --project src/CompuTech.Infrastructure/CompuTech.Infrastructure.csproj --startup-project src/CompuTech.API/CompuTech.API.csproj

# Apply migrations
dotnet ef database update --project src/CompuTech.Infrastructure/CompuTech.Infrastructure.csproj --startup-project src/CompuTech.API/CompuTech.API.csproj

# Drop database (for full reset)
dotnet ef database drop --project src/CompuTech.Infrastructure/CompuTech.Infrastructure.csproj --startup-project src/CompuTech.API/CompuTech.API.csproj --force

# Seed dummy data (requires API running)
.\seed-data.ps1
```

Swagger UI: `http://localhost:5254/swagger`  
DB: SQL Server LocalDB — `(localdb)\mssqllocaldb`, database `CompuTechDb`

---

## Architecture

Clean Architecture + CQRS. Dependency direction: `API → Application → Domain`; `Infrastructure → Application`.

```
src/
  CompuTech.Domain/          # Entities, enums, repository interfaces (no dependencies)
  CompuTech.Application/     # CQRS handlers, DTOs, validators, one domain service
  CompuTech.Infrastructure/  # EF Core, repositories, migrations
  CompuTech.API/             # Controllers, request models, Program.cs
tests/
  CompuTech.Application.Tests/  # Unit tests (xUnit + Moq + FluentAssertions)
```

**Solution file:** `CompuTech.slnx` (not `.sln`)

### Application layer conventions

Every use case lives in its own folder:  
`Application/<Module>/Commands/<CommandName>/` → `Command.cs`, `CommandHandler.cs`, `CommandValidator.cs`  
`Application/<Module>/Queries/<QueryName>/` → `Query.cs`, `QueryHandler.cs`

- Commands returning an ID implement `IRequest<int>`; state-change commands implement `IRequest` (Unit).
- Handlers throw `KeyNotFoundException` when an entity is not found.
- Handlers throw `InvalidOperationException` for business rule violations (wrong state, duplicate active schedule, etc.).
- `ValidationBehavior<TRequest, TResponse>` runs all FluentValidation validators via `ValidateAsync` (async, parallel). **Never call `Validate()` synchronously** — many validators use `MustAsync` for DB uniqueness checks.

### Domain entities

All entities have `private set` properties and `private` parameterless constructors. Mutation happens through factory methods (`Entity.Create(...)`) and behavior methods (`order.Start()`, `order.Complete(...)`, etc.). Entities are never constructed with `new`.

`ServiceOrder` has a private backing collection `private List<ServiceOrderItem> _items` exposed as `IReadOnlyList<ServiceOrderItem> Items`. When including it in EF Core queries, use the lambda form `.Include(x => x.Items)`, **not** `.Include("_items")` — EF Core 10 resolves the navigation by property name, not field name.

### Infrastructure

`CompuTechDbContext` loads all `IEntityTypeConfiguration<T>` implementations automatically via `ApplyConfigurationsFromAssembly`. Add a new configuration class and it is picked up without modifying `DbContext`.

Enums are stored as `int` via `.HasConversion<int>()` in every configuration.

`MaintenanceScheduleConfiguration` enforces a unique index on `EquipmentId` (one active schedule per equipment).

`ServiceOrderConfiguration` registers `Items` with:
```csharp
builder.Navigation(x => x.Items)
    .UsePropertyAccessMode(PropertyAccessMode.Field)
    .HasField("_items");
```

### Key business rules encoded in handlers

| Rule | Location |
|---|---|
| #1 — One active MaintenanceSchedule per equipment | `CreateMaintenanceScheduleCommandHandler` |
| #3/#4 — Part items require InventoryItemId; Labor items must not have one | `AddServiceOrderItemCommandValidator` |
| #5 — SubType must be valid for Type (Repair↔Hardware/Software/Diagnostic; Maintenance↔Preventive/OnDemand) | `CreateServiceOrderCommandValidator` |
| #6 — Subtotal = Quantity × UnitPrice; Part items decrement stock | `AddServiceOrderItemCommandHandler` |
| #8 — Completing a Preventive order triggers auto-generation of next order | `CompleteServiceOrderCommandHandler` → `IGenerateNextMaintenanceOrderService` |

### API controllers

Route names must be globally unique across all controllers. Use explicit string names:  
`[HttpGet("{id:int}", Name = "GetCustomerById")]` — never `Name = nameof(GetById)` across multiple controllers (causes startup `InvalidOperationException`).

`POST` endpoints that need to return `Location` to a route defined in a **different** controller class must use `CreatedAtRoute("RouteName", ...)` with the route's `Name` attribute string, not `CreatedAtAction(nameof(Method), ...)`.

### Tests

Test files under namespace `CompuTech.Application.Tests.*` share the `CompuTech.Application` root with the Application layer. This causes the compiler to resolve `Equipment`, `MaintenanceSchedule`, etc. as the **namespace** `CompuTech.Application.Equipment` rather than the entity type. Fix with a type alias at the top of the test file:

```csharp
using EquipmentEntity = CompuTech.Domain.Entities.Equipment;
using MaintenanceScheduleEntity = CompuTech.Domain.Entities.MaintenanceSchedule;
```

Tests use Moq for mocking repositories and FluentAssertions for assertions. No in-memory database — handlers are tested with mocked repository interfaces.

---

## Git workflow

Two-phase GitFlow:

```
main        ← production (default branch on GitHub)
 └── testing ← integration
       └── feature/*  ← one branch per issue
```

**Phase 1 (per issue):** `feature/* → testing` via squash-merge PR  
**Phase 2 (per module):** `testing → main` via squash-merge PR with `Closes #N` in body

Before creating a `testing → main` PR, run:
```powershell
git merge origin/main --strategy-option=ours --no-edit
git push origin testing
```
This avoids divergence conflicts from previous squash-merges.

---

## Packages

| Package | Version | Project |
|---|---|---|
| MediatR | 14.1.0 | Application |
| FluentValidation | 12.1.1 | Application |
| EF Core (SqlServer) | 10.0.9 | Infrastructure |
| Swashbuckle.AspNetCore | 10.2.3 | API |
| xUnit | 2.9.3 | Tests |
| Moq | 4.20.72 | Tests |
| FluentAssertions | 8.10.0 | Tests |
