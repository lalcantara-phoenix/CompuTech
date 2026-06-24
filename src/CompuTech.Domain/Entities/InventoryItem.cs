using CompuTech.Domain.Enums;

namespace CompuTech.Domain.Entities;

public class InventoryItem
{
    public int Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string SKU { get; private set; } = default!;
    public string? Description { get; private set; }
    public InventoryItemType Type { get; private set; }
    public InventoryCategory Category { get; private set; }
    public int StockQuantity { get; private set; }
    public decimal SalePrice { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private InventoryItem() { }

    public static InventoryItem Create(
        string name,
        string sku,
        string? description,
        InventoryItemType type,
        InventoryCategory category,
        int initialStock,
        decimal salePrice)
    {
        return new InventoryItem
        {
            Name = name,
            SKU = sku,
            Description = description,
            Type = type,
            Category = category,
            StockQuantity = initialStock,
            SalePrice = salePrice,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(
        string name,
        string? description,
        InventoryItemType type,
        InventoryCategory category,
        decimal salePrice)
    {
        Name = name;
        Description = description;
        Type = type;
        Category = category;
        SalePrice = salePrice;
    }

    public void AdjustStock(int delta)
    {
        var result = StockQuantity + delta;
        if (result < 0)
            throw new InvalidOperationException("Stock cannot be negative");

        StockQuantity = result;
    }

    public void Deactivate() => IsActive = false;
}
