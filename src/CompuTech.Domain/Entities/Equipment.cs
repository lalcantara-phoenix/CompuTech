using CompuTech.Domain.Enums;

namespace CompuTech.Domain.Entities;

public class Equipment
{
    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public int LocationId { get; private set; }
    public string SerialNumber { get; private set; } = default!;
    public string Brand { get; private set; } = default!;
    public string Model { get; private set; } = default!;
    public EquipmentType Type { get; private set; }
    public string? OperatingSystem { get; private set; }
    public DateTime? PurchaseDate { get; private set; }
    public string? Notes { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Equipment() { }

    public static Equipment Create(
        int customerId,
        int locationId,
        string serialNumber,
        string brand,
        string model,
        EquipmentType type,
        string? operatingSystem = null,
        DateTime? purchaseDate = null,
        string? notes = null)
    {
        return new Equipment
        {
            CustomerId = customerId,
            LocationId = locationId,
            SerialNumber = serialNumber,
            Brand = brand,
            Model = model,
            Type = type,
            OperatingSystem = operatingSystem,
            PurchaseDate = purchaseDate,
            Notes = notes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string brand, string model, EquipmentType type)
    {
        Brand = brand;
        Model = model;
        Type = type;
    }

    public void Deactivate() => IsActive = false;
}
