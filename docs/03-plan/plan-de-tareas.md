# Plan de Tareas — CompuTech API

## Contexto

El proyecto CompuTech es una Web API REST en .NET Core (Clean Architecture + CQRS) para gestionar órdenes de servicio técnico. La especificación está completa (7 entidades, 12 enums, 6 módulos), la arquitectura documentada, y los agentes/skills de Claude Code están preconfigurados. El código fuente aún no existe — hay que crearlo desde cero.

Este plan organiza el desarrollo en 7 milestones secuenciales, desde el setup inicial hasta las pruebas unitarias, siguiendo el GitFlow ya definido en el proyecto.

---

## AUTOMATIZACIÓN DE PR POR ISSUE Y CIERRE DE MÓDULO

### Flujo definitivo (dos fases)

**Rama por defecto confirmada:** `main` (verificado en el repo `lalcantara-phoenix/CompuTech`). La rama `master` queda descartada.

```
main  ← producción (rama por defecto de GitHub)
 └── testing  ← integración por módulo
       └── feature/*  ← una rama por issue
```

**Fase 1 — Por issue (integración a testing):**
- Implementar → commit con `Closes #N` → push → PR `feature/* → testing` → merge
- El issue queda **abierto** (testing no es rama por defecto, GitHub no lo cierra)
- El `Closes #N` en el PR sirve solo para traceabilidad (link entre PR e issue)

**Fase 2 — Por módulo (liberación a main):**
- Cuando todos los issues del módulo están mergeados en `testing` Y los tests pasan:
- PR `testing → main` con `Closes #N1, Closes #N2, ..., Closes #Nk` de todos los issues del módulo
- GitHub cierra los issues automáticamente al mergear a `main` (rama por defecto)

---

### Evaluación de agentes/skills existentes

| Agente/Skill | ¿Cubre la Fase 1 o Fase 2? | Brecha |
|---|---|---|
| `dotnet-clean-arch` | Solo implementación de código | No maneja push, PR ni merge |
| `github-project-manager` | Crea/actualiza issues | No hace push, PR ni resolución de conflictos |
| `implement-module` | Implementa módulo completo | No incluye flujo de GitHub |
| `load-issues` | Carga backlog inicial | Solo para setup |

**Conclusión:** Se necesitan dos nuevos skills y una actualización del agente `dotnet-clean-arch`.

---

### Nuevo skill 1: `submit-feature-pr`

**Ubicación:** `c:\Phoenix Projects\CompuTech\.claude\skills\submit-feature-pr\SKILL.md`

**Propósito:** Fase 1 — Entregar un issue implementado a `testing`:
1. Push de `feature/[rama]` al remoto
2. Crear PR `feature/[rama] → testing` con `Closes #N` en el body (traceabilidad)
3. Verificar si el PR es mergeable (detectar conflictos)
4. Si hay conflictos → rebase automático:
   ```
   git fetch origin testing
   git rebase origin/testing
   git checkout --theirs .   # para conflictos add/add
   git add .
   git rebase --continue --no-edit
   git push --force-with-lease origin feature/[rama]
   ```
5. Mergear PR con squash merge
6. **NO cerrar el issue** (el cierre ocurre en Fase 2 al llegar a `main`)

**Tools:** `Bash`, `mcp__github__create_pull_request`, `mcp__github__get_pull_request`, `mcp__github__merge_pull_request`

---

### Nuevo skill 2: `release-module`

**Ubicación:** `c:\Phoenix Projects\CompuTech\.claude\skills\release-module\SKILL.md`

**Propósito:** Fase 2 — Liberar un módulo completo de `testing` a `main`:
1. Recibe: nombre del módulo + lista de issue numbers (ej. `Customers`, `[5, 6, 7, 8, 9]`)
2. Ejecutar `dotnet test` para verificar que los tests pasan
3. Crear PR `testing → main` con body:
   ```
   ## Módulo [Nombre] — Release completo
   Closes #N1, Closes #N2, Closes #N3, Closes #N4, Closes #N5
   ```
4. Mergear PR con squash merge
5. GitHub cierra automáticamente todos los issues al mergear a `main` (rama por defecto)
6. Reportar resumen de issues cerrados

**Tools:** `Bash`, `mcp__github__create_pull_request`, `mcp__github__get_pull_request`, `mcp__github__merge_pull_request`

---

### Cambios al agente `dotnet-clean-arch`

**Archivo:** `c:\Phoenix Projects\CompuTech\.claude\agents\dotnet-clean-arch.md`

Reemplazar la sección "Flujo de trabajo por issue (NORMA OBLIGATORIA)" con:

```
## Flujo de trabajo por issue (NORMA OBLIGATORIA)

### Fase 1 — Por issue (repetir para cada issue del módulo)
1. Rama: feature/[modulo]-[descripcion-corta]
2. Implementar el issue
3. Commit: incluir "Closes #N" en el mensaje
4. Invocar /submit-feature-pr → push + PR a testing + resolver conflictos + merge
5. El issue queda ABIERTO (se cierra en Fase 2)
6. Esperar confirmación del usuario para continuar con el siguiente issue

### Fase 2 — Por módulo (cuando TODOS los issues del módulo están en testing)
1. Verificar que dotnet test pasa
2. Invocar /release-module con el nombre del módulo y los números de issue
3. El PR de testing → main cierra automáticamente todos los issues del módulo
4. Esperar confirmación del usuario para iniciar el siguiente módulo

Nunca acumular varios issues en una sola rama. Nunca cerrar issues manualmente.
```

---

### Acciones inmediatas pendientes

| # | Acción | Detalle |
|---|---|---|
| 1 | **Mergear PR #42** | PR `feature/customers-commands → testing` (issue #6). Squash merge. Issue #6 queda abierto. |
| 2 | **Crear skill `submit-feature-pr`** | `SKILL.md` con flujo Fase 1 |
| 3 | **Crear skill `release-module`** | `SKILL.md` con flujo Fase 2 |
| 4 | **Actualizar agente `dotnet-clean-arch`** | Reemplazar sección de flujo con las dos fases |
| 5 | **Continuar issue #7** | `feature/customers-queries` — Application Queries del módulo Customers |
| 6 | **Issue #5** | Queda abierto; se cerrará junto con los demás del módulo Customers cuando se haga el PR `testing → main` |

> **Nota sobre issue #5:** NO se cierra manualmente. Espera al PR de módulo `testing → main` junto con los issues #6, #7, #8, #9 del módulo Customers.

---

## MILESTONE 0 — Setup del Proyecto

### Tarea 0.1 — Inicializar Git y conectar a GitHub
- `git init` en `c:\Phoenix Projects\CompuTech`
- Crear repositorio remoto `lalcantara-phoenix/CompuTech` en GitHub
- Primer commit con archivos de configuración existentes (`.claude/`, `docs/`, `.github/`)

### Tarea 0.2 — Scaffold de la solución .NET
Crear la solución y los 4 proyectos con sus referencias:
```
dotnet new sln -n CompuTech
dotnet new webapi -n CompuTech.API
dotnet new classlib -n CompuTech.Domain
dotnet new classlib -n CompuTech.Application
dotnet new classlib -n CompuTech.Infrastructure
dotnet new xunit -n CompuTech.Application.Tests
```
Referencias entre proyectos:
- `API → Application → Domain`
- `Infrastructure → Application` (implementa interfaces)
- `Tests → Application + Domain`

### Tarea 0.3 — Instalar paquetes NuGet
| Proyecto | Paquetes |
|---|---|
| Application | `MediatR`, `FluentValidation`, `FluentValidation.DependencyInjectionExtensions` |
| Infrastructure | `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Tools` |
| API | `Microsoft.EntityFrameworkCore.Design`, `Swashbuckle.AspNetCore` |
| Tests | `Moq`, `FluentAssertions`, `Microsoft.EntityFrameworkCore.InMemory` |

### Tarea 0.4 — Configurar DI base, DbContext y Swagger
- Crear `CompuTechDbContext` vacío en Infrastructure
- Registrar MediatR, FluentValidation y EF Core en `Program.cs`
- Configurar pipeline behavior para validación automática (`ValidationBehavior`)
- Connection string a SQL Server LocalDB en `appsettings.json`
- **Swagger / OpenAPI:**
  - `builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "CompuTech API", Version = "v1" }); })`
  - Habilitar comentarios XML: `<GenerateDocumentationFile>true</GenerateDocumentationFile>` en el `.csproj` de API
  - `app.UseSwagger()` y `app.UseSwaggerUI()` en `Program.cs` (no solo en Development)
  - Cada controller tendrá comentarios XML (`/// <summary>`) para que Swagger muestre descripciones de endpoints

### Verificación M0
```
dotnet build CompuTech.sln                     # Sin errores
dotnet run --project CompuTech.API             # Swagger UI en http://localhost:{port}/swagger
# Confirmar que la UI de Swagger carga correctamente
```

---

## MILESTONE 1 — Módulo Customers

**Skill:** `/implement-module Customer`

### Tarea 1.1 — Domain
- Entidad `Customer` con todos sus campos
- Entidad `CustomerLocation` con FK a Customer
- Enum: ninguno adicional
- Interfaces: `ICustomerRepository`, `ICustomerLocationRepository`
- Skill: `/scaffold-entity Customer`

### Tarea 1.2 — Application (Commands)
- `CreateCustomerCommand` + Handler + Validator
- `UpdateCustomerCommand` + Handler + Validator
- `DeactivateCustomerCommand` + Handler
- `CreateCustomerLocationCommand` + Handler + Validator
- `UpdateCustomerLocationCommand` + Handler + Validator
- `DeactivateCustomerLocationCommand` + Handler

### Tarea 1.3 — Application (Queries)
- `GetCustomerByIdQuery` + Handler → `CustomerDto`
- `GetAllCustomersQuery` + Handler → `List<CustomerDto>`
- `GetCustomerLocationsQuery` + Handler → `List<CustomerLocationDto>`

### Tarea 1.4 — Infrastructure
- `CustomerConfiguration` (Fluent API): unicidad Email, longitudes
- `CustomerLocationConfiguration`
- `CustomerRepository` implementando `ICustomerRepository`
- `CustomerLocationRepository`
- Registrar en `DbContext` y DI

### Tarea 1.5 — Migración
- Skill: `/add-migration AddCustomerAndLocation`

### Tarea 1.6 — API
- `CustomersController` con endpoints:
  - `GET /api/customers` — Listar todos
  - `GET /api/customers/{id}` — Obtener por ID
  - `POST /api/customers` — Crear
  - `PUT /api/customers/{id}` — Actualizar
  - `DELETE /api/customers/{id}` — Desactivar
  - `GET /api/customers/{id}/locations` — Listar ubicaciones
  - `POST /api/customers/{id}/locations` — Agregar ubicación
  - `PUT /api/customers/{customerId}/locations/{locationId}` — Actualizar ubicación
  - `DELETE /api/customers/{customerId}/locations/{locationId}` — Desactivar ubicación
- Cada endpoint con `[ProducesResponseType]` y comentarios XML para Swagger
- Skill: `/api-endpoint Customer`

### Verificación M1
```
dotnet run --project CompuTech.API
# Probar CRUD completo via Swagger UI en /swagger
# Expandir "Customers" → ejecutar POST, GET, PUT, DELETE desde la interfaz
```

---

## MILESTONE 2 — Módulo Equipment

**Skill:** `/implement-module Equipment`

### Tarea 2.1 — Domain
- Entidad `Equipment` con todos sus campos
- Enum: `EquipmentType { Desktop, Laptop, Server, Workstation }`
- Interfaz: `IEquipmentRepository`
- Skill: `/scaffold-entity Equipment`

### Tarea 2.2 — Application (Commands)
- `RegisterEquipmentCommand` + Handler + Validator (valida que Customer y Location existan)
- `UpdateEquipmentCommand` + Handler + Validator
- `DeactivateEquipmentCommand` + Handler

### Tarea 2.3 — Application (Queries)
- `GetEquipmentByIdQuery` + Handler → `EquipmentDto`
- `GetEquipmentByCustomerQuery` + Handler → `List<EquipmentDto>`
- `GetEquipmentBySerialNumberQuery` + Handler → `EquipmentDto`

### Tarea 2.4 — Infrastructure
- `EquipmentConfiguration`: unicidad SerialNumber, índices sobre CustomerId y LocationId
- `EquipmentRepository`

### Tarea 2.5 — Migración
- Skill: `/add-migration AddEquipment`

### Tarea 2.6 — API
- `EquipmentController` con endpoints:
  - `GET /api/equipment` — Listar todos
  - `GET /api/equipment/{id}` — Obtener por ID
  - `GET /api/equipment/serial/{serialNumber}` — Buscar por serial
  - `GET /api/customers/{customerId}/equipment` — Por cliente
  - `POST /api/equipment` — Registrar
  - `PUT /api/equipment/{id}` — Actualizar
  - `DELETE /api/equipment/{id}` — Desactivar
- Skill: `/api-endpoint Equipment`

---

## MILESTONE 3 — Módulo Technicians

**Skill:** `/implement-module Technician`

### Tarea 3.1 — Domain
- Entidad `Technician`
- Interfaz: `ITechnicianRepository`
- Skill: `/scaffold-entity Technician`

### Tarea 3.2 — Application (Commands)
- `CreateTechnicianCommand` + Handler + Validator
- `UpdateTechnicianCommand` + Handler + Validator
- `DeactivateTechnicianCommand` + Handler

### Tarea 3.3 — Application (Queries)
- `GetTechnicianByIdQuery` + Handler → `TechnicianDto`
- `GetAllTechniciansQuery` + Handler → `List<TechnicianDto>`
- `GetActiveTechniciansQuery` + Handler → filtra IsActive = true

### Tarea 3.4 — Infrastructure + Migración
- `TechnicianConfiguration`: unicidad Email
- `TechnicianRepository`
- Skill: `/add-migration AddTechnician`

### Tarea 3.5 — API
- `TechniciansController`:
  - `GET /api/technicians` — Listar todos
  - `GET /api/technicians/active` — Solo activos
  - `GET /api/technicians/{id}` — Por ID
  - `POST /api/technicians` — Crear
  - `PUT /api/technicians/{id}` — Actualizar
  - `DELETE /api/technicians/{id}` — Desactivar
- Skill: `/api-endpoint Technician`

---

## MILESTONE 4 — Módulo Inventory

**Skill:** `/implement-module InventoryItem`

### Tarea 4.1 — Domain
- Entidad `InventoryItem`
- Enums: `InventoryItemType { Product, Part, Accessory }`, `InventoryCategory { ... 15 valores }`
- Interfaz: `IInventoryItemRepository`
- Skill: `/scaffold-entity InventoryItem`

### Tarea 4.2 — Application (Commands)
- `CreateInventoryItemCommand` + Handler + Validator
- `UpdateInventoryItemCommand` + Handler + Validator
- `AdjustStockCommand` + Handler (incremento/decremento de stock, validar no negativo)
- `DeactivateInventoryItemCommand` + Handler

### Tarea 4.3 — Application (Queries)
- `GetInventoryItemByIdQuery` + Handler → `InventoryItemDto`
- `GetInventoryItemBySkuQuery` + Handler
- `GetInventoryItemsQuery` + Handler (con filtros: tipo, categoría, stock disponible)

### Tarea 4.4 — Infrastructure + Migración
- `InventoryItemConfiguration`: unicidad SKU, precisión decimal para SalePrice
- `InventoryItemRepository`
- Skill: `/add-migration AddInventoryItem`

### Tarea 4.5 — API
- `InventoryController`:
  - `GET /api/inventory` — Listar (con filtros opcionales)
  - `GET /api/inventory/{id}` — Por ID
  - `GET /api/inventory/sku/{sku}` — Por SKU
  - `POST /api/inventory` — Crear
  - `PUT /api/inventory/{id}` — Actualizar
  - `PATCH /api/inventory/{id}/stock` — Ajustar stock
  - `DELETE /api/inventory/{id}` — Desactivar
- Skill: `/api-endpoint InventoryItem`

---

## MILESTONE 5 — Módulo Service Orders

**Skill:** `/implement-module ServiceOrder`

Este es el módulo central y más complejo. Incluye reglas de negocio críticas.

### Tarea 5.1 — Domain
- Entidad `ServiceOrder`
- Entidad `ServiceOrderItem`
- Enums: `ServiceOrderType`, `ServiceOrderSubType`, `ServiceOrderStatus`, `ServiceOrderPriority`, `ServiceOrderItemType`
- Interfaces: `IServiceOrderRepository`, `IServiceOrderItemRepository`
- Lógica de dominio: método `GenerateOrderNumber()` con formato `ORD-YYYY-XXXX`
- Skill: `/scaffold-entity ServiceOrder`

### Tarea 5.2 — Application (Commands — Ciclo de vida)
- `CreateServiceOrderCommand` + Handler + Validator
  - Valida: Equipment activo, Technician activo, SubType coherente con Type (Regla negocio #5)
- `StartServiceOrderCommand` + Handler (Planned → InProgress, registra StartedAt)
- `CompleteServiceOrderCommand` + Handler
  - Registra CompletedAt
  - Si SubType = Preventive y Schedule activo → dispara generación de siguiente orden (Regla #8)
- `DelayServiceOrderCommand` + Handler (Planned → Delayed cuando ScheduledAt venció)
- `CancelServiceOrderCommand` + Handler (cualquier estado antes de Completed)
- `UpdateServiceOrderCommand` + Handler + Validator (solo en estado Planned)

### Tarea 5.3 — Application (Commands — Items)
- `AddServiceOrderItemCommand` + Handler + Validator
  - Si ItemType = Part: InventoryItemId obligatorio, descuenta stock (Reglas #3 y #6)
  - Si ItemType = Labor: InventoryItemId debe ser null (Regla #4)
  - Persiste Subtotal = Quantity × UnitPrice (Regla #6)
- `RemoveServiceOrderItemCommand` + Handler
  - Si era Part: restaura stock del InventoryItem

### Tarea 5.4 — Application (Queries)
- `GetServiceOrderByIdQuery` + Handler → `ServiceOrderDetailDto` (con items)
- `GetServiceOrderByNumberQuery` + Handler
- `GetServiceOrdersByEquipmentQuery` + Handler
- `GetServiceOrdersByTechnicianQuery` + Handler
- `GetServiceOrdersByStatusQuery` + Handler
- `GetServiceOrderTotalQuery` + Handler → suma de Subtotales

### Tarea 5.5 — Infrastructure + Migración
- `ServiceOrderConfiguration`: índice sobre OrderNumber, FK Equipment, Technician, Schedule
- `ServiceOrderItemConfiguration`: precisión decimal para UnitPrice y Subtotal
- `ServiceOrderRepository`, `ServiceOrderItemRepository`
- Skill: `/add-migration AddServiceOrders`

### Tarea 5.6 — API
- `ServiceOrdersController`:
  - `GET /api/service-orders` — Listar (filtros: status, technician, equipment)
  - `GET /api/service-orders/{id}` — Por ID con items
  - `GET /api/service-orders/number/{orderNumber}` — Por número de orden
  - `POST /api/service-orders` — Crear
  - `PUT /api/service-orders/{id}` — Actualizar (solo Planned)
  - `POST /api/service-orders/{id}/start` — Iniciar
  - `POST /api/service-orders/{id}/complete` — Completar
  - `POST /api/service-orders/{id}/delay` — Marcar como demorada
  - `POST /api/service-orders/{id}/cancel` — Cancelar
  - `POST /api/service-orders/{id}/items` — Agregar ítem
  - `DELETE /api/service-orders/{id}/items/{itemId}` — Eliminar ítem
  - `GET /api/service-orders/{id}/total` — Total de la orden
- Skill: `/api-endpoint ServiceOrder`

---

## MILESTONE 6 — Módulo Maintenance Schedule

**Skill:** `/implement-module MaintenanceSchedule`

### Tarea 6.1 — Domain
- Entidad `MaintenanceSchedule`
- Enum: `MaintenanceFrequency { Monthly=1, Quarterly=3, SemiAnnual=6, Annual=12 }`
- Interfaz: `IMaintenanceScheduleRepository`
- Regla: un equipo solo puede tener un MaintenanceSchedule activo (Regla #1)
- Skill: `/scaffold-entity MaintenanceSchedule`

### Tarea 6.2 — Application (Commands)
- `CreateMaintenanceScheduleCommand` + Handler + Validator
  - Valida que el equipo no tenga ya un schedule activo
  - Calcula NextServiceDate inicial
- `UpdateMaintenanceScheduleCommand` + Handler + Validator (frecuencia, recalcula NextServiceDate)
- `DeactivateMaintenanceScheduleCommand` + Handler

### Tarea 6.3 — Application (Servicio interno)
- `GenerateNextMaintenanceOrderService` (o DomainService)
  - Llamado desde `CompleteServiceOrderHandler` cuando SubType = Preventive
  - Actualiza LastServiceDate y NextServiceDate del Schedule
  - Crea nueva ServiceOrder con Status = Planned y ScheduledAt = NextServiceDate

### Tarea 6.4 — Application (Queries)
- `GetMaintenanceScheduleByEquipmentQuery` + Handler → `MaintenanceScheduleDto`
- `GetDueMaintenanceSchedulesQuery` + Handler → schedules cuyo NextServiceDate <= hoy
- `GetAllMaintenanceSchedulesQuery` + Handler

### Tarea 6.5 — Infrastructure + Migración
- `MaintenanceScheduleConfiguration`: índice único sobre EquipmentId (1-a-1)
- `MaintenanceScheduleRepository`
- Skill: `/add-migration AddMaintenanceSchedule`

### Tarea 6.6 — API
- `MaintenanceSchedulesController`:
  - `GET /api/maintenance-schedules` — Listar todos
  - `GET /api/maintenance-schedules/due` — Vencidos o por vencer
  - `GET /api/equipment/{equipmentId}/maintenance-schedule` — Por equipo
  - `POST /api/maintenance-schedules` — Crear
  - `PUT /api/maintenance-schedules/{id}` — Actualizar
  - `DELETE /api/maintenance-schedules/{id}` — Desactivar
- Skill: `/api-endpoint MaintenanceSchedule`

---

## MILESTONE 7 — Pruebas Unitarias

**Skill:** `/design-tests [Módulo]`

### Tarea 7.1 — Tests de Validadores (FluentValidation)
Por módulo: `CreateCustomerCommandValidatorTests`, `RegisterEquipmentCommandValidatorTests`, etc.
- Casos válidos e inválidos para cada campo
- Casos de borde: strings vacíos, nulos, longitudes máximas

### Tarea 7.2 — Tests de Handlers (Commands)
Prioridad alta:
- `CreateServiceOrderHandlerTests` — SubType coherente con Type
- `CompleteServiceOrderHandlerTests` — Generación automática de siguiente orden
- `AddServiceOrderItemHandlerTests` — Descuento de stock para Parts, Subtotal calculado
- `CancelServiceOrderHandlerTests` — Transiciones de estado válidas

### Tarea 7.3 — Tests de Handlers (Queries)
- `GetServiceOrderByIdHandlerTests`
- `GetDueMaintenanceSchedulesHandlerTests`

### Tarea 7.4 — Tests de Reglas de Negocio
- Regla #1: Un equipo con un schedule activo no acepta otro
- Regla #5: SubType inválido para Type dado → ValidationException
- Regla #6: Subtotal = Quantity × UnitPrice persiste correctamente
- Regla #8: Solo genera siguiente orden si SubType = Preventive + Schedule activo

---

## Resumen de Milestones

| # | Milestone | Skills a usar |
|---|---|---|
| 0 | Setup .NET + Git | CLI dotnet, git |
| 1 | Customers + Locations | `/implement-module Customer` |
| 2 | Equipment | `/implement-module Equipment` |
| 3 | Technicians | `/implement-module Technician` |
| 4 | Inventory | `/implement-module InventoryItem` |
| 5 | Service Orders ⭐ | `/implement-module ServiceOrder` |
| 6 | Maintenance Schedule | `/implement-module MaintenanceSchedule` |
| 7 | Pruebas Unitarias | `/design-tests [Módulo]` |

---

## Verificación Final del Proyecto

```bash
dotnet build CompuTech.sln                              # Sin errores
dotnet test CompuTech.Application.Tests                 # Todos los tests pasan
dotnet run --project CompuTech.API                      # Swagger en /swagger
# Flujo completo manual via Swagger:
# 1. Crear Customer → Location → Equipment
# 2. Crear Technician
# 3. Crear InventoryItem con stock
# 4. Crear MaintenanceSchedule para el equipo
# 5. Crear ServiceOrder (Repair) → agregar Parts y Labor → Completar
# 6. Crear ServiceOrder (Maintenance/Preventive) → Completar → verificar nueva orden generada
```
