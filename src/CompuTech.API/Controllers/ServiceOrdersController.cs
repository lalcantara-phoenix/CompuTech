using CompuTech.Application.ServiceOrders.Commands.AddServiceOrderItem;
using CompuTech.Application.ServiceOrders.Commands.CancelServiceOrder;
using CompuTech.Application.ServiceOrders.Commands.CompleteServiceOrder;
using CompuTech.Application.ServiceOrders.Commands.CreateServiceOrder;
using CompuTech.Application.ServiceOrders.Commands.DelayServiceOrder;
using CompuTech.Application.ServiceOrders.Commands.RemoveServiceOrderItem;
using CompuTech.Application.ServiceOrders.Commands.StartServiceOrder;
using CompuTech.Application.ServiceOrders.Commands.UpdateServiceOrder;
using CompuTech.Application.ServiceOrders.DTOs;
using CompuTech.Application.ServiceOrders.Queries.GetAllServiceOrders;
using CompuTech.Application.ServiceOrders.Queries.GetServiceOrderById;
using CompuTech.Application.ServiceOrders.Queries.GetServiceOrderByNumber;
using CompuTech.Application.ServiceOrders.Queries.GetServiceOrderTotal;
using CompuTech.Application.ServiceOrders.Queries.GetServiceOrdersByEquipment;
using CompuTech.Application.ServiceOrders.Queries.GetServiceOrdersByStatus;
using CompuTech.Application.ServiceOrders.Queries.GetServiceOrdersByTechnician;
using CompuTech.API.Models.ServiceOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompuTech.API.Controllers;

/// <summary>
/// Manages service orders for technical equipment repairs and maintenance.
/// </summary>
[Route("api/service-orders")]
[ApiController]
public class ServiceOrdersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Returns service orders, optionally filtered by status, technician, or equipment.
    /// Only one filter is applied at a time; priority is: status, then technicianId, then equipmentId.
    /// </summary>
    /// <param name="status">Optional filter by status (e.g. Planned, InProgress, Completed).</param>
    /// <param name="technicianId">Optional filter by assigned technician ID.</param>
    /// <param name="equipmentId">Optional filter by equipment ID.</param>
    /// <response code="200">List of service orders returned successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ServiceOrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? status,
        [FromQuery] int? technicianId,
        [FromQuery] int? equipmentId,
        CancellationToken ct)
    {
        IReadOnlyList<ServiceOrderDto> result;

        if (status is not null)
            result = await _mediator.Send(new GetServiceOrdersByStatusQuery(status), ct);
        else if (technicianId is not null)
            result = await _mediator.Send(new GetServiceOrdersByTechnicianQuery(technicianId.Value), ct);
        else if (equipmentId is not null)
            result = await _mediator.Send(new GetServiceOrdersByEquipmentQuery(equipmentId.Value), ct);
        else
            result = await _mediator.Send(new GetAllServiceOrdersQuery(), ct);

        return Ok(result);
    }

    /// <summary>
    /// Returns a single service order by its order number.
    /// </summary>
    /// <param name="orderNumber">The unique order number (e.g. SO-20260001).</param>
    /// <response code="200">Service order found and returned.</response>
    /// <response code="404">No service order with the given order number exists.</response>
    [HttpGet("number/{orderNumber}", Name = nameof(GetByNumber))]
    [ProducesResponseType(typeof(ServiceOrderDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByNumber(string orderNumber, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetServiceOrderByNumberQuery(orderNumber), ct);
        return Ok(result);
    }

    /// <summary>
    /// Returns a single service order by ID.
    /// </summary>
    /// <param name="id">Service order identifier.</param>
    /// <response code="200">Service order found and returned.</response>
    /// <response code="404">No service order with the given ID exists.</response>
    [HttpGet("{id:int}", Name = "GetServiceOrderById")]
    [ProducesResponseType(typeof(ServiceOrderDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetServiceOrderByIdQuery(id), ct);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new service order.
    /// </summary>
    /// <param name="request">Service order data.</param>
    /// <response code="201">Service order created. Returns the new order ID.</response>
    /// <response code="400">Validation error in the request body.</response>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateServiceOrderRequest request, CancellationToken ct)
    {
        var command = new CreateServiceOrderCommand(
            request.EquipmentId,
            request.TechnicianId,
            request.Type,
            request.SubType,
            request.Priority,
            request.Description,
            request.ScheduledAt,
            request.ScheduleId);

        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>
    /// Updates the mutable fields of an existing service order.
    /// </summary>
    /// <param name="id">Service order identifier.</param>
    /// <param name="request">Updated service order data.</param>
    /// <response code="204">Service order updated successfully.</response>
    /// <response code="400">Validation error in the request body.</response>
    /// <response code="404">No service order with the given ID exists.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceOrderRequest request, CancellationToken ct)
    {
        var command = new UpdateServiceOrderCommand(
            id,
            request.TechnicianId,
            request.Priority,
            request.Description,
            request.ScheduledAt);

        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    /// Transitions a service order to InProgress status.
    /// </summary>
    /// <param name="id">Service order identifier.</param>
    /// <response code="204">Service order started successfully.</response>
    /// <response code="400">Invalid state transition.</response>
    /// <response code="404">No service order with the given ID exists.</response>
    [HttpPost("{id:int}/start")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Start(int id, CancellationToken ct)
    {
        await _mediator.Send(new StartServiceOrderCommand(id), ct);
        return NoContent();
    }

    /// <summary>
    /// Transitions a service order to Completed status.
    /// </summary>
    /// <param name="id">Service order identifier.</param>
    /// <param name="request">Optional resolution notes.</param>
    /// <response code="204">Service order completed successfully.</response>
    /// <response code="400">Invalid state transition.</response>
    /// <response code="404">No service order with the given ID exists.</response>
    [HttpPost("{id:int}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Complete(int id, [FromBody] CompleteServiceOrderRequest? request, CancellationToken ct)
    {
        await _mediator.Send(new CompleteServiceOrderCommand(id, request?.ResolutionNotes), ct);
        return NoContent();
    }

    /// <summary>
    /// Transitions a service order to Delayed status.
    /// </summary>
    /// <param name="id">Service order identifier.</param>
    /// <response code="204">Service order marked as delayed.</response>
    /// <response code="400">Invalid state transition.</response>
    /// <response code="404">No service order with the given ID exists.</response>
    [HttpPost("{id:int}/delay")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delay(int id, CancellationToken ct)
    {
        await _mediator.Send(new DelayServiceOrderCommand(id), ct);
        return NoContent();
    }

    /// <summary>
    /// Cancels a service order.
    /// </summary>
    /// <param name="id">Service order identifier.</param>
    /// <response code="204">Service order cancelled successfully.</response>
    /// <response code="400">Invalid state transition.</response>
    /// <response code="404">No service order with the given ID exists.</response>
    [HttpPost("{id:int}/cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(int id, CancellationToken ct)
    {
        await _mediator.Send(new CancelServiceOrderCommand(id), ct);
        return NoContent();
    }

    /// <summary>
    /// Adds a line item (part or labor) to an existing service order.
    /// </summary>
    /// <param name="id">Service order identifier.</param>
    /// <param name="request">Item data including type, quantity, and unit price.</param>
    /// <response code="204">Item added to the service order successfully.</response>
    /// <response code="400">Validation error or business rule violation (e.g. insufficient stock).</response>
    /// <response code="404">No service order with the given ID exists.</response>
    [HttpPost("{id:int}/items")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddItem(int id, [FromBody] AddServiceOrderItemRequest request, CancellationToken ct)
    {
        var command = new AddServiceOrderItemCommand(
            id,
            request.ItemType,
            request.Description,
            request.Quantity,
            request.UnitPrice,
            request.InventoryItemId,
            request.Notes);

        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    /// Removes a line item from a service order.
    /// </summary>
    /// <param name="id">Service order identifier.</param>
    /// <param name="itemId">Line item identifier.</param>
    /// <response code="204">Item removed from the service order successfully.</response>
    /// <response code="404">No service order or item with the given IDs exist.</response>
    [HttpDelete("{id:int}/items/{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveItem(int id, int itemId, CancellationToken ct)
    {
        await _mediator.Send(new RemoveServiceOrderItemCommand(id, itemId), ct);
        return NoContent();
    }

    /// <summary>
    /// Returns the calculated total cost of all items in a service order.
    /// </summary>
    /// <param name="id">Service order identifier.</param>
    /// <response code="200">Total returned successfully.</response>
    /// <response code="404">No service order with the given ID exists.</response>
    [HttpGet("{id:int}/total")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTotal(int id, CancellationToken ct)
    {
        var total = await _mediator.Send(new GetServiceOrderTotalQuery(id), ct);
        return Ok(new { total });
    }
}
