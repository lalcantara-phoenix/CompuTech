using CompuTech.Application.Technicians.Commands.CreateTechnician;
using CompuTech.Application.Technicians.Commands.DeactivateTechnician;
using CompuTech.Application.Technicians.Commands.UpdateTechnician;
using CompuTech.Application.Technicians.DTOs;
using CompuTech.Application.Technicians.Queries.GetActiveTechnicians;
using CompuTech.Application.Technicians.Queries.GetAllTechnicians;
using CompuTech.Application.Technicians.Queries.GetTechnicianById;
using CompuTech.API.Models.Technicians;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompuTech.API.Controllers;

/// <summary>
/// Manages technicians available for service order assignments.
/// </summary>
[Route("api/technicians")]
[ApiController]
public class TechniciansController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Returns the full list of technicians.
    /// </summary>
    /// <response code="200">List of technicians returned successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TechnicianDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllTechniciansQuery(), ct);
        return Ok(result);
    }

    /// <summary>
    /// Returns only active technicians.
    /// </summary>
    /// <response code="200">List of active technicians returned successfully.</response>
    [HttpGet("active")]
    [ProducesResponseType(typeof(IReadOnlyList<TechnicianDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetActiveTechniciansQuery(), ct);
        return Ok(result);
    }

    /// <summary>
    /// Returns a single technician by ID.
    /// </summary>
    /// <param name="id">Technician identifier.</param>
    /// <response code="200">Technician found and returned.</response>
    /// <response code="404">No technician with the given ID exists.</response>
    [HttpGet("{id:int}", Name = nameof(GetTechnicianById))]
    [ProducesResponseType(typeof(TechnicianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTechnicianById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetTechnicianByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Creates a new technician.
    /// </summary>
    /// <param name="request">Technician data.</param>
    /// <response code="201">Technician created. Returns the new technician ID.</response>
    /// <response code="400">Validation error in the request body.</response>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateTechnicianRequest request, CancellationToken ct)
    {
        var command = new CreateTechnicianCommand(request.FullName, request.Email, request.Phone, request.Specialty);
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetTechnicianById), new { id }, new { id });
    }

    /// <summary>
    /// Updates an existing technician's mutable fields.
    /// </summary>
    /// <param name="id">Technician identifier.</param>
    /// <param name="request">Updated technician data.</param>
    /// <response code="204">Technician updated successfully.</response>
    /// <response code="400">Validation error in the request body.</response>
    /// <response code="404">No technician with the given ID exists.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTechnicianRequest request, CancellationToken ct)
    {
        var command = new UpdateTechnicianCommand(id, request.FullName, request.Phone, request.Specialty);
        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    /// Deactivates (soft-deletes) a technician.
    /// </summary>
    /// <param name="id">Technician identifier.</param>
    /// <response code="204">Technician deactivated successfully.</response>
    /// <response code="404">No technician with the given ID exists.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(int id, CancellationToken ct)
    {
        await _mediator.Send(new DeactivateTechnicianCommand(id), ct);
        return NoContent();
    }
}
