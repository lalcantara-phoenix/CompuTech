using CompuTech.Domain.Enums;
using MediatR;

namespace CompuTech.Application.Inventory.Commands.UpdateInventoryItem;

public record UpdateInventoryItemCommand(
    int Id,
    string Name,
    string? Description,
    InventoryItemType Type,
    InventoryCategory Category,
    decimal SalePrice
) : IRequest;
