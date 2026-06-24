# Evidencia Académica — CompuTech API

Documentación de los 8 criterios de evaluación aplicados durante el desarrollo del proyecto CompuTech, una Web API REST con .NET 10, Clean Architecture y CQRS.

**Repositorio:** `lalcantara-phoenix/CompuTech`  
**Rama de producción:** `main`  
**Fecha de desarrollo:** 2026-06-23 al 2026-06-24

---

## Índice de Evidencias

| # | Criterio | Origen | Archivo |
|---|---|---|---|
| 1 | Plan Mode + Ask Mode | Curso 1 | [01-plan-mode.md](01-plan-mode.md) |
| 2 | /init y CLAUDE.md | Curso 1 | [02-init-claude-md.md](02-init-claude-md.md) |
| 3 | Test-driven Iteration | Curso 1 | [03-test-driven-iteration.md](03-test-driven-iteration.md) |
| 4 | Documentation Guidelines | Curso 1 | [04-documentation-guidelines.md](04-documentation-guidelines.md) |
| 5 | Security | Curso 1 | [05-security.md](05-security.md) |
| 6 | GitHub MCP Integration | Curso 2 | [06-github-mcp-integration.md](06-github-mcp-integration.md) |
| 7 | Custom Skill | Curso 2 | [07-custom-skill.md](07-custom-skill.md) |
| 8 | Custom Hook | Curso 2 | [08-custom-hook.md](08-custom-hook.md) |

---

## Resumen del Proyecto

El proyecto CompuTech es una Web API REST para gestión de órdenes de servicio técnico. Cubre **6 módulos** completos (Customers, Equipment, Technicians, Inventory, ServiceOrders, MaintenanceSchedule) con:

- **44 pruebas unitarias** — 0 fallos
- **6 migraciones EF Core** — aplicadas sobre SQL Server LocalDB
- **8 reglas de negocio** — implementadas en handlers de Application layer
- **9 skills personalizados** de Claude Code — para acelerar el desarrollo
- **3 hooks automáticos** — compilación continua, bitácora y log de migraciones
- **2 fases de GitFlow** — `feature/* → testing → main` con squash merge
- **~40 issues** gestionados via GitHub MCP Integration
