using CompuTech.Application.Equipment.Commands.DeactivateEquipment;
using CompuTech.Application.Equipment.Commands.RegisterEquipment;
using CompuTech.Application.Equipment.Commands.UpdateEquipment;
using CompuTech.Application.Equipment.DTOs;
using CompuTech.Application.Equipment.Queries.GetAllEquipment;
using CompuTech.Application.Equipment.Queries.GetEquipmentByCustomer;
using CompuTech.Application.Equipment.Queries.GetEquipmentById;
using CompuTech.Application.Equipment.Queries.GetEquipmentBySerialNumber;
using CompuTech.API.Models.Equipment;
using CompuTech.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompuTech.API.Controllers;

/// <summary>
/// Manages equipment records and their association to customers and locations.
/// </summary>
[Route("api/equipment")]
[ApiController]
public class EquipmentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Returns the full list of registered equipment.
    /// </summary>
    /// <response code="200">List of equipment returned successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<EquipmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEquipmentQuery(), ct);
        return Ok(result);
    }

    /// <summary>
    /// Returns a single equipment record by ID.
    /// </summary>
    /// <param name="id">Equipment identifier.</param>
    /// <response code="200">Equipment found and returned.</response>
    /// <response code="404">No equipment with the given ID exists.</response>
    [HttpGet("{id:int}", Name = nameof(GetEquipmentById))]
    [ProducesResponseType(typeof(EquipmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEquipmentById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEquipmentByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Returns a single equipment record by serial number.
    /// </summary>
    /// <param name="serialNumber">Equipment serial number.</param>
    /// <response code="200">Equipment found and returned.</response>
    /// <response code="404">No equipment with the given serial number exists.</response>
    [HttpGet("serial/{serialNumber}")]
    [ProducesResponseType(typeof(EquipmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBySerialNumber(string serialNumber, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEquipmentBySerialNumberQuery(serialNumber), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Registers a new piece of equipment.
    /// </summary>
    /// <param name="request">Equipment data.</param>
    /// <response code="201">Equipment registered. Returns the new equipment ID.</response>
    /// <response code="400">Validation error or invalid equipment type in the request body.</response>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterEquipmentRequest request, CancellationToken ct)
    {
        if (!Enum.TryParse<EquipmentType>(request.Type, true, out var equipmentType))
            return BadRequest("Invalid equipment type");

        var command = new RegisterEquipmentCommand(
            request.CustomerId,
            request.LocationId,
            request.SerialNumber,
            request.Brand,
            request.Model,
            equipmentType,
            request.OperatingSystem,
            request.PurchaseDate,
            request.Notes
        );

        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetEquipmentById), new { id }, new { id });
    }

    /// <summary>
    /// Updates mutable fields (brand, model, type) of an existing equipment record.
    /// </summary>
    /// <param name="id">Equipment identifier.</param>
    /// <param name="request">Updated equipment data.</param>
    /// <response code="204">Equipment updated successfully.</response>
    /// <response code="400">Validation error or invalid equipment type in the request body.</response>
    /// <response code="404">No equipment with the given ID exists.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEquipmentRequest request, CancellationToken ct)
    {
        if (!Enum.TryParse<EquipmentType>(request.Type, true, out var equipmentType))
            return BadRequest("Invalid equipment type");

        var command = new UpdateEquipmentCommand(id, request.Brand, request.Model, equipmentType);
        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    /// Deactivates (soft-deletes) an equipment record.
    /// </summary>
    /// <param name="id">Equipment identifier.</param>
    /// <response code="204">Equipment deactivated successfully.</response>
    /// <response code="404">No equipment with the given ID exists.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(int id, CancellationToken ct)
    {
        await _mediator.Send(new DeactivateEquipmentCommand(id), ct);
        return NoContent();
    }
}

/// <summary>
/// Provides equipment endpoints scoped to a specific customer.
/// </summary>
[Route("api/customers")]
[ApiController]
public class CustomerEquipmentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Returns all equipment registered under a given customer.
    /// </summary>
    /// <param name="customerId">Customer identifier.</param>
    /// <response code="200">List of equipment returned successfully.</response>
    [HttpGet("{customerId:int}/equipment")]
    [ProducesResponseType(typeof(IReadOnlyList<EquipmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCustomer(int customerId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEquipmentByCustomerQuery(customerId), ct);
        return Ok(result);
    }
}
