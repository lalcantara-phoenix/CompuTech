namespace CompuTech.Application.MaintenanceSchedules.DTOs;

public class MaintenanceScheduleDto
{
    public int Id { get; init; }
    public int EquipmentId { get; init; }
    public string Frequency { get; init; } = default!;
    public DateTime? LastServiceDate { get; init; }
    public DateTime NextServiceDate { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
}
