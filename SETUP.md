# CompuTech API — Guía de Setup

Guía completa para clonar el repositorio, configurar el entorno, ejecutar las pruebas y usar la API desde cero. Diseñada para ser ejecutada por un humano o por un agente de IA paso a paso.

---

## Requisitos previos

| Herramienta | Versión mínima | Verificar |
|---|---|---|
| Git | Cualquiera | `git --version` |
| .NET SDK | **10.0** | `dotnet --version` |
| SQL Server LocalDB | Incluido con Visual Studio | `sqllocaldb info` |
| dotnet-ef (global) | 10.x | `dotnet ef --version` |

### Instalar dotnet-ef si no está disponible

```powershell
dotnet tool install --global dotnet-ef
```

> **LocalDB** viene con Visual Studio 2022. Si no tienes VS instalado, descarga el [SQL Server Express LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb) por separado.

---

## 1. Clonar el repositorio

```powershell
git clone https://github.com/lalcantara-phoenix/CompuTech.git
cd CompuTech
```

El repositorio tiene dos ramas principales:
- `main` — producción (rama por defecto)
- `testing` — integración

---

## 2. Restaurar dependencias y compilar

```powershell
dotnet restore CompuTech.slnx
dotnet build CompuTech.slnx
```

Salida esperada: `Build succeeded. 0 Error(s)`

---

## 3. Crear la base de datos y aplicar migraciones

La API usa **SQL Server LocalDB** con la base de datos `CompuTechDb`. La cadena de conexión está configurada en `src/CompuTech.API/appsettings.json`:

```
Server=(localdb)\mssqllocaldb;Database=CompuTechDb;Trusted_Connection=True;
```

Aplicar todas las migraciones (crea la BD automáticamente si no existe):

```powershell
dotnet ef database update `
  --project src/CompuTech.Infrastructure/CompuTech.Infrastructure.csproj `
  --startup-project src/CompuTech.API/CompuTech.API.csproj
```

Las migraciones aplicadas son (en orden):
1. `AddCustomerAndLocation`
2. `AddEquipment`
3. `AddTechnician`
4. `AddInventoryItem`
5. `AddServiceOrders`
6. `AddMaintenanceSchedule`

---

## 4. Ejecutar las pruebas unitarias

```powershell
dotnet test tests/CompuTech.Application.Tests/CompuTech.Application.Tests.csproj
```

Salida esperada: **44 tests passed, 0 failed**

Las pruebas cubren:
- Reglas de negocio de validadores (FluentValidation)
- Handlers de comandos: `CreateMaintenanceSchedule`, `CompleteServiceOrder`, `CancelServiceOrder`, `AddServiceOrderItem`
- Generación automática de siguiente orden preventiva (`GenerateNextMaintenanceOrderService`)

---

## 5. Iniciar la API

```powershell
dotnet run --project src/CompuTech.API/CompuTech.API.csproj
```

La API inicia en: **`http://localhost:5254`**

Swagger UI disponible en: **`http://localhost:5254/swagger`**

> Para detener la API: `Ctrl+C`

---

## 6. Demo completa — Flujo de trabajo end-to-end

Los comandos siguientes usan PowerShell con `Invoke-RestMethod`. Ejecutarlos en orden mientras la API está corriendo.

### 6.1 Crear cliente

```powershell
$customer = Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/customers" `
  -ContentType "application/json" `
  -Body '{
    "fullName": "Empresa Demo S.A.",
    "email": "demo@empresa.com",
    "phone": "809-555-0001",
    "taxId": "RNC-123456789"
  }'

$customerId = $customer.id
Write-Host "Customer ID: $customerId"
```

### 6.2 Agregar ubicación al cliente

```powershell
$location = Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/customers/$customerId/locations" `
  -ContentType "application/json" `
  -Body '{
    "name": "Sede Principal",
    "address": "Av. Independencia 123",
    "city": "Santo Domingo",
    "contactPhone": "809-555-0002"
  }'

$locationId = $location.id
Write-Host "Location ID: $locationId"
```

### 6.3 Registrar equipo

Tipos válidos: `Desktop`, `Laptop`, `Server`, `Workstation`

```powershell
$equipment = Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/equipment" `
  -ContentType "application/json" `
  -Body "{
    `"customerId`": $customerId,
    `"locationId`": $locationId,
    `"serialNumber`": `"SN-DEMO-2026-001`",
    `"brand`": `"Dell`",
    `"model`": `"OptiPlex 7090`",
    `"type`": `"Desktop`",
    `"operatingSystem`": `"Windows 11 Pro`",
    `"purchaseDate`": `"2024-01-15T00:00:00`",
    `"notes`": `"Equipo de producción`"
  }"

$equipmentId = $equipment.id
Write-Host "Equipment ID: $equipmentId"
```

### 6.4 Crear técnico

```powershell
$technician = Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/technicians" `
  -ContentType "application/json" `
  -Body '{
    "fullName": "Carlos Ramírez",
    "email": "carlos@computech.com",
    "phone": "809-555-0010",
    "specialty": "Hardware y Redes"
  }'

$technicianId = $technician.id
Write-Host "Technician ID: $technicianId"
```

### 6.5 Crear ítem de inventario (repuesto)

Tipos válidos: `Product`, `Part`, `Accessory`  
Categorías válidas: `Memory`, `Storage`, `Processor`, `GraphicsCard`, `Motherboard`, `PowerSupply`, `CoolingSystem`, `Monitor`, `Keyboard`, `Mouse`, `Speaker`, `NetworkDevice`, `Case`, `ComputerSystem`, `Other`

```powershell
$inventoryItem = Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/inventory" `
  -ContentType "application/json" `
  -Body '{
    "name": "RAM DDR4 8GB",
    "sku": "RAM-DDR4-8GB-001",
    "description": "Módulo de memoria DDR4 2666MHz",
    "type": "Part",
    "category": "Memory",
    "initialStock": 20,
    "salePrice": 45.00
  }'

$inventoryItemId = $inventoryItem.id
Write-Host "Inventory Item ID: $inventoryItemId"
```

### 6.6 Crear programa de mantenimiento preventivo

Frecuencias válidas: `Monthly` (1), `Quarterly` (3), `SemiAnnual` (6), `Annual` (12)

```powershell
$schedule = Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/maintenance-schedules" `
  -ContentType "application/json" `
  -Body "{
    `"equipmentId`": $equipmentId,
    `"frequency`": `"Quarterly`",
    `"nextServiceDate`": `"2026-09-01T00:00:00`"
  }"

$scheduleId = $schedule.id
Write-Host "Schedule ID: $scheduleId"
```

### 6.7 Crear orden de reparación

Tipos válidos: `Repair` (subtipos: `Hardware`, `Software`, `Diagnostic`), `Maintenance` (subtipos: `Preventive`, `OnDemand`)  
Prioridades válidas: `Low`, `Medium`, `High`, `Critical`

```powershell
$repairOrder = Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/service-orders" `
  -ContentType "application/json" `
  -Body "{
    `"equipmentId`": $equipmentId,
    `"technicianId`": $technicianId,
    `"type`": `"Repair`",
    `"subType`": `"Hardware`",
    `"priority`": `"High`",
    `"description`": `"El equipo no enciende — revisar fuente de poder y RAM`"
  }"

$repairOrderId = $repairOrder.id
Write-Host "Repair Order ID: $repairOrderId"
```

### 6.8 Iniciar la orden de reparación

```powershell
Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/service-orders/$repairOrderId/start"

Write-Host "Orden iniciada (InProgress)"
```

### 6.9 Agregar ítems a la orden

**Repuesto (Part)** — descuenta stock automáticamente:

```powershell
Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/service-orders/$repairOrderId/items" `
  -ContentType "application/json" `
  -Body "{
    `"itemType`": `"Part`",
    `"description`": `"RAM DDR4 8GB — reemplazo`",
    `"quantity`": 2,
    `"unitPrice`": 45.00,
    `"inventoryItemId`": $inventoryItemId
  }"

Write-Host "Repuesto agregado (stock decrementado)"
```

**Mano de obra (Labor)**:

```powershell
Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/service-orders/$repairOrderId/items" `
  -ContentType "application/json" `
  -Body '{
    "itemType": "Labor",
    "description": "Diagnóstico y reemplazo de memoria",
    "quantity": 2,
    "unitPrice": 75.00
  }'

Write-Host "Labor agregada"
```

### 6.10 Consultar total de la orden

```powershell
$total = Invoke-RestMethod -Uri "http://localhost:5254/api/service-orders/$repairOrderId/total"
Write-Host "Total de la orden: $($total.total)"
# Esperado: 240.00 (2×45 + 2×75)
```

### 6.11 Completar la orden de reparación

```powershell
Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/service-orders/$repairOrderId/complete" `
  -ContentType "application/json" `
  -Body '{ "resolutionNotes": "Se reemplazaron 2 módulos de RAM defectuosos. Equipo operativo." }'

Write-Host "Orden completada"
```

### 6.12 Crear y completar orden de mantenimiento preventivo

Al completar una orden de tipo `Maintenance/Preventive`, la API genera automáticamente la siguiente orden de mantenimiento según el programa del equipo.

```powershell
# Crear orden preventiva vinculada al schedule
$preventiveOrder = Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/service-orders" `
  -ContentType "application/json" `
  -Body "{
    `"equipmentId`": $equipmentId,
    `"technicianId`": $technicianId,
    `"type`": `"Maintenance`",
    `"subType`": `"Preventive`",
    `"priority`": `"Medium`",
    `"description`": `"Mantenimiento preventivo trimestral`",
    `"scheduleId`": $scheduleId
  }"

$preventiveOrderId = $preventiveOrder.id

# Iniciar
Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/service-orders/$preventiveOrderId/start"

# Completar — dispara generación automática de siguiente orden
Invoke-RestMethod -Method Post `
  -Uri "http://localhost:5254/api/service-orders/$preventiveOrderId/complete" `
  -ContentType "application/json" `
  -Body '{ "resolutionNotes": "Limpieza interna, ajuste de componentes, prueba de estrés OK." }'

Write-Host "Orden preventiva completada. El sistema generó la siguiente orden automáticamente."
```

### 6.13 Verificar órdenes del equipo

```powershell
# Todas las órdenes del equipo
$orders = Invoke-RestMethod -Uri "http://localhost:5254/api/service-orders?equipmentId=$equipmentId"
$orders | ForEach-Object { Write-Host "$($_.orderNumber) — $($_.status) — $($_.subType)" }

# Programa de mantenimiento actualizado
$updatedSchedule = Invoke-RestMethod `
  -Uri "http://localhost:5254/api/equipment/$equipmentId/maintenance-schedule"
Write-Host "Próximo servicio: $($updatedSchedule.nextServiceDate)"
```

---

## Endpoints disponibles

| Módulo | Prefijo de ruta |
|---|---|
| Clientes | `GET/POST /api/customers` |
| Ubicaciones | `GET/POST/PUT/DELETE /api/customers/{id}/locations` |
| Equipos | `GET/POST/PUT/DELETE /api/equipment` |
| Técnicos | `GET/POST/PUT/DELETE /api/technicians` |
| Inventario | `GET/POST/PUT/PATCH/DELETE /api/inventory` |
| Órdenes de servicio | `GET/POST/PUT/DELETE /api/service-orders` |
| Mantenimiento | `GET/POST/PUT/DELETE /api/maintenance-schedules` |

La documentación completa de todos los endpoints, parámetros y respuestas está disponible en Swagger UI: `http://localhost:5254/swagger`

---

## Solución de problemas comunes

### La base de datos no existe
```
SqlException: Cannot open database "CompuTechDb" requested by the login.
```
Solución: ejecutar el paso 3 (migraciones).

### dotnet-ef no se encuentra
```
Could not execute because the application was not found.
```
Solución:
```powershell
dotnet tool install --global dotnet-ef
$env:PATH += ";$env:USERPROFILE\.dotnet\tools"
```

### La API falla al iniciar con InvalidOperationException sobre nombres de ruta
Este error fue corregido en el hotfix. Verificar que estás en la rama `main` (`git status`) y que los cambios más recientes están descargados (`git pull origin main`).

### Error de migración — tabla ya existe
```
There is already an object named 'X' in the database.
```
Solución: eliminar la base de datos y volver a crear:
```powershell
dotnet ef database drop `
  --project src/CompuTech.Infrastructure/CompuTech.Infrastructure.csproj `
  --startup-project src/CompuTech.API/CompuTech.API.csproj `
  --force

dotnet ef database update `
  --project src/CompuTech.Infrastructure/CompuTech.Infrastructure.csproj `
  --startup-project src/CompuTech.API/CompuTech.API.csproj
```
