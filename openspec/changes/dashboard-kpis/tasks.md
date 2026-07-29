## 1. Domain — Interfaces de repositorio

- [x] 1.1 Agregar `GetCountsByStatusAsync()` a `IServiceOrderRepository` — retorna conteos por cada `ServiceOrderStatus`
- [x] 1.2 Agregar `GetTopByActiveOrdersAsync(int top)` a `ITechnicianRepository` — retorna top N técnicos con más órdenes en Planned o InProgress
- [x] 1.3 Agregar `GetBelowStockThresholdAsync(int threshold)` a `IInventoryItemRepository` — retorna ítems con stock <= threshold
- [x] 1.4 Verificar si `IMaintenanceScheduleRepository.GetDueAsync()` ya cubre "próximos 30 días"; si no, agregar `GetDueWithinDaysAsync(int days)`

## 2. Application — Query, DTO y Handler

- [x] 2.1 Crear `src/CompuTech.Application/Dashboard/Queries/GetDashboard/GetDashboardQuery.cs` — record que implementa `IRequest<DashboardDto>`
- [x] 2.2 Crear `src/CompuTech.Application/Dashboard/DTOs/DashboardDto.cs` con las cuatro secciones: `OrdersByStatus`, `TopTechnicians`, `LowStockItems`, `DueMaintenanceSchedules`
- [x] 2.3 Crear DTOs de soporte: `OrderStatusCountDto`, `TechnicianWorkloadDto`, `LowStockItemDto`, `DueScheduleDto`
- [x] 2.4 Crear `src/CompuTech.Application/Dashboard/Queries/GetDashboard/GetDashboardQueryHandler.cs` — inyecta los cuatro repositorios, ejecuta las cuatro consultas y compone `DashboardDto`

## 3. Infrastructure — Implementación de métodos nuevos

- [x] 3.1 Implementar `GetCountsByStatusAsync()` en `ServiceOrderRepository` — query EF Core con `GroupBy` sobre `Status`
- [x] 3.2 Implementar `GetTopByActiveOrdersAsync(int top)` en `TechnicianRepository` — join con ServiceOrders, filtro por status, `OrderByDescending`, `Take(top)`
- [x] 3.3 Implementar `GetBelowStockThresholdAsync(int threshold)` en `InventoryItemRepository` — filtro `Where(x => x.Stock <= threshold)`
- [x] 3.4 Implementar o verificar `GetDueWithinDaysAsync(int days)` en `MaintenanceScheduleRepository` — filtro `Where(x => x.IsActive && x.NextServiceDate <= DateTime.UtcNow.AddDays(days))`

## 4. API — Controller

- [x] 4.1 Crear `src/CompuTech.API/Controllers/DashboardController.cs` con un único endpoint `GET /api/dashboard` (nombre de ruta: `"GetDashboard"`) que despacha `GetDashboardQuery` via IMediator y retorna HTTP 200

## 5. Tests

- [x] 5.1 Crear `tests/CompuTech.Application.Tests/Dashboard/GetDashboardQueryHandlerTests.cs` con tests que cubran:
  - Respuesta correcta cuando hay datos en los cuatro repositorios
  - Respuesta con secciones vacías cuando no hay datos (sin excepciones)
  - Conteo correcto de órdenes por estado
  - Top técnicos ordenados correctamente
  - Exclusión de ítems sobre el umbral de stock
  - Exclusión de schedules inactivos o con fecha lejana
