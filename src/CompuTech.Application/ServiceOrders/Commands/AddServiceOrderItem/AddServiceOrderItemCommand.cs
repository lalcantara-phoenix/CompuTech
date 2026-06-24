using MediatR;

namespace CompuTech.Application.ServiceOrders.Commands.AddServiceOrderItem;

public record AddServiceOrderItemCommand(
    int ServiceOrderId,
    string ItemType,
    string Description,
    int Quantity,
    decimal UnitPrice,
    int? InventoryItemId = null,
    string? Notes = null
) : IRequest;
