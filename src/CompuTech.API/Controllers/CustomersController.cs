using CompuTech.Application.Customers.Commands.CreateCustomer;
using CompuTech.Application.Customers.Commands.CreateCustomerLocation;
using CompuTech.Application.Customers.Commands.DeactivateCustomer;
using CompuTech.Application.Customers.Commands.DeactivateCustomerLocation;
using CompuTech.Application.Customers.Commands.UpdateCustomer;
using CompuTech.Application.Customers.Commands.UpdateCustomerLocation;
using CompuTech.Application.Customers.DTOs;
using CompuTech.Application.Customers.Queries.GetAllCustomers;
using CompuTech.Application.Customers.Queries.GetCustomerById;
using CompuTech.Application.Customers.Queries.GetCustomerLocations;
using CompuTech.API.Models.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompuTech.API.Controllers;

/// <summary>
/// Manages customers and their service locations.
/// </summary>
[Route("api/customers")]
[ApiController]
public class CustomersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // ── Customers ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the full list of customers.
    /// </summary>
    /// <response code="200">List of customers returned successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllCustomersQuery(), ct);
        return Ok(result);
    }

    /// <summary>
    /// Returns a single customer by ID, including their locations.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <response code="200">Customer found and returned.</response>
    /// <response code="404">No customer with the given ID exists.</response>
    [HttpGet("{id:int}", Name = "GetCustomerById")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="request">Customer data.</param>
    /// <response code="201">Customer created. Returns the new customer ID.</response>
    /// <response code="400">Validation error in the request body.</response>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken ct)
    {
        var command = new CreateCustomerCommand(request.FullName, request.Email, request.Phone, request.TaxId);
        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>
    /// Updates an existing customer's mutable fields.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <param name="request">Updated customer data.</param>
    /// <response code="204">Customer updated successfully.</response>
    /// <response code="400">Validation error in the request body.</response>
    /// <response code="404">No customer with the given ID exists.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerRequest request, CancellationToken ct)
    {
        var command = new UpdateCustomerCommand(id, request.FullName, request.Phone, request.TaxId);
        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    /// Deactivates (soft-deletes) a customer.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <response code="204">Customer deactivated successfully.</response>
    /// <response code="404">No customer with the given ID exists.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(int id, CancellationToken ct)
    {
        await _mediator.Send(new DeactivateCustomerCommand(id), ct);
        return NoContent();
    }

    // ── Customer Locations ────────────────────────────────────────────────────

    /// <summary>
    /// Returns all service locations for a given customer.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <response code="200">List of locations returned successfully.</response>
    [HttpGet("{id:int}/locations")]
    [ProducesResponseType(typeof(IReadOnlyList<CustomerLocationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLocations(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCustomerLocationsQuery(id), ct);
        return Ok(result);
    }

    /// <summary>
    /// Adds a new service location to a customer.
    /// </summary>
    /// <param name="id">Customer identifier.</param>
    /// <param name="request">Location data.</param>
    /// <response code="201">Location created. Returns the new location ID.</response>
    /// <response code="400">Validation error in the request body.</response>
    /// <response code="404">No customer with the given ID exists.</response>
    [HttpPost("{id:int}/locations")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateLocation(int id, [FromBody] CreateCustomerLocationRequest request, CancellationToken ct)
    {
        var command = new CreateCustomerLocationCommand(id, request.Name, request.Address, request.City, request.ContactPhone);
        var locationId = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id = locationId });
    }

    /// <summary>
    /// Updates an existing service location.
    /// </summary>
    /// <param name="customerId">Customer identifier.</param>
    /// <param name="locationId">Location identifier.</param>
    /// <param name="request">Updated location data.</param>
    /// <response code="204">Location updated successfully.</response>
    /// <response code="400">Validation error in the request body.</response>
    /// <response code="404">No location with the given ID exists for this customer.</response>
    [HttpPut("{customerId:int}/locations/{locationId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLocation(int customerId, int locationId, [FromBody] UpdateCustomerLocationRequest request, CancellationToken ct)
    {
        var command = new UpdateCustomerLocationCommand(locationId, request.Name, request.Address, request.City, request.ContactPhone);
        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    /// Deactivates (soft-deletes) a service location.
    /// </summary>
    /// <param name="customerId">Customer identifier.</param>
    /// <param name="locationId">Location identifier.</param>
    /// <response code="204">Location deactivated successfully.</response>
    /// <response code="404">No location with the given ID exists for this customer.</response>
    [HttpDelete("{customerId:int}/locations/{locationId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateLocation(int customerId, int locationId, CancellationToken ct)
    {
        await _mediator.Send(new DeactivateCustomerLocationCommand(locationId), ct);
        return NoContent();
    }
}
