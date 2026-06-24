---
name: load-issues
description: >
  Carga el backlog completo del proyecto CompuTech como issues en GitHub.
  Crea los milestones, los labels y los ~38 issues en orden (Setup primero,
  luego entidad por entidad) usando el agente github-project-manager.
  Invócalo una sola vez para poblar el backlog inicial del repositorio.
argument-hint: ""
---

# Load Issues — CompuTech

Carga el backlog completo del proyecto como issues de GitHub en el repositorio `lalcantara-phoenix/CompuTech`.

---

## Pre-requisitos

Antes de ejecutar este skill, verificar:
1. El MCP de GitHub está activo (los tools `mcp__github__*` son accesibles)
2. La rama `testing` existe en el repositorio
3. No existen issues previos (para evitar duplicados)

---

## Paso 1: Crear los Milestones en GitHub

Usar `mcp__github__create_milestone` (o la herramienta equivalente disponible) para crear los 7 milestones en este orden:

| # | Título | Descripción |
|---|---|---|
| 1 | `Setup` | Configuración inicial de la solución .NET |
| 2 | `Customers` | Módulo completo: Domain → Application → Infrastructure → API → Testing |
| 3 | `Equipment` | Módulo completo: Domain → Application → Infrastructure → API → Testing |
| 4 | `Technicians` | Módulo completo: Domain → Application → Infrastructure → API → Testing |
| 5 | `Inventory` | Módulo completo: Domain → Application → Infrastructure → API → Testing |
| 6 | `ServiceOrders` | Módulo completo: Domain → Application → Infrastructure → API → Testing |
| 7 | `MaintenanceSchedule` | Módulo completo: Domain → Application → Infrastructure → API → Testing |

> **Nota:** Si `mcp__github__create_milestone` no está disponible, los milestones deberán crearse manualmente en GitHub antes de ejecutar el paso 3.

---

## Paso 2: Crear los Labels en GitHub

Usar la API de GitHub para crear los siguientes labels:

**Labels de capa:**
| Label | Color | Descripción |
|---|---|---|
| `setup` | `#0075ca` | Configuración inicial del proyecto |
| `domain` | `#7057ff` | Entidades, enums, interfaces de repositorio |
| `application` | `#008672` | CQRS handlers, DTOs, validators |
| `infrastructure` | `#e4e669` | EF Core, repositorios, migraciones |
| `api` | `#d93f0b` | Controllers REST |
| `testing` | `#0e8a16` | Pruebas unitarias |

**Labels de módulo:**
| Label | Color | Descripción |
|---|---|---|
| `customers` | `#bfd4f2` | Módulo Customers |
| `equipment` | `#bfd4f2` | Módulo Equipment |
| `technicians` | `#bfd4f2` | Módulo Technicians |
| `inventory` | `#bfd4f2` | Módulo Inventory |
| `serviceorders` | `#bfd4f2` | Módulo ServiceOrders |
| `maintenanceschedule` | `#bfd4f2` | Módulo MaintenanceSchedule |

> **Nota:** Si `mcp__github__create_label` no está disponible, los labels deberán crearse manualmente en GitHub → Issues → Labels antes de ejecutar el paso 3.

---

## Paso 3: Crear los Issues

Invocar al agente `github-project-manager` con la instrucción:

> "Crea todos los issues del backlog completo de CompuTech en GitHub, en el orden establecido (Setup primero, luego entidad por entidad: Customers, Equipment, Technicians, Inventory, ServiceOrders, MaintenanceSchedule). Lee el spec en `docs/01-especificacion/especificacion.md` para completar el contexto técnico de cada issue. Asigna el milestone y los labels correctos a cada uno."

El agente creará los ~38 issues usando `mcp__github__create_issue`.

---

## Paso 4: Verificar

Después de que el agente termine, verificar:
1. Contar los issues creados por milestone
2. Confirmar que cada issue tiene: body completo, labels, milestone y rama de trabajo
3. Reportar el resumen al usuario

---

## Resumen esperado

```
✓ Milestones creados: 7
✓ Labels creados: 12
✓ Issues creados: ~38
  - Setup: 3
  - Customers: 5
  - Equipment: 5
  - Technicians: 5
  - Inventory: 5
  - ServiceOrders: 7
  - MaintenanceSchedule: 5 (o más según spec)
```
