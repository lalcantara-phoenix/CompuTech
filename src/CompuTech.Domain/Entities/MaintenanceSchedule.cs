using CompuTech.Domain.Enums;

namespace CompuTech.Domain.Entities;

public class MaintenanceSchedule
{
    public int Id { get; private set; }
    public int EquipmentId { get; private set; }
    public MaintenanceFrequency Frequency { get; private set; }
    public DateTime? LastServiceDate { get; private set; }
    public DateTime NextServiceDate { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private MaintenanceSchedule() { }

    public static MaintenanceSchedule Create(int equipmentId, MaintenanceFrequency frequency, DateTime nextServiceDate)
    {
        return new MaintenanceSchedule
        {
            EquipmentId = equipmentId,
            Frequency = frequency,
            NextServiceDate = nextServiceDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(MaintenanceFrequency frequency, DateTime nextServiceDate)
    {
        Frequency = frequency;
        NextServiceDate = nextServiceDate;
    }

    public void Deactivate() => IsActive = false;

    // Actualiza fechas al completar un mantenimiento preventivo
    public void RecordServiceCompletion(DateTime serviceDate)
    {
        LastServiceDate = serviceDate;
        NextServiceDate = serviceDate.AddMonths((int)Frequency);
    }
}
