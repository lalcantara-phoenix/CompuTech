# CompuTech API — Especificación del Proyecto

## Descripción general

**CompuTech** es una Web API REST desarrollada en **.NET Core** para gestionar órdenes de servicio técnico de una empresa de venta y mantenimiento de computadoras y laptops.

El sistema cubre el ciclo completo del servicio técnico: registro de clientes y sus equipos, gestión del inventario de piezas y productos, asignación de técnicos, programación de mantenimientos preventivos recurrentes y ejecución de órdenes de reparación y mantenimiento.

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

## Arquitectura

El proyecto sigue **Clean Architecture** organizada en cuatro capas:

```
CompuTech.sln
├── CompuTech.Domain          # Entidades, enums, interfaces del dominio
├── CompuTech.Application     # CQRS (Commands/Queries), DTOs, validaciones, behaviors
├── CompuTech.Infrastructure  # EF Core, DbContext, repositorios, configuraciones
└── CompuTech.API             # Controllers REST, middleware, DI
```

El flujo de dependencias va siempre hacia adentro: `API → Application → Domain`. Infrastructure depende de Application para implementar sus interfaces.

---

## Módulos del sistema

| Módulo | Responsabilidad |
|---|---|
| **Clientes** | Registro de clientes y sus ubicaciones |
| **Equipos** | Computadoras/laptops/servidores asociados a un cliente y ubicación |
| **Inventario** | Piezas, productos y accesorios disponibles para venta o uso en reparaciones |
| **Técnicos** | Personal técnico disponible para atender órdenes |
| **Órdenes de servicio** | Reparaciones y mantenimientos con sus ítems (piezas y mano de obra) |
| **Programación de mantenimiento** | Ciclos de mantenimiento preventivo por equipo con generación automática |

---

## Entidades y relaciones

### `Customer`
Representa a un cliente del negocio, ya sea persona física o empresa.

| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | Clave primaria |
| `FullName` | `string` | Nombre completo o razón social |
| `Email` | `string` | Correo electrónico (único) |
| `Phone` | `string` | Teléfono principal |
| `TaxId` | `string?` | RNC o cédula fiscal (opcional) |
| `IsActive` | `bool` | Indica si el cliente está activo |
| `CreatedAt` | `DateTime` | Fecha de registro |

**Relaciones:**
- Un `Customer` tiene muchas `CustomerLocation`
- Un `Customer` tiene muchos `Equipment`

---

### `CustomerLocation`
Representa una ubicación física asociada a un cliente. Un cliente puede tener múltiples sucursales o sitios.

| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | Clave primaria |
| `CustomerId` | `int` | FK → Customer |
| `Name` | `string` | Nombre de la ubicación (ej. "Sucursal Norte") |
| `Address` | `string` | Dirección física |
| `City` | `string` | Ciudad |
| `ContactPhone` | `string?` | Teléfono de contacto en esta ubicación |
| `IsActive` | `bool` | Indica si la ubicación está activa |

**Relaciones:**
- Una `CustomerLocation` pertenece a un `Customer`
- Una `CustomerLocation` tiene muchos `Equipment`

---

### `Equipment`
Representa un equipo físico registrado bajo un cliente, ubicado en una de sus locaciones.

| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | Clave primaria |
| `CustomerId` | `int` | FK → Customer |
| `LocationId` | `int` | FK → CustomerLocation |
| `SerialNumber` | `string` | Número de serie (único) |
| `Brand` | `string` | Marca del equipo |
| `Model` | `string` | Modelo del equipo |
| `Type` | `EquipmentType` | **Enum:** Desktop, Laptop, Server, Workstation |
| `OperatingSystem` | `string?` | Sistema operativo instalado |
| `PurchaseDate` | `DateTime?` | Fecha de compra (opcional) |
| `Notes` | `string?` | Observaciones generales del equipo |
| `IsActive` | `bool` | Indica si el equipo está activo en el sistema |

**Relaciones:**
- Un `Equipment` pertenece a un `Customer`
- Un `Equipment` pertenece a una `CustomerLocation`
- Un `Equipment` tiene muchas `ServiceOrder`
- Un `Equipment` tiene a lo sumo un `MaintenanceSchedule`

---

### `Technician`
Representa un técnico del equipo de trabajo capaz de atender cualquier tipo de orden.

| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | Clave primaria |
| `FullName` | `string` | Nombre completo |
| `Email` | `string` | Correo electrónico (único) |
| `Phone` | `string` | Teléfono de contacto |
| `IsActive` | `bool` | Indica si el técnico está disponible |
| `CreatedAt` | `DateTime` | Fecha de registro |

**Relaciones:**
- Un `Technician` puede tener muchas `ServiceOrder` asignadas

---

### `InventoryItem`
Catálogo unificado de productos, piezas y accesorios disponibles tanto para venta directa como para uso en reparaciones.

| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | Clave primaria |
| `SKU` | `string` | Código único del ítem |
| `Name` | `string` | Nombre descriptivo |
| `Type` | `InventoryItemType` | **Enum:** Product, Part, Accessory |
| `Category` | `InventoryCategory` | **Enum:** Processor, Memory, Storage, GraphicsCard, Monitor, Keyboard, Mouse, Speaker, Case, Motherboard, PowerSupply, CoolingSystem, NetworkDevice, ComputerSystem, Other |
| `Stock` | `int` | Cantidad disponible en inventario |
| `SalePrice` | `decimal` | Precio de venta unitario |
| `Description` | `string?` | Descripción técnica del ítem |
| `IsActive` | `bool` | Indica si el ítem está disponible |

**Relaciones:**
- Un `InventoryItem` puede aparecer en muchos `ServiceOrderItem` (cuando `ItemType = Part`)

---

### `ServiceOrder`
Entidad central del sistema. Representa una orden de servicio técnico, ya sea una reparación o un mantenimiento. Unifica ambos conceptos mediante `Type` y `SubType`.

| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | Clave primaria |
| `OrderNumber` | `string` | Número de orden legible (ej. `ORD-2024-0001`) |
| `EquipmentId` | `int` | FK → Equipment |
| `TechnicianId` | `int` | FK → Technician |
| `ScheduleId` | `int?` | FK → MaintenanceSchedule (null si es a demanda) |
| `Type` | `ServiceOrderType` | **Enum:** Repair, Maintenance |
| `SubType` | `ServiceOrderSubType` | **Enum:** Hardware, Software, Diagnostic, Preventive, OnDemand |
| `Status` | `ServiceOrderStatus` | **Enum:** Planned, InProgress, Completed, Delayed, Cancelled |
| `Priority` | `ServiceOrderPriority` | **Enum:** Low, Medium, High, Critical |
| `Description` | `string` | Descripción del problema o servicio requerido |
| `DiagnosisNotes` | `string?` | Notas del diagnóstico técnico |
| `ResolutionNotes` | `string?` | Notas de la resolución aplicada |
| `ScheduledAt` | `DateTime?` | Fecha y hora programada para el servicio |
| `StartedAt` | `DateTime?` | Fecha y hora de inicio real |
| `CompletedAt` | `DateTime?` | Fecha y hora de cierre |
| `CreatedAt` | `DateTime` | Fecha de creación de la orden |

**Ciclo de estados:**
```
Planned → InProgress → Completed
                     → Delayed      (cuando ScheduledAt vence sin iniciarse)
        → Cancelled                 (en cualquier momento antes de Completed)
```

**Relaciones:**
- Una `ServiceOrder` pertenece a un `Equipment`
- Una `ServiceOrder` es atendida por un `Technician`
- Una `ServiceOrder` puede originarse desde un `MaintenanceSchedule` (nullable)
- Una `ServiceOrder` tiene muchos `ServiceOrderItem`

---

### `ServiceOrderItem`
Representa una línea de detalle dentro de una orden de servicio. Puede ser una pieza física extraída del inventario o una línea de mano de obra registrada por el técnico.

| Campo | Tipo | Aplica a `Part` | Aplica a `Labor` |
|---|---|---|---|
| `Id` | `int` | ✅ | ✅ |
| `ServiceOrderId` | `int` (FK) | ✅ | ✅ |
| `ItemType` | `ServiceOrderItemType` (Enum) | `Part` | `Labor` |
| `InventoryItemId` | `int?` (FK, nullable) | ✅ Requerido | ❌ Null |
| `Description` | `string` | Nombre del ítem | Descripción del trabajo realizado |
| `Quantity` | `decimal` | Unidades usadas | Horas trabajadas |
| `UnitPrice` | `decimal` | Precio unitario del ítem | Tarifa por hora del técnico |
| `Subtotal` | `decimal` (calculado) | `Quantity × UnitPrice` | `Quantity × UnitPrice` |
| `Notes` | `string?` | Observación de la pieza | Observación del trabajo |

**Ejemplo de líneas en una orden:**

```
Orden #ORD-2024-0042 — Reparación laptop HP
├── [Part]  1x  SSD 500GB Samsung        $3,500.00
├── [Part]  2x  Memoria RAM 8GB Kingston  $2,400.00
└── [Labor] 3h  Instalación y config      $1,500.00
                                   Total: $7,400.00
```

**Relaciones:**
- Un `ServiceOrderItem` pertenece a una `ServiceOrder`
- Un `ServiceOrderItem` de tipo `Part` referencia a un `InventoryItem`

---

### `MaintenanceSchedule`
Define la programación de mantenimiento preventivo recurrente para un equipo. Cuando una orden de mantenimiento preventivo se completa, el sistema genera automáticamente la siguiente orden con base en la frecuencia configurada.

| Campo | Tipo | Descripción |
|---|---|---|
| `Id` | `int` | Clave primaria |
| `EquipmentId` | `int` | FK → Equipment (único por equipo) |
| `Frequency` | `MaintenanceFrequency` | **Enum:** Monthly (1), Quarterly (3), SemiAnnual (6), Annual (12) |
| `LastServiceDate` | `DateTime?` | Fecha del último mantenimiento completado |
| `NextServiceDate` | `DateTime` | Fecha calculada del próximo mantenimiento |
| `IsActive` | `bool` | Indica si la programación está vigente |
| `CreatedAt` | `DateTime` | Fecha de creación |

**Comportamiento de generación automática:**
Al completar una `ServiceOrder` de tipo `Maintenance/Preventive`, el sistema ejecuta:
1. Actualiza `LastServiceDate = CompletedAt` de la orden completada
2. Calcula `NextServiceDate = LastServiceDate + Frequency (meses)`
3. Crea una nueva `ServiceOrder` con `Status = Planned` y `ScheduledAt = NextServiceDate`

**Relaciones:**
- Un `MaintenanceSchedule` pertenece a un `Equipment` (relación 1-a-1)
- Un `MaintenanceSchedule` puede originar muchas `ServiceOrder` a lo largo del tiempo

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

## Resumen de relaciones

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

## Reglas de negocio clave

1. Un equipo solo puede tener **un** `MaintenanceSchedule` activo a la vez.
2. Una `ServiceOrder` solo puede tener **un** técnico asignado.
3. Si `ItemType = Part`, el campo `InventoryItemId` es **obligatorio** y el stock del `InventoryItem` se descuenta al registrar el ítem.
4. Si `ItemType = Labor`, el campo `InventoryItemId` es **siempre null**.
5. `SubType` debe ser coherente con `Type`: si `Type = Repair`, los subtypes válidos son `Hardware`, `Software`, `Diagnostic`; si `Type = Maintenance`, los válidos son `Preventive` y `OnDemand`.
6. El `Subtotal` de cada `ServiceOrderItem` es siempre `Quantity × UnitPrice` y se persiste (no se recalcula en consulta).
7. La transición a estado `Delayed` ocurre cuando `ScheduledAt` ha vencido y el estado sigue en `Planned`.
8. Solo se genera la próxima orden automática cuando `SubType = Preventive` y el `MaintenanceSchedule` tiene `IsActive = true`.
