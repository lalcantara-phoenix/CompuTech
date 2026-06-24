# Evidencia 4 — Documentation Guidelines

**Criterio:** Generar con Claude el README principal y los XML comments de al menos una clase del dominio.

---

## Descripción

La documentación fue generada en tres niveles: (1) README/guía de onboarding para humanos, (2) comentarios XML en controllers para Swagger, y (3) documentación técnica para agentes de IA en CLAUDE.md.

---

## Artefacto 1: SETUP.md — Guía de Onboarding

**Archivo:** `SETUP.md` (raíz del repositorio)

Generado por Claude como guía completa para que cualquier desarrollador pueda clonar el repositorio, configurar el entorno y ejecutar un demo end-to-end. Incluye:

- **Prerrequisitos** — .NET 10 SDK, SQL Server LocalDB, `dotnet-ef` global tool
- **Pasos de setup** — clone, restore, build
- **Lista de migraciones** — 6 migraciones en orden (AddCustomerAndLocation → AddMaintenanceSchedule)
- **Ejecución de tests** — `dotnet test` con resultado esperado: 44 tests, 0 fallos
- **Inicio de la API** — puerto 5254, Swagger en `/swagger`
- **Demo PowerShell de 13 pasos** — flujo completo: Customer → Location → Equipment → Technician → InventoryItem → MaintenanceSchedule → Repair Order → Start → Items → Complete; luego Preventive Order → Complete → auto-generación
- **Tabla de endpoints** por módulo
- **4 escenarios de troubleshooting** comunes

**Commit:** `9d0b1b6  docs: add SETUP.md with full environment setup and end-to-end demo guide`

---

## Artefacto 2: XML Comments en Controllers

El skill `/api-endpoint` instruyó al agente `dotnet-clean-arch` a generar comentarios XML en todos los controllers para que Swagger muestre descripciones de endpoints.

**Ejemplo — `src/CompuTech.API/Controllers/CustomersController.cs`:**

```csharp
/// <summary>
/// Manages customers and their service locations.
/// </summary>
[Route("api/customers")]
[ApiController]
public class CustomersController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Returns the full list of customers.
    /// </summary>
    /// <response code="200">List of customers returned successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)

    /// <summary>
    /// Returns a customer by ID.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <response code="200">Customer found.</response>
    /// <response code="404">Customer not found.</response>
    [HttpGet("{id:int}", Name = "GetCustomerById")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
```

Los comentarios XML se incluyen en la documentación de Swagger porque `Program.cs` configura:

```csharp
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
if (File.Exists(xmlPath))
    c.IncludeXmlComments(xmlPath);
```

Y el proyecto API tiene `<GenerateDocumentationFile>true</GenerateDocumentationFile>` en su `.csproj`.

**Controllers con XML comments:**
- `CustomersController.cs`
- `EquipmentController.cs`
- `TechniciansController.cs`
- `InventoryController.cs`
- `ServiceOrdersController.cs`
- `MaintenanceSchedulesController.cs`

---

## Artefacto 3: Documentación de Arquitectura

**Archivo:** `docs/02-arquitectura/arquitectura.md`

Generado por Claude documentando la arquitectura completa del proyecto, incluyendo diagrama de capas, flujo de una request y decisiones de diseño.

---

## Artefacto 4: Especificación Estructurada

**Archivo:** `docs/01-especificacion/especificacion.md`

Claude procesó el spec original y lo estructuró con secciones claras por entidad, enums, reglas de negocio y endpoints.

---

## Resultado Verificable

Swagger UI en `http://localhost:5254/swagger` muestra:
- Nombre y descripción de cada endpoint
- Tipos de respuesta con códigos HTTP
- Modelos de request/response documentados
- Agrupación por controller (módulo)
