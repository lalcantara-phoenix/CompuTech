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

- [2026-06-23 21:47] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs

## Fin de sesión — 2026-06-23 21:47 (15 archivos modificados)
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
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\UnitTest1.cs

## Fin de sesión — 2026-06-23 22:02 (15 archivos modificados)
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
- C:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\UnitTest1.cs

- [2026-06-23 22:14] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\Common\Behaviors\ValidationBehavior.cs

- [2026-06-23 22:14] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Application\DependencyInjection.cs

- [2026-06-23 22:14] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs

- [2026-06-23 22:14] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs

- [2026-06-23 22:15] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Program.cs

- [2026-06-23 22:15] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\appsettings.json

- [2026-06-23 22:15] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\CompuTech.API.csproj

- [2026-06-23 22:17] Modificado: c:\Phoenix Projects\CompuTech\src\CompuTech.API\Program.cs

## Fin de sesión — 2026-06-23 22:18 (18 archivos modificados)
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
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\DependencyInjection.cs
- C:\Phoenix Projects\CompuTech\src\CompuTech.Infrastructure\Persistence\CompuTechDbContext.cs
- C:\Phoenix Projects\CompuTech\tests\CompuTech.Application.Tests\UnitTest1.cs
