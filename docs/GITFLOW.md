# GitFlow — CompuTech

## Estructura de ramas

```
main
 └── testing          ← integración / QA
       └── feature/*  ← desarrollo por tarea
```

| Rama | Propósito |
|---|---|
| `main` | Código en producción. Solo recibe PRs desde `testing` tras QA aprobado. |
| `testing` | Rama de integración. Acumula features completas para validación. |
| `feature/*` | Una rama por issue. Se crea desde `testing` y se merge de vuelta via PR. |

---

## Convención de nombres de ramas feature

El nombre de la rama es el kebab-case del título del issue:

```
feature/[titulo-del-issue-en-kebab-case]
```

**Ejemplos:**
| Issue | Rama |
|---|---|
| Setup: Crear solución .NET y estructura de proyectos | `feature/setup-crear-solucion-dotnet` |
| Domain: Customer y CustomerLocation | `feature/domain-customer-y-customerlocation` |
| Application: Módulo Customers | `feature/application-modulo-customers` |
| Infrastructure: Configuración EF Core — Customer | `feature/infrastructure-ef-core-customer` |
| API: Controller Customers | `feature/api-controller-customers` |
| Testing: Suite Customers | `feature/testing-suite-customers` |

---

## Flujo de trabajo

### 1. Iniciar una tarea

```bash
# Siempre partir desde testing
git checkout testing
git pull origin testing
git checkout -b feature/[nombre-del-issue]
```

### 2. Desarrollar

- Implementar la tarea en la rama `feature/*`
- Commits atómicos con mensajes descriptivos
- El hook de compilación valida automáticamente cada archivo `.cs` guardado

### 3. Abrir PR: feature → testing

- Título: igual al título del issue
- Descripción: usar la plantilla `.github/PULL_REQUEST_TEMPLATE.md`
- Referenciar el issue: `Closes #[número]`
- Revisar que todos los criterios de aceptación del issue están cumplidos

### 4. Merge a main (release)

- Solo desde `testing` hacia `main`
- Requiere que todos los issues del milestone estén cerrados
- PR con descripción del milestone completado

---

## Milestones del proyecto

| # | Milestone | Issues | Descripción |
|---|---|---|---|
| 1 | Setup | 3 | Solución .NET, DI, DbContext, Swagger |
| 2 | Customers | 5 | Domain → Application → Infrastructure → API → Testing |
| 3 | Equipment | 5 | Domain → Application → Infrastructure → API → Testing |
| 4 | Technicians | 5 | Domain → Application → Infrastructure → API → Testing |
| 5 | Inventory | 5 | Domain → Application → Infrastructure → API → Testing |
| 6 | ServiceOrders | 7 | Domain → Application → Infrastructure → API → Testing |
| 7 | MaintenanceSchedule | 5 | Domain → Application → Infrastructure → API → Testing |

---

## Labels de GitHub

| Label | Color | Uso |
|---|---|---|
| `setup` | `#0075ca` | Issues de configuración inicial |
| `domain` | `#7057ff` | Entidades, enums, interfaces de repositorio |
| `application` | `#008672` | CQRS handlers, DTOs, validators |
| `infrastructure` | `#e4e669` | EF Core, repositorios, migraciones |
| `api` | `#d93f0b` | Controllers REST |
| `testing` | `#0e8a16` | Suites de pruebas unitarias |
| `customers` | `#bfd4f2` | Módulo Customers |
| `equipment` | `#bfd4f2` | Módulo Equipment |
| `technicians` | `#bfd4f2` | Módulo Technicians |
| `inventory` | `#bfd4f2` | Módulo Inventory |
| `serviceorders` | `#bfd4f2` | Módulo ServiceOrders |
| `maintenanceschedule` | `#bfd4f2` | Módulo MaintenanceSchedule |
