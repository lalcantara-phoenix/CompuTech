using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Commands.CreateMaintenanceSchedule;

public record CreateMaintenanceScheduleCommand(
    int EquipmentId,
    string Frequency,
    DateTime NextServiceDate
) : IRequest<int>;
