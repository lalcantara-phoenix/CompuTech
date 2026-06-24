namespace CompuTech.API.Models.Inventory;

/// <summary>Request body for creating a new inventory item.</summary>
public record CreateInventoryItemRequest(
    string Name,
    string SKU,
    string? Description,
    string Type,
    string Category,
    int InitialStock,
    decimal SalePrice
);

/// <summary>Request body for updating mutable fields of an existing inventory item.</summary>
public record UpdateInventoryItemRequest(
    string Name,
    string? Description,
    string Type,
    string Category,
    decimal SalePrice
);

/// <summary>Request body for adjusting the stock quantity of an inventory item.</summary>
public record AdjustStockRequest(int Delta);
