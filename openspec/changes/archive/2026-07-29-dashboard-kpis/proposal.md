## Por qué

El sistema CompuTech no ofrece actualmente una vista consolidada del estado operativo del negocio. Los supervisores deben consultar endpoint por endpoint para conocer cuántas órdenes están activas, qué técnicos están sobrecargados o qué repuestos están por agotarse. Centralizar estas métricas en un único endpoint elimina ese trabajo manual y sienta las bases para un panel de control.

## Qué cambia

- Se agrega un nuevo endpoint `GET /api/dashboard` que devuelve un snapshot del estado operativo actual.
- El endpoint agrega datos de cuatro módulos existentes sin modificar ninguno de ellos.
- No se crean entidades, migraciones ni cambios al dominio.
- Las métricas incluidas son:
  - **Órdenes por estado**: conteo de ServiceOrders agrupadas por `ServiceOrderStatus` (Planned, InProgress, Delayed, Completed, Cancelled).
  - **Técnicos con más órdenes activas**: top-5 técnicos ordenados por cantidad de órdenes en estado InProgress o Planned.
  - **Ítems de inventario bajo stock mínimo**: InventoryItems cuyo stock actual está por debajo de un umbral configurable (default: 5 unidades).
  - **Schedules de mantenimiento vencidos o próximos**: MaintenanceSchedules cuyo `NextServiceDate` es anterior a hoy o cae dentro de los próximos 30 días.

## Capabilities

### Nuevas capabilities

- `dashboard-kpis`: Endpoint de solo lectura que agrega métricas operativas cruzando los módulos ServiceOrders, Technicians, Inventory y MaintenanceSchedule.

### Capabilities modificadas

_(ninguna — este cambio no altera requisitos existentes de ningún módulo)_

## No-goals

- No se implementa autenticación ni autorización sobre el endpoint.
- No se persiste ningún dato calculado (no hay caché ni tabla de snapshots).
- No se incluyen métricas financieras (totales de facturación, ingresos por técnico).
- No se construye un frontend — el endpoint expone JSON para consumo externo.
- No se modifican ni extienden los repositorios existentes con lógica de negocio nueva.

## Impacto

| Área | Detalle |
|---|---|
| **Módulos afectados (lectura)** | ServiceOrders, Technicians, Inventory, MaintenanceSchedule |
| **Capas nuevas** | Application (Query + Handler + DTO), API (Controller o endpoint en DashboardController) |
| **Capas sin cambios** | Domain, Infrastructure, repositorios existentes |
| **Dependencias** | `IServiceOrderRepository`, `ITechnicianRepository`, `IInventoryItemRepository`, `IMaintenanceScheduleRepository` |
| **Migración EF Core** | No requerida |
| **Tests** | Un test del handler con cuatro repositorios mockeados |
