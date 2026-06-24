namespace CompuTech.API.Models.MaintenanceSchedules;

public record CreateMaintenanceScheduleRequest(
    int EquipmentId,
    string Frequency,
    DateTime NextServiceDate);

public record UpdateMaintenanceScheduleRequest(
    string Frequency,
    DateTime NextServiceDate);
