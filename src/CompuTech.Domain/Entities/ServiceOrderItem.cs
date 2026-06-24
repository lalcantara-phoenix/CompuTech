using CompuTech.Domain.Enums;

namespace CompuTech.Domain.Entities;

public class ServiceOrderItem
{
    public int Id { get; private set; }
    public int ServiceOrderId { get; private set; }
    public ServiceOrderItemType ItemType { get; private set; }
    public int? InventoryItemId { get; private set; }
    public string Description { get; private set; } = default!;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Subtotal { get; private set; }
    public string? Notes { get; private set; }

    private ServiceOrderItem() { }

    public static ServiceOrderItem Create(
        int serviceOrderId,
        ServiceOrderItemType itemType,
        string description,
        int quantity,
        decimal unitPrice,
        int? inventoryItemId = null,
        string? notes = null)
    {
        return new ServiceOrderItem
        {
            ServiceOrderId = serviceOrderId,
            ItemType = itemType,
            InventoryItemId = inventoryItemId,
            Description = description,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Subtotal = quantity * unitPrice,
            Notes = notes
        };
    }
}
