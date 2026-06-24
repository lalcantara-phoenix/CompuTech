using CompuTech.Application.Inventory.DTOs;
using CompuTech.Domain.Enums;
using MediatR;

namespace CompuTech.Application.Inventory.Queries.GetAllInventoryItems;

public record GetAllInventoryItemsQuery(
    InventoryItemType? Type = null,
    InventoryCategory? Category = null,
    bool? InStockOnly = null
) : IRequest<IReadOnlyList<InventoryItemDto>>;
