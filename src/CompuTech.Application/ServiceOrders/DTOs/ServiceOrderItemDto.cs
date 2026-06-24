namespace CompuTech.Application.ServiceOrders.DTOs;

public record ServiceOrderItemDto(
    int Id,
    int ServiceOrderId,
    string ItemType,
    int? InventoryItemId,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    string? Notes
);
