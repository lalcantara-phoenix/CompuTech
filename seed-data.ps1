#Requires -Version 5.1
<#
.SYNOPSIS
    Pobla la base de datos de CompuTech con datos de prueba.
.DESCRIPTION
    Crea clientes, ubicaciones, equipos, tecnicos, inventario,
    programas de mantenimiento y ordenes de servicio en varios estados.
    Requiere que la API este corriendo en http://localhost:5254.
.EXAMPLE
    .\seed-data.ps1
#>

$BASE_URL = "http://localhost:5254/api"
$ErrorActionPreference = "Stop"

function Invoke-Api {
    param(
        [string]$Method = "GET",
        [string]$Path,
        [object]$Body = $null
    )
    $uri = "$BASE_URL$Path"
    $params = @{ Method = $Method; Uri = $uri; ContentType = "application/json" }
    if ($null -ne $Body) {
        $params.Body = ($Body | ConvertTo-Json -Depth 10)
    }
    try {
        return Invoke-RestMethod @params
    } catch {
        $statusCode = $_.Exception.Response.StatusCode.value__
        $detail = $_.ErrorDetails.Message
        Write-Error "[$Method $Path] HTTP $statusCode - $detail"
        throw
    }
}

function Write-Step { param([string]$msg) Write-Host "`n== $msg ==" -ForegroundColor Cyan }
function Write-Ok   { param([string]$msg) Write-Host "  OK  $msg" -ForegroundColor Green }

# ── Verificar que la API este disponible ──────────────────────────────────────
Write-Step "Verificando conexion con la API"
try {
    Invoke-RestMethod -Uri "$BASE_URL/../swagger/v1/swagger.json" | Out-Null
    Write-Ok "API disponible en $BASE_URL"
} catch {
    Write-Host "ERROR: La API no responde en $BASE_URL" -ForegroundColor Red
    Write-Host "Ejecuta primero: dotnet run --project src/CompuTech.API/CompuTech.API.csproj" -ForegroundColor Yellow
    exit 1
}

# ─────────────────────────────────────────────────────────────────────────────
# CLIENTES Y UBICACIONES
# ─────────────────────────────────────────────────────────────────────────────

Write-Step "Creando clientes"

$c1 = Invoke-Api -Method Post -Path "/customers" -Body @{
    fullName = "Empresa Tecnologica ABC S.A."
    email    = "sistemas@empresaabc.com"
    phone    = "809-555-1001"
    taxId    = "RNC-101234567"
}
$c2 = Invoke-Api -Method Post -Path "/customers" -Body @{
    fullName = "Consultora Digital XYZ S.R.L."
    email    = "it@consultoraxyz.com"
    phone    = "829-555-2002"
    taxId    = "RNC-202345678"
}
$c3 = Invoke-Api -Method Post -Path "/customers" -Body @{
    fullName = "Universidad Central del Este"
    email    = "soporte@uce.edu.do"
    phone    = "809-555-3003"
    taxId    = "RNC-303456789"
}
Write-Ok "Clientes creados: IDs $($c1.id), $($c2.id), $($c3.id)"

Write-Step "Creando ubicaciones"

$l1 = Invoke-Api -Method Post -Path "/customers/$($c1.id)/locations" -Body @{
    name         = "Sede Central"
    address      = "Av. Winston Churchill 123, Torre Empresarial"
    city         = "Santo Domingo"
    contactPhone = "809-555-1010"
}
$l2 = Invoke-Api -Method Post -Path "/customers/$($c1.id)/locations" -Body @{
    name         = "Sucursal Norte"
    address      = "Calle 27 de Febrero 456, Local 2B"
    city         = "Santo Domingo Norte"
    contactPhone = "809-555-1011"
}
$l3 = Invoke-Api -Method Post -Path "/customers/$($c2.id)/locations" -Body @{
    name         = "Oficina Principal"
    address      = "Av. Juan Pablo Duarte 789, Piso 3"
    city         = "Santiago"
    contactPhone = "829-555-2010"
}
$l4 = Invoke-Api -Method Post -Path "/customers/$($c3.id)/locations" -Body @{
    name         = "Campus Norte - Edificio TI"
    address      = "Autopista del Este Km 7.5"
    city         = "San Pedro de Macoris"
    contactPhone = "809-555-3010"
}
$l5 = Invoke-Api -Method Post -Path "/customers/$($c3.id)/locations" -Body @{
    name         = "Biblioteca Central"
    address      = "Autopista del Este Km 7.5, Edificio B"
    city         = "San Pedro de Macoris"
    contactPhone = "809-555-3011"
}
Write-Ok "Ubicaciones creadas: IDs $($l1.id), $($l2.id), $($l3.id), $($l4.id), $($l5.id)"

# ─────────────────────────────────────────────────────────────────────────────
# EQUIPOS
# ─────────────────────────────────────────────────────────────────────────────

Write-Step "Registrando equipos"

$e1 = Invoke-Api -Method Post -Path "/equipment" -Body @{
    customerId      = $c1.id
    locationId      = $l1.id
    serialNumber    = "DELL-OPT-2024-001"
    brand           = "Dell"
    model           = "OptiPlex 7090"
    type            = "Desktop"
    operatingSystem = "Windows 11 Pro"
    purchaseDate    = "2024-03-15T00:00:00"
    notes           = "Equipo de contabilidad - uso intensivo"
}
$e2 = Invoke-Api -Method Post -Path "/equipment" -Body @{
    customerId      = $c1.id
    locationId      = $l1.id
    serialNumber    = "HP-LAT-2023-002"
    brand           = "HP"
    model           = "EliteBook 840 G9"
    type            = "Laptop"
    operatingSystem = "Windows 11 Pro"
    purchaseDate    = "2023-11-20T00:00:00"
    notes           = "Laptop del gerente de sistemas"
}
$e3 = Invoke-Api -Method Post -Path "/equipment" -Body @{
    customerId      = $c1.id
    locationId      = $l2.id
    serialNumber    = "LENOVO-WS-2022-003"
    brand           = "Lenovo"
    model           = "ThinkStation P360"
    type            = "Workstation"
    operatingSystem = "Windows 10 Pro"
    purchaseDate    = "2022-07-10T00:00:00"
    notes           = "Estacion grafica - sucursal norte"
}
$e4 = Invoke-Api -Method Post -Path "/equipment" -Body @{
    customerId      = $c2.id
    locationId      = $l3.id
    serialNumber    = "HP-SRV-2023-004"
    brand           = "HP"
    model           = "ProLiant DL380 Gen10"
    type            = "Server"
    operatingSystem = "Windows Server 2022"
    purchaseDate    = "2023-01-05T00:00:00"
    notes           = "Servidor principal de la consultora"
}
$e5 = Invoke-Api -Method Post -Path "/equipment" -Body @{
    customerId      = $c2.id
    locationId      = $l3.id
    serialNumber    = "DELL-LAT-2024-005"
    brand           = "Dell"
    model           = "Latitude 5540"
    type            = "Laptop"
    operatingSystem = "Windows 11 Pro"
    purchaseDate    = "2024-01-18T00:00:00"
    notes           = $null
}
$e6 = Invoke-Api -Method Post -Path "/equipment" -Body @{
    customerId      = $c3.id
    locationId      = $l4.id
    serialNumber    = "DELL-SRV-2021-006"
    brand           = "Dell"
    model           = "PowerEdge R740"
    type            = "Server"
    operatingSystem = "Ubuntu Server 22.04"
    purchaseDate    = "2021-09-01T00:00:00"
    notes           = "Servidor de base de datos universitario"
}
$e7 = Invoke-Api -Method Post -Path "/equipment" -Body @{
    customerId      = $c3.id
    locationId      = $l4.id
    serialNumber    = "HP-OPT-2023-007"
    brand           = "HP"
    model           = "ProDesk 400 G9"
    type            = "Desktop"
    operatingSystem = "Windows 11 Pro"
    purchaseDate    = "2023-06-15T00:00:00"
    notes           = "Laboratorio de informatica - puesto 01"
}
$e8 = Invoke-Api -Method Post -Path "/equipment" -Body @{
    customerId      = $c3.id
    locationId      = $l5.id
    serialNumber    = "LENOVO-LAP-2024-008"
    brand           = "Lenovo"
    model           = "ThinkPad E14 Gen 5"
    type            = "Laptop"
    operatingSystem = "Windows 11 Home"
    purchaseDate    = "2024-02-28T00:00:00"
    notes           = "Laptop biblioteca - consulta de recursos digitales"
}
Write-Ok "Equipos registrados: IDs $($e1.id)..$($e8.id)"

# ─────────────────────────────────────────────────────────────────────────────
# TECNICOS
# ─────────────────────────────────────────────────────────────────────────────

Write-Step "Creando tecnicos"

$t1 = Invoke-Api -Method Post -Path "/technicians" -Body @{
    fullName  = "Carlos Ernesto Ramirez"
    email     = "cramirez@computech.com"
    phone     = "809-555-9001"
    specialty = "Hardware y Componentes"
}
$t2 = Invoke-Api -Method Post -Path "/technicians" -Body @{
    fullName  = "Maria Alejandra Gonzalez"
    email     = "mgonzalez@computech.com"
    phone     = "829-555-9002"
    specialty = "Software y Sistemas Operativos"
}
$t3 = Invoke-Api -Method Post -Path "/technicians" -Body @{
    fullName  = "Pedro Antonio Jimenez"
    email     = "pjimenez@computech.com"
    phone     = "849-555-9003"
    specialty = "Redes y Servidores"
}
Write-Ok "Tecnicos creados: IDs $($t1.id), $($t2.id), $($t3.id)"

# ─────────────────────────────────────────────────────────────────────────────
# INVENTARIO
# ─────────────────────────────────────────────────────────────────────────────

Write-Step "Creando items de inventario"

$i1 = Invoke-Api -Method Post -Path "/inventory" -Body @{
    name         = "RAM DDR4 8GB 2666MHz"
    sku          = "MEM-DDR4-8GB-2666"
    description  = "Modulo de memoria DDR4 8GB 2666MHz compatible con la mayoria de placas modernas"
    type         = "Part"
    category     = "Memory"
    initialStock = 30
    salePrice    = 42.50
}
$i2 = Invoke-Api -Method Post -Path "/inventory" -Body @{
    name         = "SSD Samsung 870 EVO 500GB"
    sku          = "SSD-SAM-870-500GB"
    description  = "Disco solido SATA III 500GB - velocidad lectura 560MB/s"
    type         = "Part"
    category     = "Storage"
    initialStock = 20
    salePrice    = 65.00
}
$i3 = Invoke-Api -Method Post -Path "/inventory" -Body @{
    name         = "Procesador Intel Core i5-12400"
    sku          = "CPU-INT-I5-12400"
    description  = "Procesador de 6 nucleos, 12 hilos, hasta 4.4GHz"
    type         = "Part"
    category     = "Processor"
    initialStock = 10
    salePrice    = 195.00
}
$i4 = Invoke-Api -Method Post -Path "/inventory" -Body @{
    name         = "Pasta Termica Arctic MX-4"
    sku          = "COOL-ARCTIC-MX4-4G"
    description  = "Pasta termica de alto rendimiento, jeringa 4g"
    type         = "Accessory"
    category     = "CoolingSystem"
    initialStock = 50
    salePrice    = 8.50
}
$i5 = Invoke-Api -Method Post -Path "/inventory" -Body @{
    name         = "Cable HDMI 2.0 1.8m"
    sku          = "CAB-HDMI-20-18M"
    description  = "Cable HDMI 4K 60Hz, 1.8 metros, conectores dorados"
    type         = "Accessory"
    category     = "Other"
    initialStock = 40
    salePrice    = 12.00
}
$i6 = Invoke-Api -Method Post -Path "/inventory" -Body @{
    name         = "Fuente de Poder Corsair 650W 80+ Bronze"
    sku          = "PSU-COR-650W-BRZ"
    description  = "Fuente modular 650W certificacion 80+ Bronze"
    type         = "Part"
    category     = "PowerSupply"
    initialStock = 8
    salePrice    = 85.00
}
Write-Ok "Inventario creado: IDs $($i1.id)..$($i6.id)"

# ─────────────────────────────────────────────────────────────────────────────
# PROGRAMAS DE MANTENIMIENTO
# ─────────────────────────────────────────────────────────────────────────────

Write-Step "Creando programas de mantenimiento"

$sch1 = Invoke-Api -Method Post -Path "/maintenance-schedules" -Body @{
    equipmentId     = $e4.id
    frequency       = "Quarterly"
    nextServiceDate = "2026-09-01T08:00:00"
}
$sch2 = Invoke-Api -Method Post -Path "/maintenance-schedules" -Body @{
    equipmentId     = $e6.id
    frequency       = "SemiAnnual"
    nextServiceDate = "2026-12-01T08:00:00"
}
$sch3 = Invoke-Api -Method Post -Path "/maintenance-schedules" -Body @{
    equipmentId     = $e1.id
    frequency       = "Annual"
    nextServiceDate = "2027-01-15T08:00:00"
}
Write-Ok "Schedules creados: IDs $($sch1.id), $($sch2.id), $($sch3.id)"

# ─────────────────────────────────────────────────────────────────────────────
# ORDENES DE SERVICIO
# ─────────────────────────────────────────────────────────────────────────────

Write-Step "Creando ordenes de servicio"

# ── Orden 1: Completada - Reparacion Hardware (reemplazo RAM + labor) ─────────
$o1 = Invoke-Api -Method Post -Path "/service-orders" -Body @{
    equipmentId = $e2.id
    technicianId = $t1.id
    type        = "Repair"
    subType     = "Hardware"
    priority    = "High"
    description = "Laptop no enciende. Se reportan 2 beeps al inicio. Posible fallo en RAM."
}
Invoke-Api -Method Post -Path "/service-orders/$($o1.id)/start" | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o1.id)/items" -Body @{
    itemType       = "Part"
    description    = "RAM DDR4 8GB - reemplazo modulo defectuoso"
    quantity       = 1
    unitPrice      = 42.50
    inventoryItemId = $i1.id
} | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o1.id)/items" -Body @{
    itemType  = "Labor"
    description = "Diagnostico de hardware y reemplazo de memoria"
    quantity  = 1
    unitPrice = 85.00
} | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o1.id)/complete" -Body @{
    resolutionNotes = "Se identifico modulo de RAM defectuoso en slot A1. Reemplazo realizado. Laptop operativa al 100%."
} | Out-Null
Write-Ok "Orden #$($o1.id) - Completada (Repair/Hardware)"

# ── Orden 2: Completada - Reparacion Software (reinstalacion SO + labor) ──────
$o2 = Invoke-Api -Method Post -Path "/service-orders" -Body @{
    equipmentId = $e7.id
    technicianId = $t2.id
    type        = "Repair"
    subType     = "Software"
    priority    = "Medium"
    description = "Sistema operativo corrupto. Windows no inicia. Se necesita reinstalacion."
}
Invoke-Api -Method Post -Path "/service-orders/$($o2.id)/start" | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o2.id)/items" -Body @{
    itemType    = "Labor"
    description = "Reinstalacion de Windows 11 Pro, drivers y configuracion inicial"
    quantity    = 3
    unitPrice   = 45.00
} | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o2.id)/complete" -Body @{
    resolutionNotes = "Reinstalacion completa de Windows 11 Pro. Se realizaron actualizaciones y se restauraron archivos de usuario desde backup. Equipo operativo."
} | Out-Null
Write-Ok "Orden #$($o2.id) - Completada (Repair/Software)"

# ── Orden 3: En progreso - Diagnostico servidor ───────────────────────────────
$o3 = Invoke-Api -Method Post -Path "/service-orders" -Body @{
    equipmentId = $e4.id
    technicianId = $t3.id
    type        = "Repair"
    subType     = "Diagnostic"
    priority    = "Critical"
    description = "Servidor con intermitencia en la red. Paquetes perdidos en LAN. Afecta a 15 usuarios."
    scheduleId  = $null
}
Invoke-Api -Method Post -Path "/service-orders/$($o3.id)/start" | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o3.id)/items" -Body @{
    itemType    = "Labor"
    description = "Diagnostico de red - analisis de trafico y revision de NIC"
    quantity    = 2
    unitPrice   = 95.00
} | Out-Null
Write-Ok "Orden #$($o3.id) - En progreso (Repair/Diagnostic) - critica"

# ── Orden 4: Planificada - Mantenimiento preventivo servidor universitario ─────
$o4 = Invoke-Api -Method Post -Path "/service-orders" -Body @{
    equipmentId  = $e6.id
    technicianId = $t3.id
    type         = "Maintenance"
    subType      = "Preventive"
    priority     = "Medium"
    description  = "Mantenimiento preventivo semestral - limpieza, revision de discos y actualizacion de firmware."
    scheduledAt  = "2026-12-01T08:00:00"
    scheduleId   = $sch2.id
}
Write-Ok "Orden #$($o4.id) - Planificada (Maintenance/Preventive)"

# ── Orden 5: Planificada - Reparacion hardware desktop ────────────────────────
$o5 = Invoke-Api -Method Post -Path "/service-orders" -Body @{
    equipmentId  = $e3.id
    technicianId = $t1.id
    type         = "Repair"
    subType      = "Hardware"
    priority     = "Low"
    description  = "Workstation con ruido excesivo en el sistema de enfriamiento. Posible fan defectuoso."
    scheduledAt  = "2026-07-10T09:00:00"
}
Write-Ok "Orden #$($o5.id) - Planificada (Repair/Hardware)"

# ── Orden 6: Cancelada - Reparacion laptop ────────────────────────────────────
$o6 = Invoke-Api -Method Post -Path "/service-orders" -Body @{
    equipmentId  = $e5.id
    technicianId = $t2.id
    type         = "Repair"
    subType      = "Software"
    priority     = "Low"
    description  = "Reporte de lentitud general. Cliente solicita revision de malware y optimizacion."
}
Invoke-Api -Method Post -Path "/service-orders/$($o6.id)/cancel" | Out-Null
Write-Ok "Orden #$($o6.id) - Cancelada (cliente resolvio internamente)"

# ── Orden 7: Planificada - Mantenimiento bajo demanda ────────────────────────
$o7 = Invoke-Api -Method Post -Path "/service-orders" -Body @{
    equipmentId  = $e8.id
    technicianId = $t2.id
    type         = "Maintenance"
    subType      = "OnDemand"
    priority     = "Low"
    description  = "Solicitud del cliente: limpieza general y verificacion de bateria en laptop de biblioteca."
    scheduledAt  = "2026-07-05T14:00:00"
}
Write-Ok "Orden #$($o7.id) - Planificada (Maintenance/OnDemand)"

# ── Orden 8: Completada - Reparacion con multiples repuestos ─────────────────
$o8 = Invoke-Api -Method Post -Path "/service-orders" -Body @{
    equipmentId  = $e1.id
    technicianId = $t1.id
    type         = "Repair"
    subType      = "Hardware"
    priority     = "High"
    description  = "Desktop no arranca. Fallo multiple: fuente de poder y SSD con sectores danados."
}
Invoke-Api -Method Post -Path "/service-orders/$($o8.id)/start" | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o8.id)/items" -Body @{
    itemType        = "Part"
    description     = "Fuente de poder 650W - reemplazo"
    quantity        = 1
    unitPrice       = 85.00
    inventoryItemId = $i6.id
} | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o8.id)/items" -Body @{
    itemType        = "Part"
    description     = "SSD 500GB - reemplazo disco danado"
    quantity        = 1
    unitPrice       = 65.00
    inventoryItemId = $i2.id
} | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o8.id)/items" -Body @{
    itemType    = "Labor"
    description = "Diagnostico, reemplazo de componentes e instalacion de SO"
    quantity    = 4
    unitPrice   = 75.00
} | Out-Null
Invoke-Api -Method Post -Path "/service-orders/$($o8.id)/complete" -Body @{
    resolutionNotes = "Fuente de poder quemada por sobretension. SSD con sectores irrecuperables. Se reemplazaron ambos componentes y se reinstalo Windows 11 Pro. Se recomienda UPS al cliente."
} | Out-Null
Write-Ok "Orden #$($o8.id) - Completada (Repair/Hardware) - fuente + SSD + labor"

# ─────────────────────────────────────────────────────────────────────────────
# RESUMEN
# ─────────────────────────────────────────────────────────────────────────────

Write-Host ""
Write-Host "========================================" -ForegroundColor Yellow
Write-Host "  SEED COMPLETADO EXITOSAMENTE" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Yellow
Write-Host ""
Write-Host "Clientes      : 3  (IDs $($c1.id)-$($c3.id))"
Write-Host "Ubicaciones   : 5  (IDs $($l1.id)-$($l5.id))"
Write-Host "Equipos       : 8  (IDs $($e1.id)-$($e8.id))"
Write-Host "Tecnicos      : 3  (IDs $($t1.id)-$($t3.id))"
Write-Host "Inventario    : 6  (IDs $($i1.id)-$($i6.id))"
Write-Host "Schedules     : 3  (IDs $($sch1.id)-$($sch3.id))"
Write-Host "Ordenes       : 8"
Write-Host "  - Completadas : 3 (IDs $($o1.id), $($o2.id), $($o8.id))"
Write-Host "  - En progreso : 1 (ID  $($o3.id))"
Write-Host "  - Planificadas: 3 (IDs $($o4.id), $($o5.id), $($o7.id))"
Write-Host "  - Canceladas  : 1 (ID  $($o6.id))"
Write-Host ""
Write-Host "Swagger UI: http://localhost:5254/swagger" -ForegroundColor Cyan
Write-Host ""
