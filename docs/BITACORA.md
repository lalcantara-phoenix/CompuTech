# Bitácora de Desarrollo — CompuTech API

Registro cronológico de todas las actividades realizadas durante el desarrollo.

---

## 2026-06-23

### Inicio del proyecto
- Revisión y análisis de la especificación del proyecto (`CompuTech-ProjectSpec.md`).
- Creación de la carpeta `/docs` para documentación y evidencia del desarrollo.
- Definición de la estructura de carpetas de documentación.

**Artefactos generados:**
- `docs/README.md` — índice de la documentación
- `docs/BITACORA.md` — este archivo
- `docs/01-especificacion/especificacion.md` — copia estructurada del spec
- `docs/02-arquitectura/arquitectura.md` — descripción de la arquitectura

---

## 2026-06-23 (continuación)

### Configuración de agentes y skills de Claude Code

Se crearon los agentes y skills del proyecto en `.claude/` para acelerar el desarrollo.

**Agentes creados (`.claude/agents/`):**
- `dotnet-clean-arch` — Especialista en .NET Clean Architecture + CQRS. Conoce todas las convenciones, plantillas y reglas de negocio del proyecto.
- `dotnet-unit-test` — Especialista en xUnit + Moq + FluentAssertions. Genera tests para handlers, validators y reglas de negocio.

**Skills creadas (`.claude/skills/`):**
- `/scaffold-entity [Entidad]` — Entidad DDD + enums + interfaz de repositorio
- `/add-migration [Descripcion]` — Migración EF Core con validación automática
- `/add-cqrs-handler command|query [Nombre]` — Command o Query individual con handler + validator
- `/api-endpoint [Modulo]` — Controller REST completo con todos los endpoints
- `/implement-module [Modulo]` — Implementación completa Domain → Application → Infrastructure → API
- `/design-tests [Modulo]` — Suite de pruebas unitarias del módulo

**Artefactos generados:**
- `.claude/agents/dotnet-clean-arch.md`
- `.claude/agents/dotnet-unit-test.md`
- `.claude/skills/scaffold-entity/SKILL.md`
- `.claude/skills/add-migration/SKILL.md`
- `.claude/skills/add-cqrs-handler/SKILL.md`
- `.claude/skills/api-endpoint/SKILL.md`
- `.claude/skills/implement-module/SKILL.md`
- `.claude/skills/design-tests/SKILL.md`

---

## 2026-06-23 (continuación)

### Integración GitHub MCP

Se configuró el servidor MCP de GitHub para el proyecto.

**Archivos creados:**
- `.mcp.json` — define el servidor `github` usando `@modelcontextprotocol/server-github` via `npx`
- `.claude/settings.json` — habilita automáticamente todos los servidores MCP del proyecto (`enableAllProjectMcpServers: true`)

**Prerequisito pendiente:** definir la variable de entorno `GITHUB_TOKEN` con un Personal Access Token de GitHub (ver instrucciones abajo).

---

---

## 2026-06-23 (continuación)

### Configuración de Hooks de Claude Code

Se configuraron 4 hooks en `.claude/settings.json`:
- **Hook 1 (PostToolUse Write|Edit):** Compila automáticamente el proyecto tras guardar un `.cs`
- **Hook 2 (PostToolUse Write|Edit):** Registra en `BITACORA.md` cada archivo de las capas Domain/Application/Infrastructure/API modificado
- **Hook 3 (PostToolUse Bash):** Registra en `docs/04-migraciones/ef-log.txt` cada comando `dotnet ef` ejecutado
- **Hook 4 (Stop):** Al cerrar la sesión, lista todos los archivos `.cs/.json/.md` modificados en las últimas 8h y los agrega a `BITACORA.md`

---

## 2026-06-23 (continuación)

### GitFlow + Gestión de Backlog en GitHub

Se estableció el flujo GitFlow y se preparó la infraestructura de gestión de tareas.

**GitFlow:**
- Rama `testing` creada en GitHub desde `main`
- Flujo: `feature/*` → `testing` (PR requerido) → `main` (PR tras QA)
- Convención de ramas: `feature/[titulo-del-issue-en-kebab-case]`

**Milestones (entidad-first):**
Setup → Customers → Equipment → Technicians → Inventory → ServiceOrders → MaintenanceSchedule

**Agente creado:**
- `github-project-manager` — analiza el spec y crea ~38 issues con body técnico, labels, milestone y rama asociada. Organización: Setup primero, luego todas las capas de cada entidad antes de pasar a la siguiente.

**Skill creada:**
- `/load-issues` — orquesta la carga completa del backlog: milestones → labels → ~38 issues via agente

**Archivos generados:**
- `docs/GITFLOW.md` — convenciones GitFlow documentadas
- `.github/PULL_REQUEST_TEMPLATE.md` — template de PR con checklist
- `.claude/agents/github-project-manager.md` — agente gestor de backlog GitHub
- `.claude/skills/load-issues/SKILL.md` — skill de carga masiva de issues

---

## Fin de sesión — 2026-06-23 20:35 (7 archivos modificados)
- C:\Phoenix Projects\CompuTech\.mcp.json
- C:\Phoenix Projects\CompuTech\CompuTech-ProjectSpec.md
- C:\Phoenix Projects\CompuTech\README.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\README.md
- C:\Phoenix Projects\CompuTech\docs\01-especificacion\especificacion.md
- C:\Phoenix Projects\CompuTech\docs\02-arquitectura\arquitectura.md

## Fin de sesión — 2026-06-23 21:19 (9 archivos modificados)
- C:\Phoenix Projects\CompuTech\.mcp.json
- C:\Phoenix Projects\CompuTech\CompuTech-ProjectSpec.md
- C:\Phoenix Projects\CompuTech\README.md
- C:\Phoenix Projects\CompuTech\.github\PULL_REQUEST_TEMPLATE.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\GITFLOW.md
- C:\Phoenix Projects\CompuTech\docs\README.md
- C:\Phoenix Projects\CompuTech\docs\01-especificacion\especificacion.md
- C:\Phoenix Projects\CompuTech\docs\02-arquitectura\arquitectura.md

## Fin de sesión — 2026-06-23 22:22 (9 archivos modificados)
- C:\Phoenix Projects\CompuTech\.mcp.json
- C:\Phoenix Projects\CompuTech\CompuTech-ProjectSpec.md
- C:\Phoenix Projects\CompuTech\README.md
- C:\Phoenix Projects\CompuTech\.github\PULL_REQUEST_TEMPLATE.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\GITFLOW.md
- C:\Phoenix Projects\CompuTech\docs\README.md
- C:\Phoenix Projects\CompuTech\docs\01-especificacion\especificacion.md
- C:\Phoenix Projects\CompuTech\docs\02-arquitectura\arquitectura.md

## Fin de sesión — 2026-06-23 22:28 (9 archivos modificados)
- C:\Phoenix Projects\CompuTech\.mcp.json
- C:\Phoenix Projects\CompuTech\CompuTech-ProjectSpec.md
- C:\Phoenix Projects\CompuTech\README.md
- C:\Phoenix Projects\CompuTech\.github\PULL_REQUEST_TEMPLATE.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\GITFLOW.md
- C:\Phoenix Projects\CompuTech\docs\README.md
- C:\Phoenix Projects\CompuTech\docs\01-especificacion\especificacion.md
- C:\Phoenix Projects\CompuTech\docs\02-arquitectura\arquitectura.md

- [2026-06-23 22:31] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Customer.cs

- [2026-06-23 22:31] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\CustomerLocation.cs

- [2026-06-23 22:31] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerRepository.cs

- [2026-06-23 22:31] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerLocationRepository.cs

- [2026-06-23 22:33] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Domain\CompuTech.Domain.csproj

- [2026-06-23 22:33] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\CompuTech.Application.csproj

- [2026-06-23 22:33] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\CompuTech.Infrastructure.csproj

- [2026-06-23 22:33] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\CompuTech.API.csproj

- [2026-06-23 22:33] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\CompuTech.Application.Tests.csproj

- [2026-06-23 22:34] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Program.cs

- [2026-06-23 22:36] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs

- [2026-06-23 22:36] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs

- [2026-06-23 22:36] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs

- [2026-06-23 22:36] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs

- [2026-06-23 22:36] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.json

- [2026-06-23 22:36] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.Development.json

- [2026-06-23 22:36] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\UnitTest1.cs

- [2026-06-23 22:37] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Program.cs

- [2026-06-23 22:37] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Program.cs

- [2026-06-23 22:37] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\UnitTest1.cs

- [2026-06-23 23:30] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerLocationDto.cs

- [2026-06-23 23:30] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerDto.cs

- [2026-06-23 23:30] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQuery.cs

- [2026-06-23 23:30] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQueryHandler.cs

- [2026-06-23 23:30] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQuery.cs

- [2026-06-23 23:30] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQueryHandler.cs

- [2026-06-23 23:30] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQuery.cs

- [2026-06-23 23:30] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQueryHandler.cs

## Fin de sesión — 2026-06-23 23:32 (46 archivos modificados)
- C:\Phoenix Projects\CompuTech\.mcp.json
- C:\Phoenix Projects\CompuTech\CompuTech-ProjectSpec.md
- C:\Phoenix Projects\CompuTech\README.md
- C:\Phoenix Projects\CompuTech\.github\PULL_REQUEST_TEMPLATE.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\GITFLOW.md
- C:\Phoenix Projects\CompuTech\docs\README.md
- C:\Phoenix Projects\CompuTech\docs\01-especificacion\especificacion.md
- C:\Phoenix Projects\CompuTech\docs\02-arquitectura\arquitectura.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.Development.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Program.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Properties\launchSettings.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomer\DeactivateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomer\DeactivateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomerLocation\DeactivateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomerLocation\DeactivateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerLocationDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Customer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\CustomerLocation.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerLocationRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\UnitTest1.cs

- [2026-06-23 23:36] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\CustomerConfiguration.cs

- [2026-06-23 23:36] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\CustomerLocationConfiguration.cs

- [2026-06-23 23:36] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\CustomerRepository.cs

- [2026-06-23 23:37] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\CustomerLocationRepository.cs

- [2026-06-23 23:37] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs

- [2026-06-23 23:37] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs

- [2026-06-23 23:40] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Customers\CustomerRequests.cs

- [2026-06-23 23:40] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs

- [2026-06-23 23:41] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\CompuTech.API.csproj

- [2026-06-23 23:41] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\CompuTech.API.csproj

## Fin de sesión — 2026-06-24 00:28 (53 archivos modificados)
- C:\Phoenix Projects\CompuTech\.mcp.json
- C:\Phoenix Projects\CompuTech\CompuTech-ProjectSpec.md
- C:\Phoenix Projects\CompuTech\README.md
- C:\Phoenix Projects\CompuTech\.github\PULL_REQUEST_TEMPLATE.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\GITFLOW.md
- C:\Phoenix Projects\CompuTech\docs\README.md
- C:\Phoenix Projects\CompuTech\docs\01-especificacion\especificacion.md
- C:\Phoenix Projects\CompuTech\docs\02-arquitectura\arquitectura.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.Development.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Program.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Properties\launchSettings.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomer\DeactivateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomer\DeactivateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomerLocation\DeactivateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomerLocation\DeactivateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerLocationDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Customer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\CustomerLocation.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerLocationRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624033807_AddCustomerAndLocation.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624033807_AddCustomerAndLocation.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\CustomerConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\CustomerLocationConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\CustomerLocationRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\CustomerRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\UnitTest1.cs

- [2026-06-24 00:36] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs

## Fin de sesión — 2026-06-24 05:24 (55 archivos modificados)
- C:\Phoenix Projects\CompuTech\.mcp.json
- C:\Phoenix Projects\CompuTech\CompuTech-ProjectSpec.md
- C:\Phoenix Projects\CompuTech\.github\PULL_REQUEST_TEMPLATE.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\GITFLOW.md
- C:\Phoenix Projects\CompuTech\docs\README.md
- C:\Phoenix Projects\CompuTech\docs\01-especificacion\especificacion.md
- C:\Phoenix Projects\CompuTech\docs\02-arquitectura\arquitectura.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.Development.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Program.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Customers\CustomerRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Properties\launchSettings.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomer\DeactivateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomer\DeactivateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomerLocation\DeactivateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomerLocation\DeactivateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerLocationDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Customer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\CustomerLocation.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerLocationRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624033807_AddCustomerAndLocation.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624033807_AddCustomerAndLocation.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\CustomerConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\CustomerLocationConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\CustomerLocationRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\CustomerRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\UnitTest1.cs

- [2026-06-24 05:25] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs

- [2026-06-24 05:25] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs

- [2026-06-24 05:27] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs

- [2026-06-24 05:27] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs

- [2026-06-24 05:27] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs

- [2026-06-24 05:27] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs

- [2026-06-24 05:28] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs

- [2026-06-24 05:28] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs

- [2026-06-24 05:28] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs

- [2026-06-24 05:28] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs

- [2026-06-24 05:28] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs

- [2026-06-24 05:28] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs

- [2026-06-24 05:29] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs

- [2026-06-24 05:29] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs

- [2026-06-24 05:30] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs

- [2026-06-24 05:30] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs

- [2026-06-24 05:30] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs

- [2026-06-24 05:30] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs

- [2026-06-24 05:30] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs

- [2026-06-24 05:30] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs

- [2026-06-24 05:30] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs

- [2026-06-24 05:31] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs

- [2026-06-24 05:31] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs

- [2026-06-24 05:32] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs

- [2026-06-24 05:32] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs

- [2026-06-24 05:33] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs

- [2026-06-24 05:34] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs

- [2026-06-24 05:36] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs

- [2026-06-24 05:36] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs

- [2026-06-24 05:38] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs

- [2026-06-24 05:39] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs

- [2026-06-24 05:40] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs

- [2026-06-24 05:40] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs

- [2026-06-24 05:40] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs

- [2026-06-24 05:40] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs

- [2026-06-24 05:42] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs

- [2026-06-24 05:42] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs

- [2026-06-24 05:44] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs

- [2026-06-24 05:44] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs

- [2026-06-24 05:45] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs

- [2026-06-24 05:45] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs

- [2026-06-24 05:46] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs

- [2026-06-24 05:46] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs

- [2026-06-24 05:46] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs

- [2026-06-24 05:47] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs

- [2026-06-24 05:49] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs

- [2026-06-24 05:49] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs

- [2026-06-24 05:49] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs

- [2026-06-24 05:49] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs

- [2026-06-24 05:49] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs

- [2026-06-24 05:49] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs

- [2026-06-24 05:49] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs

- [2026-06-24 05:51] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs

- [2026-06-24 05:51] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs

- [2026-06-24 05:51] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs

- [2026-06-24 05:51] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs

- [2026-06-24 05:53] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs

- [2026-06-24 05:53] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs

- [2026-06-24 05:57] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs

- [2026-06-24 05:57] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs

- [2026-06-24 05:57] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs

- [2026-06-24 05:57] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs

- [2026-06-24 05:57] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs

- [2026-06-24 05:58] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs

- [2026-06-24 05:58] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs

- [2026-06-24 05:58] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs

- [2026-06-24 05:58] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs

- [2026-06-24 06:00] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs

- [2026-06-24 06:00] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs

- [2026-06-24 06:00] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs

- [2026-06-24 06:02] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs

- [2026-06-24 06:04] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs

- [2026-06-24 06:04] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs

- [2026-06-24 06:04] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs

- [2026-06-24 06:04] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs

- [2026-06-24 06:04] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs

- [2026-06-24 06:06] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs

- [2026-06-24 06:06] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs

- [2026-06-24 06:06] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs

- [2026-06-24 06:06] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs

- [2026-06-24 06:06] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs

- [2026-06-24 06:06] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs

- [2026-06-24 06:07] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs

- [2026-06-24 06:10] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs

- [2026-06-24 06:10] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs

- [2026-06-24 06:10] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs

- [2026-06-24 06:10] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs

- [2026-06-24 06:10] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs

- [2026-06-24 06:10] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs

- [2026-06-24 06:13] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs

- [2026-06-24 06:13] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs

- [2026-06-24 06:16] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs

- [2026-06-24 06:16] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs

- [2026-06-24 06:16] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs

- [2026-06-24 06:18] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs

- [2026-06-24 06:18] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs

- [2026-06-24 06:18] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs

- [2026-06-24 06:18] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs

- [2026-06-24 06:18] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs

- [2026-06-24 06:18] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs

- [2026-06-24 06:18] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs

- [2026-06-24 06:18] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs

- [2026-06-24 06:20] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs

- [2026-06-24 06:21] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs

- [2026-06-24 06:21] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs

- [2026-06-24 06:21] Modificado: C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs

- [2026-06-24 06:23] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs

- [2026-06-24 06:24] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs

- [2026-06-24 06:24] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs

- [2026-06-24 06:24] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs

- [2026-06-24 06:24] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs

- [2026-06-24 06:24] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs

- [2026-06-24 06:24] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs

- [2026-06-24 06:26] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs

- [2026-06-24 06:26] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs

- [2026-06-24 06:26] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs

- [2026-06-24 06:26] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs

- [2026-06-24 06:27] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs

- [2026-06-24 06:27] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs

- [2026-06-24 06:28] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs

- [2026-06-24 06:31] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

- [2026-06-24 06:31] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs

- [2026-06-24 06:32] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs

- [2026-06-24 06:32] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs

- [2026-06-24 06:32] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs

- [2026-06-24 07:11] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs

- [2026-06-24 07:11] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs

- [2026-06-24 07:12] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs

- [2026-06-24 07:12] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs

- [2026-06-24 07:13] Modificado: c:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs

## Fin de sesión — 2026-06-24 07:14 (216 archivos modificados)
- C:\Phoenix Projects\CompuTech\.mcp.json
- C:\Phoenix Projects\CompuTech\CompuTech-ProjectSpec.md
- C:\Phoenix Projects\CompuTech\.github\PULL_REQUEST_TEMPLATE.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\GITFLOW.md
- C:\Phoenix Projects\CompuTech\docs\README.md
- C:\Phoenix Projects\CompuTech\docs\01-especificacion\especificacion.md
- C:\Phoenix Projects\CompuTech\docs\02-arquitectura\arquitectura.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.Development.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Program.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Customers\CustomerRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Properties\launchSettings.json
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomer\CreateCustomerCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\CreateCustomerLocation\CreateCustomerLocationCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomer\DeactivateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomer\DeactivateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomerLocation\DeactivateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\DeactivateCustomerLocation\DeactivateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomer\UpdateCustomerCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Commands\UpdateCustomerLocation\UpdateCustomerLocationCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\DTOs\CustomerLocationDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetAllCustomers\GetAllCustomersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerById\GetCustomerByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Customers\Queries\GetCustomerLocations\GetCustomerLocationsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Customer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\CustomerLocation.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerLocationRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ICustomerRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624033807_AddCustomerAndLocation.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624033807_AddCustomerAndLocation.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\CustomerConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\CustomerLocationConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\CustomerLocationRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\CustomerRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

## Fin de sesión — 2026-06-24 08:37 (168 archivos modificados)
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

## Fin de sesión — 2026-06-24 08:38 (168 archivos modificados)
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

- [2026-06-24 08:41] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs

- [2026-06-24 08:41] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs

## Fin de sesión — 2026-06-24 08:42 (169 archivos modificados)
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

## Fin de sesión — 2026-06-24 08:53 (170 archivos modificados)
- C:\Phoenix Projects\CompuTech\SETUP.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

## Fin de sesión — 2026-06-24 08:57 (170 archivos modificados)
- C:\Phoenix Projects\CompuTech\SETUP.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

- [2026-06-24 09:04] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs

- [2026-06-24 09:05] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs

## Fin de sesión — 2026-06-24 09:07 (171 archivos modificados)
- C:\Phoenix Projects\CompuTech\SETUP.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

## Fin de sesión — 2026-06-24 09:19 (172 archivos modificados)
- C:\Phoenix Projects\CompuTech\CLAUDE.md
- C:\Phoenix Projects\CompuTech\SETUP.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

## Fin de sesión — 2026-06-24 09:24 (173 archivos modificados)
- C:\Phoenix Projects\CompuTech\CLAUDE.md
- C:\Phoenix Projects\CompuTech\SETUP.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\03-plan\plan-de-tareas.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs

## Fin de sesión — 2026-06-24 11:01 (182 archivos modificados)
- C:\Phoenix Projects\CompuTech\CLAUDE.md
- C:\Phoenix Projects\CompuTech\SETUP.md
- C:\Phoenix Projects\CompuTech\docs\BITACORA.md
- C:\Phoenix Projects\CompuTech\docs\03-plan\plan-de-tareas.md
- C:\Phoenix Projects\CompuTech\docs\evidence\01-plan-mode.md
- C:\Phoenix Projects\CompuTech\docs\evidence\02-init-claude-md.md
- C:\Phoenix Projects\CompuTech\docs\evidence\03-test-driven-iteration.md
- C:\Phoenix Projects\CompuTech\docs\evidence\04-documentation-guidelines.md
- C:\Phoenix Projects\CompuTech\docs\evidence\05-security.md
- C:\Phoenix Projects\CompuTech\docs\evidence\06-github-mcp-integration.md
- C:\Phoenix Projects\CompuTech\docs\evidence\07-custom-skill.md
- C:\Phoenix Projects\CompuTech\docs\evidence\08-custom-hook.md
- C:\Phoenix Projects\CompuTech\docs\evidence\README.md
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\CustomersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\EquipmentController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\InventoryController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\MaintenanceSchedulesController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\ServiceOrdersController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Controllers\TechniciansController.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Equipment\EquipmentRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Inventory\InventoryRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\MaintenanceSchedules\MaintenanceScheduleRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\ServiceOrders\ServiceOrderRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.API\Models\Technicians\TechnicianRequests.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Interfaces\IGenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\DeactivateEquipment\DeactivateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\RegisterEquipment\RegisterEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Commands\UpdateEquipment\UpdateEquipmentCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\DTOs\EquipmentDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetAllEquipment\GetAllEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentByCustomer\GetEquipmentByCustomerQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentById\GetEquipmentByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Equipment\Queries\GetEquipmentBySerialNumber\GetEquipmentBySerialNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\AdjustStock\AdjustStockCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\CreateInventoryItem\CreateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\DeactivateInventoryItem\DeactivateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Commands\UpdateInventoryItem\UpdateInventoryItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\DTOs\InventoryItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetAllInventoryItems\GetAllInventoryItemsQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemById\GetInventoryItemByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Inventory\Queries\GetInventoryItemBySku\GetInventoryItemBySkuQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\CreateMaintenanceSchedule\CreateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\DeactivateMaintenanceSchedule\DeactivateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Commands\UpdateMaintenanceSchedule\UpdateMaintenanceScheduleCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\DTOs\MaintenanceScheduleDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetAllMaintenanceSchedules\GetAllMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetDueMaintenanceSchedules\GetDueMaintenanceSchedulesQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Queries\GetMaintenanceScheduleByEquipment\GetMaintenanceScheduleByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderService.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\AddServiceOrderItem\AddServiceOrderItemCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CancelServiceOrder\CancelServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CompleteServiceOrder\CompleteServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\CreateServiceOrder\CreateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\DelayServiceOrder\DelayServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\RemoveServiceOrderItem\RemoveServiceOrderItemCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\StartServiceOrder\StartServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Commands\UpdateServiceOrder\UpdateServiceOrderCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDetailDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\DTOs\ServiceOrderItemDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetAllServiceOrders\GetAllServiceOrdersQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderById\GetServiceOrderByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderByNumber\GetServiceOrderByNumberQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByEquipment\GetServiceOrdersByEquipmentQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByStatus\GetServiceOrdersByStatusQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrdersByTechnician\GetServiceOrdersByTechnicianQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\ServiceOrders\Queries\GetServiceOrderTotal\GetServiceOrderTotalQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\CreateTechnician\CreateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\DeactivateTechnician\DeactivateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommand.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Commands\UpdateTechnician\UpdateTechnicianCommandValidator.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\DTOs\TechnicianDto.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetActiveTechnicians\GetActiveTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetAllTechnicians\GetAllTechniciansQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQuery.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Technicians\Queries\GetTechnicianById\GetTechnicianByIdQueryHandler.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Equipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\InventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\MaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrder.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\ServiceOrderItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Entities\Technician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\EquipmentType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryCategory.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\InventoryItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\MaintenanceFrequency.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderItemType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderPriority.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderStatus.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderSubType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Enums\ServiceOrderType.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IEquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IInventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IMaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\IServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Domain\Interfaces\ITechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624093226_AddEquipment.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624094057_AddTechnician.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624095146_AddInventoryItem.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624101100_AddServiceOrders.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\20260624102630_AddMaintenanceSchedule.Designer.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Migrations\CompuTechDbContextModelSnapshot.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\EquipmentConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\InventoryItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\MaintenanceScheduleConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\ServiceOrderItemConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Configurations\TechnicianConfiguration.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\EquipmentRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\InventoryItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\MaintenanceScheduleRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderItemRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\ServiceOrderRepository.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\Repositories\TechnicianRepository.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Handlers\CreateMaintenanceScheduleCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\MaintenanceSchedules\Services\GenerateNextMaintenanceOrderServiceTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\AddServiceOrderItemCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CancelServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Handlers\CompleteServiceOrderCommandHandlerTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\AddServiceOrderItemCommandValidatorTests.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\ServiceOrders\Validators\CreateServiceOrderCommandValidatorTests.cs
