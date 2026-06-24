using CompuTech.Application.MaintenanceSchedules.Commands.CreateMaintenanceSchedule;
using CompuTech.Application.MaintenanceSchedules.Commands.DeactivateMaintenanceSchedule;
using CompuTech.Application.MaintenanceSchedules.Commands.UpdateMaintenanceSchedule;
using CompuTech.Application.MaintenanceSchedules.DTOs;
using CompuTech.Application.MaintenanceSchedules.Queries.GetAllMaintenanceSchedules;
using CompuTech.Application.MaintenanceSchedules.Queries.GetDueMaintenanceSchedules;
using CompuTech.Application.MaintenanceSchedules.Queries.GetMaintenanceScheduleByEquipment;
using CompuTech.API.Models.MaintenanceSchedules;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompuTech.API.Controllers;

/// <summary>
/// Manages preventive maintenance schedules for equipment.
/// </summary>
[Route("api/maintenance-schedules")]
[ApiController]
public class MaintenanceSchedulesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Returns all maintenance schedules.
    /// </summary>
    /// <response code="200">List of maintenance schedules returned successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MaintenanceScheduleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllMaintenanceSchedulesQuery(), ct);
        return Ok(result);
    }

    /// <summary>
    /// Returns active maintenance schedules whose next service date is on or before the reference date.
    /// Defaults to today (UTC) when no date is provided.
    /// </summary>
    /// <param name="referenceDate">Optional reference date (UTC). Defaults to today.</param>
    /// <response code="200">List of due schedules returned successfully.</response>
    [HttpGet("due")]
    [ProducesResponseType(typeof(IReadOnlyList<MaintenanceScheduleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDue([FromQuery] DateTime? referenceDate, CancellationToken ct)
    {
        var date = referenceDate ?? DateTime.UtcNow;
        var result = await _mediator.Send(new GetDueMaintenanceSchedulesQuery(date), ct);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new maintenance schedule for an equipment.
    /// </summary>
    /// <param name="request">Schedule data: equipment ID, frequency, and first next service date.</param>
    /// <response code="201">Schedule created. Returns the new schedule ID.</response>
    /// <response code="400">Validation error (e.g. equipment already has an active schedule).</response>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMaintenanceScheduleRequest request, CancellationToken ct)
    {
        var command = new CreateMaintenanceScheduleCommand(
            request.EquipmentId,
            request.Frequency,
            request.NextServiceDate);

        var id = await _mediator.Send(command, ct);
        return CreatedAtRoute("GetByEquipment",
            new { equipmentId = request.EquipmentId },
            new { id });
    }

    /// <summary>
    /// Updates frequency and next service date of an existing schedule.
    /// </summary>
    /// <param name="id">Schedule identifier.</param>
    /// <param name="request">Updated frequency and next service date.</param>
    /// <response code="204">Schedule updated successfully.</response>
    /// <response code="400">Validation error in the request body.</response>
    /// <response code="404">No schedule with the given ID exists.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMaintenanceScheduleRequest request, CancellationToken ct)
    {
        await _mediator.Send(new UpdateMaintenanceScheduleCommand(id, request.Frequency, request.NextServiceDate), ct);
        return NoContent();
    }

    /// <summary>
    /// Deactivates (soft-deletes) a maintenance schedule.
    /// </summary>
    /// <param name="id">Schedule identifier.</param>
    /// <response code="204">Schedule deactivated successfully.</response>
    /// <response code="404">No schedule with the given ID exists.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(int id, CancellationToken ct)
    {
        await _mediator.Send(new DeactivateMaintenanceScheduleCommand(id), ct);
        return NoContent();
    }
}

/// <summary>
/// Equipment-scoped maintenance schedule endpoint.
/// </summary>
[Route("api/equipment")]
[ApiController]
public class EquipmentMaintenanceScheduleController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Returns the maintenance schedule for a specific equipment.
    /// </summary>
    /// <param name="equipmentId">Equipment identifier.</param>
    /// <response code="200">Maintenance schedule found and returned.</response>
    /// <response code="404">No maintenance schedule for the given equipment ID.</response>
    [HttpGet("{equipmentId:int}/maintenance-schedule", Name = nameof(GetByEquipment))]
    [ProducesResponseType(typeof(MaintenanceScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEquipment(int equipmentId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetMaintenanceScheduleByEquipmentQuery(equipmentId), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
}
