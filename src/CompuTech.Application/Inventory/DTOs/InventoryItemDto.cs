namespace CompuTech.Application.Inventory.DTOs;

public record InventoryItemDto(
    int Id,
    string Name,
    string SKU,
    string? Description,
    string Type,
    string Category,
    int StockQuantity,
    decimal SalePrice,
    bool IsActive,
    DateTime CreatedAt
);
