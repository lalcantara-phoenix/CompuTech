## Purpose

Permite consultar en una sola llamada el estado operativo actual del negocio, agregando métricas de órdenes, técnicos, inventario y mantenimiento preventivo.

## Requirements

### Requirement: Endpoint de dashboard disponible

El sistema SHALL exponer `GET /api/dashboard` sin parámetros obligatorios. La respuesta SHALL ser HTTP 200 con un objeto JSON que contenga las cuatro secciones de métricas definidas en este spec.

#### Scenario: Consulta exitosa con datos existentes
- **WHEN** un cliente hace GET /api/dashboard y existen registros en todos los módulos
- **THEN** el sistema responde HTTP 200 con las cuatro secciones de métricas pobladas

#### Scenario: Consulta exitosa sin datos
- **WHEN** un cliente hace GET /api/dashboard y la base de datos está vacía
- **THEN** el sistema responde HTTP 200 con las cuatro secciones con valores en cero o listas vacías (nunca 404 ni 500)

### Requirement: Órdenes agrupadas por estado

El sistema SHALL incluir en la respuesta un conteo de ServiceOrders agrupadas por cada valor de `ServiceOrderStatus` (Planned, InProgress, Delayed, Completed, Cancelled). Cada grupo SHALL indicar el nombre del estado y la cantidad de órdenes.

#### Scenario: Conteo correcto por estado
- **WHEN** existen 3 órdenes Planned, 2 InProgress y 1 Completed
- **THEN** la sección `ordersByStatus` contiene exactamente esos tres grupos con sus conteos

#### Scenario: Estado sin órdenes no se omite
- **WHEN** no existen órdenes en estado Cancelled
- **THEN** el estado Cancelled aparece en la respuesta con conteo 0

### Requirement: Top técnicos por carga activa

El sistema SHALL incluir los 5 técnicos con mayor cantidad de órdenes en estado InProgress o Planned, ordenados de mayor a menor. Cada entrada SHALL incluir el ID, nombre completo del técnico y el conteo de órdenes activas.

#### Scenario: Ranking correcto
- **WHEN** el técnico A tiene 4 órdenes activas y el técnico B tiene 2
- **THEN** el técnico A aparece primero en la lista

#### Scenario: Menos de 5 técnicos con órdenes activas
- **WHEN** solo 2 técnicos tienen órdenes activas
- **THEN** la lista contiene únicamente esos 2 técnicos

### Requirement: Ítems de inventario bajo stock mínimo

El sistema SHALL incluir todos los InventoryItems cuyo stock actual sea menor o igual a un umbral. El umbral por defecto SHALL ser 5 unidades. Cada entrada SHALL incluir ID, nombre, SKU y stock actual.

#### Scenario: Ítems bajo umbral incluidos
- **WHEN** un ítem tiene stock 3 y el umbral es 5
- **THEN** ese ítem aparece en la sección `lowStockItems`

#### Scenario: Ítems sobre umbral excluidos
- **WHEN** un ítem tiene stock 6 y el umbral es 5
- **THEN** ese ítem NO aparece en `lowStockItems`

### Requirement: Schedules de mantenimiento vencidos o próximos

El sistema SHALL incluir todos los MaintenanceSchedules activos cuyo `NextServiceDate` sea anterior a la fecha actual o caiga dentro de los próximos 30 días calendario. Cada entrada SHALL incluir ID, ID de equipo, frecuencia y fecha de próximo servicio.

#### Scenario: Schedule vencido incluido
- **WHEN** un schedule activo tiene NextServiceDate anterior a hoy
- **THEN** ese schedule aparece en `dueMaintenanceSchedules`

#### Scenario: Schedule próximo a vencer incluido
- **WHEN** un schedule activo tiene NextServiceDate 15 días en el futuro
- **THEN** ese schedule aparece en `dueMaintenanceSchedules`

#### Scenario: Schedule futuro lejano excluido
- **WHEN** un schedule activo tiene NextServiceDate 45 días en el futuro
- **THEN** ese schedule NO aparece en `dueMaintenanceSchedules`

#### Scenario: Schedule inactivo excluido
- **WHEN** un schedule tiene IsActive = false y NextServiceDate es hoy
- **THEN** ese schedule NO aparece en `dueMaintenanceSchedules`
