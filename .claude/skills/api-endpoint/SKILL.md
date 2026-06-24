---
name: api-endpoint
description: >
  Genera el Controller REST completo para un módulo del proyecto CompuTech.
  Crea todos los endpoints CRUD estándar más los endpoints específicos del negocio,
  usando IMediator para despachar commands/queries. Incluye documentación XML para Swagger.
argument-hint: "[NombreModulo]"
---

# API Endpoint — CompuTech

Genera el controller REST completo para el módulo: `$ARGUMENTS`

## Pasos a ejecutar

1. **Leer los commands y queries existentes** del módulo en `CompuTech.Application/$ARGUMENTS/` para saber qué operaciones están disponibles para despachar.

2. **Leer el spec** en `docs/01-especificacion/especificacion.md` para identificar endpoints específicos del negocio que van más allá del CRUD estándar.

3. **Generar el controller** en `CompuTech.API/Controllers/$ARGUMENTSController.cs`:

```csharp
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class $ARGUMENTSController : ControllerBase
{
    private readonly IMediator _mediator;

    public $ARGUMENTSController(IMediator mediator) => _mediator = mediator;

    /// <summary>Obtiene todos los [entidad]s.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<[Entidad]Dto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAll$ARGUMENTSQuery(), ct);
        return Ok(result);
    }

    /// <summary>Obtiene un [entidad] por Id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof([Entidad]Dto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new Get[Entidad]ByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>Crea un nuevo [entidad].</summary>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] Create[Entidad]Command command, CancellationToken ct)
    {
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    /// <summary>Actualiza un [entidad] existente.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(int id, [FromBody] Update[Entidad]Command command, CancellationToken ct)
    {
        await _mediator.Send(command with { Id = id }, ct);
        return NoContent();
    }

    /// <summary>Desactiva un [entidad] (baja lógica).</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(int id, CancellationToken ct)
    {
        await _mediator.Send(new Deactivate[Entidad]Command(id), ct);
        return NoContent();
    }
}
```

## Códigos HTTP a usar

| Situación | Código |
|---|---|
| Lectura exitosa | `200 OK` |
| Creación exitosa | `201 Created` con `Location` header |
| Actualización/eliminación exitosa | `204 No Content` |
| Recurso no encontrado | `404 Not Found` |
| Error de validación FluentValidation | `422 Unprocessable Entity` |
| Error de regla de negocio | `400 Bad Request` |

## Endpoints adicionales por módulo

Basarte en el spec para agregar endpoints específicos:

- **ServiceOrders**: `PUT /api/serviceorders/{id}/status`, `POST /api/serviceorders/{id}/items`
- **Equipment**: `GET /api/equipment/customer/{customerId}`, `GET /api/equipment/serial/{serialNumber}`
- **Inventory**: `GET /api/inventory/low-stock`, `PUT /api/inventory/{id}/stock`
- **MaintenanceSchedule**: `GET /api/maintenanceschedules/due`, `PUT /api/maintenanceschedules/{id}/activate`

## Pasos finales

4. Verificar que todos los commands/queries referenciados existen en Application.
5. Actualizar `docs/BITACORA.md` con el controller generado.
