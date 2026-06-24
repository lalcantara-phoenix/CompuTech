# CompuTech API — Especificación del Proyecto

**Fecha de registro:** 2026-06-23  
**Fuente:** `CompuTech-ProjectSpec.md`

---

## Stack tecnológico

| Capa | Tecnología |
|---|---|
| Framework | .NET Core — Web API REST |
| Arquitectura | Clean Architecture + CQRS |
| Mediador | MediatR |
| ORM | Entity Framework Core |
| Base de datos | SQL Server LocalDB |
| Validaciones | FluentValidation |

---

## Estructura de solución

```
CompuTech.sln
├── CompuTech.Domain          # Entidades, enums, interfaces del dominio
├── CompuTech.Application     # CQRS (Commands/Queries), DTOs, validaciones, behaviors
├── CompuTech.Infrastructure  # EF Core, DbContext, repositorios, configuraciones
└── CompuTech.API             # Controllers REST, middleware, DI
```

**Flujo de dependencias:** `API → Application → Domain`  
Infrastructure depende de Application para implementar sus interfaces.

---

## Módulos del sistema

| Módulo | Responsabilidad |
|---|---|
| Clientes | Registro de clientes y sus ubicaciones |
| Equipos | Computadoras/laptops/servidores asociados a cliente y ubicación |
| Inventario | Piezas, productos y accesorios para venta o uso en reparaciones |
| Técnicos | Personal técnico disponible para atender órdenes |
| Órdenes de servicio | Reparaciones y mantenimientos con ítems (piezas y mano de obra) |
| Programación de mantenimiento | Ciclos preventivos con generación automática de próxima orden |

---

## Entidades

### Customer
| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | PK |
| `FullName` | `string` | Nombre completo o razón social |
| `Email` | `string` | Correo único |
| `Phone` | `string` | Teléfono principal |
| `TaxId` | `string?` | RNC o cédula fiscal |
| `IsActive` | `bool` | Cliente activo |
| `CreatedAt` | `DateTime` | Fecha de registro |

### CustomerLocation
| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | PK |
| `CustomerId` | `int` | FK → Customer |
| `Name` | `string` | Nombre de la ubicación |
| `Address` | `string` | Dirección física |
| `City` | `string` | Ciudad |
| `ContactPhone` | `string?` | Teléfono de contacto |
| `IsActive` | `bool` | Ubicación activa |

### Equipment
| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | PK |
| `CustomerId` | `int` | FK → Customer |
| `LocationId` | `int` | FK → CustomerLocation |
| `SerialNumber` | `string` | Número de serie (único) |
| `Brand` | `string` | Marca |
| `Model` | `string` | Modelo |
| `Type` | `EquipmentType` | Desktop, Laptop, Server, Workstation |
| `OperatingSystem` | `string?` | SO instalado |
| `PurchaseDate` | `DateTime?` | Fecha de compra |
| `Notes` | `string?` | Observaciones |
| `IsActive` | `bool` | Activo en el sistema |

### Technician
| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | PK |
| `FullName` | `string` | Nombre completo |
| `Email` | `string` | Correo único |
| `Phone` | `string` | Teléfono |
| `IsActive` | `bool` | Disponible |
| `CreatedAt` | `DateTime` | Fecha de registro |

### InventoryItem
| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | PK |
| `SKU` | `string` | Código único |
| `Name` | `string` | Nombre descriptivo |
| `Type` | `InventoryItemType` | Product, Part, Accessory |
| `Category` | `InventoryCategory` | Ver enum |
| `Stock` | `int` | Cantidad disponible |
| `SalePrice` | `decimal` | Precio de venta |
| `Description` | `string?` | Descripción técnica |
| `IsActive` | `bool` | Disponible |

### ServiceOrder
| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | PK |
| `OrderNumber` | `string` | Número legible (ej. ORD-2024-0001) |
| `EquipmentId` | `int` | FK → Equipment |
| `TechnicianId` | `int` | FK → Technician |
| `ScheduleId` | `int?` | FK → MaintenanceSchedule (null si a demanda) |
| `Type` | `ServiceOrderType` | Repair, Maintenance |
| `SubType` | `ServiceOrderSubType` | Hardware, Software, Diagnostic, Preventive, OnDemand |
| `Status` | `ServiceOrderStatus` | Planned, InProgress, Completed, Delayed, Cancelled |
| `Priority` | `ServiceOrderPriority` | Low, Medium, High, Critical |
| `Description` | `string` | Descripción del problema/servicio |
| `DiagnosisNotes` | `string?` | Notas de diagnóstico |
| `ResolutionNotes` | `string?` | Notas de resolución |
| `ScheduledAt` | `DateTime?` | Fecha programada |
| `StartedAt` | `DateTime?` | Fecha de inicio real |
| `CompletedAt` | `DateTime?` | Fecha de cierre |
| `CreatedAt` | `DateTime` | Fecha de creación |

**Ciclo de estados:**
```
Planned → InProgress → Completed
                     → Delayed
        → Cancelled
```

### ServiceOrderItem
| Campo | Aplica a Part | Aplica a Labor |
|---|---|---|
| `Id` | ✅ | ✅ |
| `ServiceOrderId` | ✅ | ✅ |
| `ItemType` | `Part` | `Labor` |
| `InventoryItemId` | ✅ Requerido | ❌ Null |
| `Description` | Nombre del ítem | Trabajo realizado |
| `Quantity` | Unidades | Horas |
| `UnitPrice` | Precio unitario | Tarifa/hora |
| `Subtotal` | Quantity × UnitPrice | Quantity × UnitPrice |
| `Notes` | `string?` | `string?` |

### MaintenanceSchedule
| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | PK |
| `EquipmentId` | `int` | FK → Equipment (único) |
| `Frequency` | `MaintenanceFrequency` | Monthly(1), Quarterly(3), SemiAnnual(6), Annual(12) |
| `LastServiceDate` | `DateTime?` | Último mantenimiento completado |
| `NextServiceDate` | `DateTime` | Próximo mantenimiento calculado |
| `IsActive` | `bool` | Programación vigente |
| `CreatedAt` | `DateTime` | Fecha de creación |

---

## Enums

```csharp
enum EquipmentType         { Desktop, Laptop, Server, Workstation }
enum ServiceOrderType      { Repair, Maintenance }
enum ServiceOrderSubType   { Hardware, Software, Diagnostic, Preventive, OnDemand }
enum ServiceOrderStatus    { Planned, InProgress, Completed, Delayed, Cancelled }
enum ServiceOrderPriority  { Low, Medium, High, Critical }
enum ServiceOrderItemType  { Part, Labor }
enum InventoryItemType     { Product, Part, Accessory }
enum InventoryCategory     { ComputerSystem, Processor, Memory, Storage, GraphicsCard,
                             Motherboard, PowerSupply, Case, CoolingSystem, Monitor,
                             Keyboard, Mouse, Speaker, NetworkDevice, Other }
enum MaintenanceFrequency  { Monthly = 1, Quarterly = 3, SemiAnnual = 6, Annual = 12 }
```

---

## Relaciones

```
Customer           1 ──── N   CustomerLocation
Customer           1 ──── N   Equipment
CustomerLocation   1 ──── N   Equipment
Equipment          1 ──── N   ServiceOrder
Equipment          1 ──── 1   MaintenanceSchedule
Technician         1 ──── N   ServiceOrder
MaintenanceSchedule 1 ─── N   ServiceOrder
ServiceOrder       1 ──── N   ServiceOrderItem
InventoryItem      1 ──── N   ServiceOrderItem   (solo ItemType = Part)
```

---

## Reglas de negocio

1. Un equipo solo puede tener **un** `MaintenanceSchedule` activo a la vez.
2. Una `ServiceOrder` solo puede tener **un** técnico asignado.
3. Si `ItemType = Part`, `InventoryItemId` es obligatorio y descuenta stock.
4. Si `ItemType = Labor`, `InventoryItemId` es siempre null.
5. `SubType` coherente con `Type`: Repair → Hardware/Software/Diagnostic; Maintenance → Preventive/OnDemand.
6. `Subtotal` = `Quantity × UnitPrice`, se persiste (no se recalcula en consulta).
7. Transición a `Delayed` cuando `ScheduledAt` vence con estado `Planned`.
8. Generación automática de próxima orden solo cuando `SubType = Preventive` y `MaintenanceSchedule.IsActive = true`.
