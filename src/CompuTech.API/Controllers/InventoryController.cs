using CompuTech.Application.Inventory.Commands.AdjustStock;
using CompuTech.Application.Inventory.Commands.CreateInventoryItem;
using CompuTech.Application.Inventory.Commands.DeactivateInventoryItem;
using CompuTech.Application.Inventory.Commands.UpdateInventoryItem;
using CompuTech.Application.Inventory.DTOs;
using CompuTech.Application.Inventory.Queries.GetAllInventoryItems;
using CompuTech.Application.Inventory.Queries.GetInventoryItemById;
using CompuTech.Application.Inventory.Queries.GetInventoryItemBySku;
using CompuTech.API.Models.Inventory;
using CompuTech.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompuTech.API.Controllers;

/// <summary>
/// Manages inventory items (products, parts, and accessories) for service orders.
/// </summary>
[Route("api/inventory")]
[ApiController]
public class InventoryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Returns inventory items, optionally filtered by type, category, or stock availability.
    /// </summary>
    /// <param name="type">Optional filter by item type (Product, Part, Accessory).</param>
    /// <param name="category">Optional filter by category (e.g. Storage, Memory, Monitor).</param>
    /// <param name="inStockOnly">When true, returns only items with stock greater than zero.</param>
    /// <response code="200">List of inventory items returned successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<InventoryItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? type,
        [FromQuery] string? category,
        [FromQuery] bool? inStockOnly,
        CancellationToken ct)
    {
        InventoryItemType? itemType = null;
        if (type is not null && Enum.TryParse<InventoryItemType>(type, true, out var t))
            itemType = t;

        InventoryCategory? itemCategory = null;
        if (category is not null && Enum.TryParse<InventoryCategory>(category, true, out var c))
            itemCategory = c;

        var result = await _mediator.Send(
            new GetAllInventoryItemsQuery(itemType, itemCategory, inStockOnly), ct);
        return Ok(result);
    }

    /// <summary>
    /// Returns a single inventory item by SKU.
    /// </summary>
    /// <param name="sku">The unique SKU of the inventory item.</param>
    /// <response code="200">Inventory item found and returned.</response>
    /// <response code="404">No inventory item with the given SKU exists.</response>
    [HttpGet("sku/{sku}", Name = nameof(GetInventoryItemBySku))]
    [ProducesResponseType(typeof(InventoryItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInventoryItemBySku(string sku, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetInventoryItemBySkuQuery(sku), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Returns a single inventory item by ID.
    /// </summary>
    /// <param name="id">Inventory item identifier.</param>
    /// <response code="200">Inventory item found and returned.</response>
    /// <response code="404">No inventory item with the given ID exists.</response>
    [HttpGet("{id:int}", Name = nameof(GetInventoryItemById))]
    [ProducesResponseType(typeof(InventoryItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInventoryItemById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetInventoryItemByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Creates a new inventory item.
    /// </summary>
    /// <param name="request">Inventory item data.</param>
    /// <response code="201">Inventory item created. Returns the new item ID.</response>
    /// <response code="400">Validation error in the request body.</response>
    [HttpPost]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateInventoryItemRequest request, CancellationToken ct)
    {
        if (!Enum.TryParse<InventoryItemType>(request.Type, true, out var itemType))
            return BadRequest($"Invalid Type value: '{request.Type}'.");

        if (!Enum.TryParse<InventoryCategory>(request.Category, true, out var itemCategory))
            return BadRequest($"Invalid Category value: '{request.Category}'.");

        var command = new CreateInventoryItemCommand(
            request.Name,
            request.SKU,
            request.Description,
            itemType,
            itemCategory,
            request.InitialStock,
            request.SalePrice);

        var id = await _mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetInventoryItemById), new { id }, new { id });
    }

    /// <summary>
    /// Updates an existing inventory item's mutable fields.
    /// </summary>
    /// <param name="id">Inventory item identifier.</param>
    /// <param name="request">Updated inventory item data.</param>
    /// <response code="204">Inventory item updated successfully.</response>
    /// <response code="400">Validation error in the request body.</response>
    /// <response code="404">No inventory item with the given ID exists.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInventoryItemRequest request, CancellationToken ct)
    {
        if (!Enum.TryParse<InventoryItemType>(request.Type, true, out var itemType))
            return BadRequest($"Invalid Type value: '{request.Type}'.");

        if (!Enum.TryParse<InventoryCategory>(request.Category, true, out var itemCategory))
            return BadRequest($"Invalid Category value: '{request.Category}'.");

        var command = new UpdateInventoryItemCommand(
            id,
            request.Name,
            request.Description,
            itemType,
            itemCategory,
            request.SalePrice);

        await _mediator.Send(command, ct);
        return NoContent();
    }

    /// <summary>
    /// Adjusts the stock quantity of an inventory item by a positive or negative delta.
    /// </summary>
    /// <param name="id">Inventory item identifier.</param>
    /// <param name="request">Delta value to apply to current stock.</param>
    /// <response code="204">Stock adjusted successfully.</response>
    /// <response code="400">Validation error in the request body.</response>
    /// <response code="404">No inventory item with the given ID exists.</response>
    [HttpPatch("{id:int}/stock")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AdjustStock(int id, [FromBody] AdjustStockRequest request, CancellationToken ct)
    {
        await _mediator.Send(new AdjustStockCommand(id, request.Delta), ct);
        return NoContent();
    }

    /// <summary>
    /// Deactivates (soft-deletes) an inventory item.
    /// </summary>
    /// <param name="id">Inventory item identifier.</param>
    /// <response code="204">Inventory item deactivated successfully.</response>
    /// <response code="404">No inventory item with the given ID exists.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(int id, CancellationToken ct)
    {
        await _mediator.Send(new DeactivateInventoryItemCommand(id), ct);
        return NoContent();
    }
}
