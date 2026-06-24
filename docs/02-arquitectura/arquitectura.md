# CompuTech API — Arquitectura del Sistema

**Fecha de registro:** 2026-06-23

---

## Patrón arquitectónico

**Clean Architecture + CQRS** con MediatR como mediador de comandos y queries.

### Principio de dependencias
```
API → Application → Domain
Infrastructure → Application (implementa interfaces del dominio)
```

Ninguna capa interna conoce las capas externas.

---

## Capas del proyecto

### `CompuTech.Domain`
- Entidades del negocio (Customer, Equipment, ServiceOrder, etc.)
- Enums del dominio
- Interfaces de repositorios (contratos)
- Sin dependencias externas

### `CompuTech.Application`
- Handlers de Commands y Queries (MediatR)
- DTOs de entrada y salida
- Validaciones con FluentValidation
- Pipeline behaviors (validación, logging)
- Interfaces de servicios de aplicación

### `CompuTech.Infrastructure`
- DbContext (Entity Framework Core)
- Implementaciones de repositorios
- Configuraciones de entidades (Fluent API)
- Migraciones de base de datos

### `CompuTech.API`
- Controllers REST
- Configuración de middleware
- Registro de dependencias (DI)
- Punto de entrada de la aplicación

---

## Flujo de una solicitud

```
HTTP Request
    │
    ▼
Controller (API)
    │  envía Command/Query via MediatR
    ▼
Pipeline Behavior (validación FluentValidation)
    │
    ▼
Handler (Application)
    │  usa interfaz de repositorio
    ▼
Repository (Infrastructure)
    │  EF Core
    ▼
SQL Server LocalDB
```

---

## Módulos y sus responsabilidades CQRS

| Módulo | Commands | Queries |
|---|---|---|
| Customers | Create, Update, Deactivate | GetById, GetAll, GetLocations |
| Equipment | Register, Update, Deactivate | GetById, GetByCustomer, GetBySerial |
| Inventory | AddItem, UpdateStock, Deactivate | GetById, GetAll, GetLowStock |
| Technicians | Create, Update, Deactivate | GetById, GetAll, GetAvailable |
| ServiceOrders | Create, UpdateStatus, AddItem, Complete | GetById, GetAll, GetByEquipment, GetByTechnician |
| MaintenanceSchedule | Create, Update, Deactivate | GetByEquipment, GetDue |
