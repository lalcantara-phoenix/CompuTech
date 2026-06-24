# Evidencia 2 — /init y CLAUDE.md

**Criterio:** El repositorio debe tener un CLAUDE.md configurado con contexto del proyecto: stack, convenciones, rutas importantes y restricciones.

---

## Descripción

Se utilizó el skill `/init` de Claude Code para generar el archivo `CLAUDE.md` en la raíz del repositorio. Este archivo sirve como guía contextual para que cualquier instancia de Claude Code que abra el repositorio comprenda la arquitectura, convenciones y restricciones del proyecto sin necesidad de explorar el código manualmente.

## Artefacto Generado

**Archivo:** `CLAUDE.md` (raíz del repositorio)

## Secciones del CLAUDE.md

### 1. Commands
Comandos PowerShell listos para usar:
- Build: `dotnet build CompuTech.slnx`
- Tests: `dotnet test` (incluyendo filtros por clase y por método individual)
- Run API: `dotnet run --project src/CompuTech.API/CompuTech.API.csproj`
- EF migrations: `dotnet ef migrations add` y `dotnet ef database update`
- Seed data: `.\seed-data.ps1`

### 2. Architecture
Descripción de Clean Architecture + CQRS:
- Dirección de dependencias: `API → Application → Domain`; `Infrastructure → Application`
- Estructura de carpetas por capa
- Convenciones de carpetas por caso de uso

### 3. Application Layer Conventions
Restricciones explícitas para evitar errores comunes:
- Commands retornan `IRequest<int>` o `IRequest` según corresponda
- Handlers lanzan `KeyNotFoundException` y `InvalidOperationException`
- **Restricción crítica:** `ValidationBehavior<TRequest, TResponse>` usa `ValidateAsync` — nunca llamar `Validate()` sincrónicamente porque los validators usan `MustAsync`

### 4. Domain Entities
- Propiedades con `private set`
- Constructores `private` sin parámetros
- Construcción solo via factory methods (`Entity.Create(...)`)
- **Restricción crítica:** `.Include(x => x.Items)` — **no** `.Include("_items")` en EF Core 10

### 5. Infrastructure
- `ApplyConfigurationsFromAssembly` carga todas las configuraciones automáticamente
- Enums almacenados como `int` via `.HasConversion<int>()`

### 6. Key Business Rules Table

| Regla | Handler |
|---|---|
| #1 — Un schedule activo por equipo | `CreateMaintenanceScheduleCommandHandler` |
| #3/#4 — Part/Labor InventoryItemId | `AddServiceOrderItemCommandValidator` |
| #5 — SubType válido para Type | `CreateServiceOrderCommandValidator` |
| #6 — Subtotal = Qty × UnitPrice | `AddServiceOrderItemCommandHandler` |
| #8 — Preventive genera siguiente orden | `CompleteServiceOrderCommandHandler` |

### 7. API Controllers
Restricciones de rutas únicas globales y uso de `CreatedAtRoute`

### 8. Tests
Fix de colisión de namespace con aliases de tipo:
```csharp
using EquipmentEntity = CompuTech.Domain.Entities.Equipment;
```

### 9. Git Workflow
Flujo GitFlow de dos fases con el comando exacto para evitar conflictos antes del PR `testing → main`:
```powershell
git merge origin/main --strategy-option=ours --no-edit
```

### 10. Packages Table
Versiones exactas de todas las dependencias.

## Commit de Evidencia

```
91ce95a  docs: add CLAUDE.md with architecture guide for AI agents
```

Publicado via PR #86 (`feature/claude-md → testing`) y liberado a `main` via PR #87.

## Ruta al Artefacto

```
CLAUDE.md
```
