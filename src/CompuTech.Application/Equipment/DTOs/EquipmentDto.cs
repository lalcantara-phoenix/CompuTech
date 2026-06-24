namespace CompuTech.Application.Equipment.DTOs;

public record EquipmentDto(
    int Id,
    int CustomerId,
    int LocationId,
    string SerialNumber,
    string Brand,
    string Model,
    string Type,
    string? OperatingSystem,
    DateTime? PurchaseDate,
    string? Notes,
    bool IsActive,
    DateTime CreatedAt
);
