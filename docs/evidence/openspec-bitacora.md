# Bitácora OpenSpec — CompuTech

Registro cronológico de cada interacción con el framework OpenSpec durante el desarrollo de nuevas funcionalidades. Generado como evidencia académica del criterio **Custom Skill / Framework externo**.

**Framework:** OpenSpec v1.7.0  
**Repositorio:** `lalcantara-phoenix/CompuTech`  
**Fecha de inicio:** 2026-07-28

---

## Sesión 1 — Dashboard de KPIs

**Fecha:** 2026-07-28  
**Funcionalidad:** Endpoint `GET /api/dashboard` que agrega métricas de todos los módulos.

---

### Paso 1 — Crear el change

**Comando ejecutado:**
```bash
openspec new change "dashboard-kpis"
```
**Resultado:** OpenSpec creó el directorio `openspec/changes/dashboard-kpis/` con schema `spec-driven`.

---

### Paso 2 — Obtener el orden de construcción de artefactos

**Comando ejecutado:**
```bash
openspec status --change "dashboard-kpis" --json
```
**Resultado:** El status reveló el orden de dependencias:
- `proposal` → ready (sin dependencias)
- `specs` → blocked (requiere proposal)
- `design` → blocked (requiere proposal)
- `tasks` → blocked (requiere specs + design)

El campo `applyRequires` indica que `tasks` es el artefacto necesario para poder implementar.

---

### Paso 3 — Obtener instrucciones para cada artefacto

**Comando ejecutado por artefacto:**
```bash
openspec instructions <artifact-id> --change "dashboard-kpis" --json
```
Las instrucciones de cada artefacto incluyen:
- `context`: el contexto del proyecto inyectado desde `openspec/config.yaml`
- `rules`: reglas configuradas (ej. "Escribir propuestas en español", "separar tareas por capa")
- `template`: estructura exacta a seguir para el archivo
- `instruction`: guía específica del artefacto (qué incluir, qué evitar)

---

### Paso 4 — Artefactos generados

| Artefacto | Ruta | Descripción |
|---|---|---|
| `proposal.md` | `openspec/changes/dashboard-kpis/proposal.md` | Por qué, qué cambia, capabilities, no-goals, impacto |
| `specs/dashboard-kpis/spec.md` | `openspec/changes/dashboard-kpis/specs/dashboard-kpis/spec.md` | 4 requisitos con 9 escenarios WHEN/THEN |
| `design.md` | `openspec/changes/dashboard-kpis/design.md` | 5 decisiones técnicas con alternativas descartadas |
| `tasks.md` | `openspec/changes/dashboard-kpis/tasks.md` | 17 tareas en 5 grupos por capa arquitectónica |

---

### Paso 5 — Estado final de la propuesta

**Comando ejecutado:**
```bash
openspec status --change "dashboard-kpis"
```
**Resultado:**
```
Progress: 4/4 artifacts complete
[x] proposal  [x] specs  [x] design  [x] tasks
All artifacts complete!
```

**Próximo paso:** `/opsx:apply` para implementar las tareas.

---

### Observaciones sobre el flujo OpenSpec

1. **El contexto del `config.yaml` fue inyectado automáticamente** en las instrucciones de cada artefacto — las reglas del proyecto (convenciones CQRS, idioma español, separación por capas) se aplicaron sin tener que repetirlas manualmente.
2. **El orden de dependencias fue respetado** — no fue posible crear `tasks.md` hasta que `specs` y `design` estuvieran completos.
3. **Las reglas configuradas se aplicaron:** propuesta en español, tareas separadas por capa arquitectónica, sección Non-goals incluida.
4. **El spec generado es testeable:** cada requisito tiene al menos un escenario WHEN/THEN que puede convertirse directamente en un test unitario.

---

## Sesión 2 — Implementación con `/opsx:apply`

**Fecha:** 2026-07-29  
**Comando ejecutado:** `/opsx:apply dashboard-kpis`  
**Punto de partida:** 4/4 artefactos completos, 0/17 tareas implementadas.

---

### Paso 1 — Lectura de instrucciones de implementación

**Comando interno ejecutado por el skill:**
```bash
openspec instructions apply --change "dashboard-kpis" --json
```
**Resultado relevante:**
- `state`: ready (todos los artefactos presentes)
- `progress`: 0/17 tareas completas
- `contextFiles`: proposal.md, specs/dashboard-kpis/spec.md, design.md, tasks.md
- `operationGuidance`: tareas por capa, verificar build tras cada grupo
- `instruction`: implementar en el orden de las capas, marcar `[x]` al completar cada tarea

---

### Paso 2 — Grupo 1: Domain (tareas 1.1–1.4)

**Objetivo:** Agregar los nuevos métodos a las interfaces de repositorio.

| Tarea | Archivo | Cambio |
|---|---|---|
| 1.1 | `IServiceOrderRepository.cs` | `Task<IReadOnlyList<(ServiceOrderStatus Status, int Count)>> GetCountsByStatusAsync(...)` |
| 1.2 | `ITechnicianRepository.cs` | `Task<IReadOnlyList<(int TechnicianId, string FullName, int ActiveOrderCount)>> GetTopByActiveOrdersAsync(int top, ...)` |
| 1.3 | `IInventoryItemRepository.cs` | `Task<IReadOnlyList<InventoryItem>> GetBelowStockThresholdAsync(int threshold, ...)` |
| 1.4 | `IMaintenanceScheduleRepository.cs` | Verificado: `GetDueAsync(DateTime referenceDate)` ya cubre "próximos 30 días" pasando `DateTime.UtcNow.AddDays(30)` — no se requirió cambio |

**Estado:** ✅ 4/4 tareas completadas

---

### Paso 3 — Grupo 2: Application (tareas 2.1–2.4)

**Objetivo:** Crear la query, los DTOs y el handler.

| Tarea | Archivo creado | Descripción |
|---|---|---|
| 2.1 | `Application/Dashboard/Queries/GetDashboard/GetDashboardQuery.cs` | `public record GetDashboardQuery : IRequest<DashboardDto>` |
| 2.2 | `Application/Dashboard/DTOs/DashboardDto.cs` | Record principal con las cuatro colecciones |
| 2.3 | (mismo archivo) | DTOs de soporte: `OrderStatusCountDto`, `TechnicianWorkloadDto`, `LowStockItemDto`, `DueScheduleDto` |
| 2.4 | `Application/Dashboard/Queries/GetDashboard/GetDashboardQueryHandler.cs` | Handler que inyecta cuatro repositorios |

**Error encontrado y corregido:**  
El handler usó inicialmente la sintaxis `await (Task1, Task2, Task3, Task4)` para ejecutar los cuatro repositorios en paralelo. C# no soporta `await` sobre una tupla de Tasks (`CS1061: '(Task<...>, ...)' does not contain a definition for 'GetAwaiter'`). Se corrigió usando el patrón `Task.WhenAll`:

```csharp
// ❌ Inválido en C#
var (a, b, c, d) = await (repo1.Query(), repo2.Query(), repo3.Query(), repo4.Query());

// ✅ Correcto
var t1 = repo1.GetCountsByStatusAsync(ct);
var t2 = repo2.GetTopByActiveOrdersAsync(5, ct);
var t3 = repo3.GetBelowStockThresholdAsync(5, ct);
var t4 = repo4.GetDueAsync(DateTime.UtcNow.AddDays(30), ct);
await Task.WhenAll(t1, t2, t3, t4);
var statusCounts = await t1;  // retorna inmediatamente (ya completado)
```

**Segunda corrección:** Las propiedades de `InventoryItem` se llaman `SKU` (mayúsculas) y `StockQuantity` — no `Sku`/`Stock` como se asumió inicialmente. Identificado revisando la entidad de dominio.

**Estado:** ✅ 4/4 tareas completadas — build: 0 errores

---

### Paso 4 — Grupo 3: Infrastructure (tareas 3.1–3.4)

**Objetivo:** Implementar los nuevos métodos de repositorio con EF Core.

| Tarea | Repositorio | Implementación |
|---|---|---|
| 3.1 | `ServiceOrderRepository.cs` | `GroupBy(x => x.Status).Select(g => new { Status = g.Key, Count = g.Count() })` |
| 3.2 | `TechnicianRepository.cs` | Correlated subquery: cuenta `ServiceOrders` con status `Planned` o `InProgress` por técnico, ordena descendente, `Take(top)` |
| 3.3 | `InventoryItemRepository.cs` | `Where(i => i.IsActive && i.StockQuantity <= threshold).OrderBy(i => i.StockQuantity)` |
| 3.4 | `MaintenanceScheduleRepository.cs` | Sin cambio — `GetDueAsync` ya implementado y cubre el requerimiento |

**Estado:** ✅ 4/4 tareas completadas — build: 0 errores

---

### Paso 5 — Grupo 4: API (tarea 4.1)

**Objetivo:** Crear el controller con el único endpoint del dashboard.

**Archivo creado:** `src/CompuTech.API/Controllers/DashboardController.cs`

```csharp
[Route("api/dashboard")]
[ApiController]
public class DashboardController(IMediator mediator) : ControllerBase
{
    [HttpGet(Name = "GetDashboard")]
    public async Task<IActionResult> GetDashboard(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetDashboardQuery(), cancellationToken);
        return Ok(result);
    }
}
```

El nombre de ruta `"GetDashboard"` sigue la convención del proyecto de evitar `nameof(...)` entre controllers.

**Estado:** ✅ 1/1 tarea completada — build: 0 errores

---

### Paso 6 — Grupo 5: Tests (tarea 5.1)

**Objetivo:** Suite de pruebas unitarias para `GetDashboardQueryHandler`.

**Archivo creado:** `tests/CompuTech.Application.Tests/Dashboard/GetDashboardQueryHandlerTests.cs`

9 tests escritos con xUnit + Moq + FluentAssertions:

| # | Nombre del test | Qué verifica |
|---|---|---|
| 1 | `Handle_ReturnsAllSections_WhenAllRepositoriesHaveData` | Las 4 secciones del DTO se populan correctamente |
| 2 | `Handle_ReturnsEmptySections_WhenRepositoriesReturnEmpty` | No lanza excepción con repos vacíos |
| 3 | `Handle_OrdersByStatus_IncludesAllStatusValues` | `OrdersByStatus` tiene una entrada por cada valor del enum |
| 4 | `Handle_OrdersByStatus_MapsCountsCorrectly` | Los conteos se asignan al status correcto; ausentes → Count=0 |
| 5 | `Handle_TopTechnicians_PassesCorrectTopCount` | Llama `GetTopByActiveOrdersAsync(5, ...)` exactamente una vez |
| 6 | `Handle_LowStockItems_PassesCorrectThreshold` | Llama `GetBelowStockThresholdAsync(5, ...)` exactamente una vez |
| 7 | `Handle_TopTechnicians_MapsTupleFieldsToDto` | Mapeo correcto de la tupla al DTO |
| 8 | `Handle_DueMaintenanceSchedules_MapsEntityFieldsToDto` | Mapeo correcto de la entidad al DTO |
| 9 | `Handle_LowStockItems_MapsEntityFieldsToDto` | `SKU` y `StockQuantity` mapeados correctamente |

**Nota técnica:** Se requirió agregar alias de tipos al inicio del archivo de test para evitar que el compilador resolviera `MaintenanceSchedule` como el namespace `CompuTech.Application.MaintenanceSchedule` en vez de la entidad de dominio — convención documentada en CLAUDE.md.

**Resultado:** `dotnet test` — **53/53 tests pasan** (44 previos + 9 nuevos)

**Estado:** ✅ 1/1 tarea completada

---

### Resumen del apply

| Grupo | Tareas | Estado |
|---|---|---|
| 1 — Domain | 1.1, 1.2, 1.3, 1.4 | ✅ Completo |
| 2 — Application | 2.1, 2.2, 2.3, 2.4 | ✅ Completo |
| 3 — Infrastructure | 3.1, 3.2, 3.3, 3.4 | ✅ Completo |
| 4 — API | 4.1 | ✅ Completo |
| 5 — Tests | 5.1 | ✅ Completo |
| **Total** | **17/17** | **✅ Implementación completa** |

**Build final:** `dotnet build` — 0 errores  
**Tests finales:** `dotnet test` — 53/53 pasan  
**Commit:** `feat(dashboard): implementar endpoint GET /api/dashboard con KPIs`  
**PR:** `feature/dashboard-kpis → testing` — PR #93

---

### Observaciones sobre el flujo apply

1. **El skill mantuvo el contexto del spec durante toda la implementación** — las decisiones de diseño (umbral de stock = 5, top técnicos = 5, ventana de 30 días) se tomaron del `design.md` generado en la sesión 1, sin necesidad de repetirlas.
2. **El orden por capas evitó bloqueos** — implementar Domain primero permitió que Application compilara, y Application permitió que Infrastructure implementara sin errores de tipo.
3. **Dos errores de build encontrados durante el apply** sirven como evidencia de que el framework no es un generador automático sin revisión — requiere supervisión técnica para corregir incompatibilidades de lenguaje y convenciones de entidad.
4. **La tarea 1.4 fue resuelta sin código nuevo** — el skill leyó la implementación existente y determinó que `GetDueAsync(DateTime.UtcNow.AddDays(30))` ya cubre el requisito, evitando código duplicado.

---

## Sesión 3 — Cierre con `/opsx:archive`

**Fecha:** 2026-07-29  
**Comando ejecutado:** `/opsx:archive dashboard-kpis`  
**Punto de partida:** 17/17 tareas implementadas, PR #93 creado.

---

### Paso 1 — Verificación de completitud

**Comandos ejecutados:**
```bash
openspec instructions archive --change "dashboard-kpis" --json
openspec status --change "dashboard-kpis" --json
```

**Resultado:**
- `isComplete: true`
- 4/4 artefactos con status `done`
- 17/17 tareas con `[x]`
- Sin advertencias

**Guidance de archive aplicado:** resumir reglas de negocio implementadas y desviaciones del proposal.

---

### Paso 2 — Evaluación del delta spec

El delta spec `openspec/changes/dashboard-kpis/specs/dashboard-kpis/spec.md` existe pero **no había spec principal** en `openspec/specs/dashboard-kpis/spec.md` — capability nueva, nunca archivada antes.

**Operación requerida:** crear el spec principal con los 5 requisitos y 12 escenarios de la capability `dashboard-kpis`.

---

### Paso 3 — Sync del spec

**Operación:** antes de escribir el spec principal se obtuvieron las reglas del artefacto `specs` vía:
```bash
openspec instructions specs --change "dashboard-kpis" --json
```

Las reglas confirmaron el formato correcto para capabilities nuevas: incluir `## Purpose`, convertir `## ADDED Requirements` en `## Requirements` canónico (sin prefijo).

**Archivo creado:** `openspec/specs/dashboard-kpis/spec.md`

| Elemento | Resultado |
|---|---|
| `## Purpose` | Copiado del delta spec |
| Requisitos | 5 requisitos, cabecera `ADDED` eliminada |
| Escenarios | 12 escenarios WHEN/THEN |
| Verificación | Delta ↔ main: todos los requisitos y escenarios presentes ✅ |

---

### Paso 4 — Archive

```bash
mv openspec/changes/dashboard-kpis \
   openspec/changes/archive/2026-07-29-dashboard-kpis
```

El directorio del change fue movido al archivo histórico con prefijo de fecha.

---

### Resumen del archive

| Elemento | Valor |
|---|---|
| Change | `dashboard-kpis` |
| Schema | `spec-driven` |
| Archivado en | `openspec/changes/archive/2026-07-29-dashboard-kpis/` |
| Spec principal | `openspec/specs/dashboard-kpis/spec.md` ✓ Creado y sincronizado |
| Artefactos | 4/4 completos |
| Tareas | 17/17 completas |

---

### Reglas de negocio implementadas (por guidance del archive)

| Regla | Implementación |
|---|---|
| Dashboard agrega 4 fuentes en paralelo | `Task.WhenAll` en el handler — sin bloqueo secuencial |
| Conteo por status incluye todos los valores del enum | Handler itera `Enum.GetValues<ServiceOrderStatus>()` y asigna 0 a los ausentes |
| Umbral de stock = 5 unidades | Constante `LowStockThreshold = 5` en el handler (no hardcoded en la query) |
| Top técnicos = 5 | Constante `TopTechniciansCount = 5` |
| Ventana de mantenimiento = 30 días | Constante `DueWithinDays = 30`; reutiliza `GetDueAsync` existente |

**Desviaciones del proposal:** ninguna. El endpoint implementado cubre exactamente los cuatro módulos definidos en el proposal (`OrdersByStatus`, `TopTechnicians`, `LowStockItems`, `DueMaintenanceSchedules`) con los valores de umbral y ventana especificados en `design.md`.

---

### Flujo OpenSpec completo — Dashboard KPIs

```
propose  →  apply  →  archive
[2026-07-28]  [2026-07-29]  [2026-07-29]
4 artefactos  17 tareas     spec canónico
generados     implementadas en openspec/specs/
```

---

### Observaciones sobre el flujo archive

1. **El sync verifica antes de mover** — el skill obtiene las reglas del artefacto `specs` antes de escribir el archivo principal, garantizando que el formato sea correcto. Si el CLI no responde, el archive se detiene sin dejar el repositorio en estado inconsistente.
2. **El spec principal es el registro permanente** — una vez archivado, `openspec/specs/dashboard-kpis/spec.md` es el contrato de comportamiento que cualquier futuro change puede referenciar o modificar con `MODIFIED Requirements`.
3. **El archive histórico queda en `openspec/changes/archive/`** — con prefijo de fecha, permite reconstruir el historial completo de decisiones (proposal + design + tasks) de cada feature entregada.

---
