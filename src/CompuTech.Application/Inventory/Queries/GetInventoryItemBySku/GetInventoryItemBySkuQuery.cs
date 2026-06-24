using CompuTech.Application.Inventory.DTOs;
using MediatR;

namespace CompuTech.Application.Inventory.Queries.GetInventoryItemBySku;

public record GetInventoryItemBySkuQuery(string SKU) : IRequest<InventoryItemDto?>;
