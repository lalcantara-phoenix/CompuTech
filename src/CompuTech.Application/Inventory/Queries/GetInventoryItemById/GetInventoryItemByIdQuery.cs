using CompuTech.Application.Inventory.DTOs;
using MediatR;

namespace CompuTech.Application.Inventory.Queries.GetInventoryItemById;

public record GetInventoryItemByIdQuery(int Id) : IRequest<InventoryItemDto?>;
