## Context

CompuTech sigue Clean Architecture + CQRS. Cada caso de uso vive en `Application/<Módulo>/Queries/<Nombre>/`. Los cuatro repositorios necesarios (`IServiceOrderRepository`, `ITechnicianRepository`, `IInventoryItemRepository`, `IMaintenanceScheduleRepository`) ya existen y están registrados en DI. Ver proposal.md para la motivación del cambio.

## Goals / Non-Goals

**Goals:**
- Un único query handler que agrega datos de cuatro repositorios sin tocar ninguno de ellos.
- Un controller dedicado `DashboardController` con un solo endpoint GET.
- DTO de respuesta plano y serializable (`DashboardDto`).

**Non-Goals:**
- Caché o persistencia de los resultados calculados.
- Modificar métodos existentes en repositorios.
- Paginación o filtros sobre las métricas.

## Decisions

### 1. Módulo de Application: `Dashboard` independiente

**Decisión:** Crear `Application/Dashboard/Queries/GetDashboard/` como módulo propio, no colocarlo en ninguno de los módulos existentes.

**Razón:** El query cruza cuatro módulos. Colocarlo dentro de ServiceOrders, Inventory u otro generaría dependencia implícita entre módulos en la capa Application, violando la separación de responsabilidades.

**Alternativa descartada:** Agregar el query dentro de `ServiceOrders/Queries/` — descartado porque la respuesta no es una ServiceOrder sino un agregado multi-módulo.

### 2. Cuatro repositorios inyectados en un solo handler

**Decisión:** `GetDashboardQueryHandler` recibe los cuatro repositorios via constructor injection. El handler ejecuta las cuatro consultas y compone el DTO.

**Razón:** Es la forma estándar del proyecto para handlers que necesitan múltiples fuentes. No justifica un servicio de aplicación intermedio dado que no hay lógica de negocio compleja, solo agregación de datos.

**Alternativa descartada:** Servicios de dominio intermedios — añaden indirección sin valor para un caso de solo lectura.

### 3. Métodos de repositorio necesarios

Los repositorios existentes no tienen los métodos de agregación requeridos. Se deben agregar:

| Repositorio | Método nuevo |
|---|---|
| `IServiceOrderRepository` | `GetCountsByStatusAsync()` → `IReadOnlyList<(ServiceOrderStatus Status, int Count)>` |
| `ITechnicianRepository` | `GetTopByActiveOrdersAsync(int top)` → `IReadOnlyList<(int TechnicianId, string FullName, int ActiveOrderCount)>` |
| `IInventoryItemRepository` | `GetBelowStockThresholdAsync(int threshold)` → `IReadOnlyList<InventoryItem>` |
| `IMaintenanceScheduleRepository` | `GetDueWithinDaysAsync(int days)` → ya existe `GetDueAsync()`, revisar si cubre el caso |

**Razón:** Los repositorios existentes solo tienen `GetByIdAsync`, `GetAllAsync` y similares. Traer todas las entidades al handler para filtrarlas en memoria sería ineficiente; las consultas deben ejecutarse en la base de datos.

### 4. Umbral de stock como constante

**Decisión:** El umbral de stock bajo (5 unidades) se define como constante en el handler, no como parámetro del endpoint ni como configuración en appsettings.

**Razón:** Mantiene el endpoint sin parámetros (simplicidad del spec). Puede refactorizarse a configuración en el futuro sin cambiar el contrato del endpoint.

### 5. Ventana de mantenimiento: 30 días calendario

**Decisión:** "Próximos" significa `NextServiceDate <= DateTime.UtcNow.AddDays(30)`.

**Razón:** Valor fijo que cubre un ciclo mensual completo, suficiente para planificación operativa básica.

## Risks / Trade-offs

- **Cuatro queries en una sola llamada** → Si el volumen de datos crece, la latencia del endpoint crecerá proporcionalmente. Mitigación: los métodos nuevos en repositorios deben usar proyecciones SQL, no cargar entidades completas en memoria.
- **Repositorios ampliados** → Agregar métodos a las interfaces existentes requiere implementarlos en las clases de repositorio concretas y potencialmente en los mocks de tests. Impacto bajo para tests existentes porque los mocks no son exhaustivos.
- **Sin caché** → Peticiones frecuentes al dashboard ejecutan cuatro queries cada vez. Aceptable en el contexto académico; en producción se recomendaría caché de corta duración (ej. 60 segundos).
