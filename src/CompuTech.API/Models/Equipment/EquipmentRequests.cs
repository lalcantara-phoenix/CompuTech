namespace CompuTech.API.Models.Equipment;

/// <summary>Request body for registering a new piece of equipment.</summary>
public record RegisterEquipmentRequest(
    int CustomerId,
    int LocationId,
    string SerialNumber,
    string Brand,
    string Model,
    string Type,
    string? OperatingSystem,
    DateTime? PurchaseDate,
    string? Notes
);

/// <summary>Request body for updating mutable fields of an existing equipment record.</summary>
public record UpdateEquipmentRequest(
    string Brand,
    string Model,
    string Type
);
