# Evidencia 6 — GitHub MCP Integration

**Criterio:** Usar la integración de GitHub MCP para al menos una acción real: crear un issue, hacer un commit, abrir o revisar un PR desde Claude Code.

---

## Descripción

La integración de GitHub MCP se utilizó de forma **masiva y sistemática** durante todo el desarrollo del proyecto. Claude Code gestionó el ciclo completo de issues, ramas y PRs sin salir del entorno de desarrollo, usando las herramientas `mcp__github__*` del servidor MCP de GitHub.

---

## Configuración del Servidor MCP

**Archivo:** `.mcp.json` (raíz del repositorio)

```json
{
  "mcpServers": {
    "github": {
      "command": "npx",
      "args": ["-y", "@modelcontextprotocol/server-github"],
      "env": {
        "GITHUB_PERSONAL_ACCESS_TOKEN": "..."
      }
    }
  }
}
```

**Activación automática:** `.claude/settings.json` tiene `"enableAllProjectMcpServers": true`, lo que activa el servidor MCP de GitHub en cada sesión del proyecto.

---

## Acciones Realizadas con GitHub MCP

### 1. Creación del Backlog Completo (skill `/load-issues`)

El agente `github-project-manager` usó `mcp__github__create_issue` para crear todos los issues del proyecto:

- **~38 issues** creados con body técnico, labels y milestone
- **7 milestones** creados: Setup, Customers, Equipment, Technicians, Inventory, ServiceOrders, MaintenanceSchedule
- **12 labels** creados: por capa (domain, application, infrastructure, api, testing) y por módulo

### 2. PRs de Feature → Testing (skill `/submit-feature-pr`)

Por cada issue implementado, Claude usó:

| Tool MCP | Propósito |
|---|---|
| `mcp__github__get_issue` | Leer el título del issue para el PR |
| `mcp__github__create_pull_request` | Crear PR `feature/* → testing` |
| `mcp__github__get_pull_request` | Verificar estado mergeable |
| `mcp__github__merge_pull_request` | Squash merge del PR |

**Ejemplo de PR creado por MCP:**
- PR #80: `fix(api): unique route names to prevent startup conflict`
- PR #84: `seed-data.ps1 + fix async validation + fix Include navigation`
- PR #86: `docs: add CLAUDE.md with architecture guide for AI agents`

### 3. PRs de Módulo Testing → Main (skill `/release-module`)

Cuando todos los issues de un módulo estaban integrados en `testing`, Claude creó el PR de release:

| Módulo | PR | Issues Cerrados |
|---|---|---|
| Customers | #9 | #5, #6, #7, #8, #9 |
| Equipment | #14 | #10, #11, #12, #13, #14 |
| Technicians | #19 | #15, #16, #17, #18, #19 |
| Inventory | #24 | #20, #21, #22, #23, #24 |
| ServiceOrders | #31 | #25, #26, #27, #28, #29, #30, #31 |
| MaintenanceSchedule | #36 | #32, #33, #34, #35, #36 |
| Tests | — | 44 tests, 0 fallos |
| Hotfixes/Docs | #89 | Route name fix, SETUP.md, CLAUDE.md, plan export |

### 4. Cierre Automático de Issues

Los `Closes #N` en el body de los PRs `testing → main` activaron el cierre automático de issues en GitHub al mergear a `main` (rama por defecto del repositorio).

**Tool usado:** `mcp__github__update_issue` como respaldo cuando el cierre automático tenía delay.

### 5. Verificación de Estados de Issues

Claude verificó el estado de cierre de cada issue después del merge:
```
mcp__github__get_issue(owner="lalcantara-phoenix", repo="CompuTech", issue_number=N)
→ state: "closed"
```

---

## Extracto del Git Log — Evidencia de PRs Mergeados

```
7c7d669  docs: export plan-de-tareas.md to docs/03-plan/
91ce95a  docs: add CLAUDE.md with architecture guide for AI agents
4ef8787  fix: async validation pipeline + Include navigation name + seed script
9d0b1b6  docs: add SETUP.md with full environment setup and end-to-end demo guide
92d3b68  release(hotfix): fix route name conflict preventing API startup
22f443e  release(tests): suite de pruebas unitarias — 44 tests, 0 fallos
9165c04  release(maintenanceschedule): módulo MaintenanceSchedule completo
a7e6210  release(serviceorders): módulo ServiceOrders completo
```

---

## Permiso Específico Concedido

El archivo `.claude/settings.local.json` contiene el permiso explícito para que Claude pueda mergear PRs sin interrumpir al usuario por confirmación:

```json
{
  "permissions": {
    "allow": ["mcp__github__merge_pull_request"]
  }
}
```

Este permiso fue necesario porque `merge_pull_request` es una operación destructiva que por defecto requiere confirmación manual.
