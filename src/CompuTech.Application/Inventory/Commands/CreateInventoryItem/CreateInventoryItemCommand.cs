using CompuTech.Domain.Enums;
using MediatR;

namespace CompuTech.Application.Inventory.Commands.CreateInventoryItem;

public record CreateInventoryItemCommand(
    string Name,
    string SKU,
    string? Description,
    InventoryItemType Type,
    InventoryCategory Category,
    int InitialStock,
    decimal SalePrice
) : IRequest<int>;
