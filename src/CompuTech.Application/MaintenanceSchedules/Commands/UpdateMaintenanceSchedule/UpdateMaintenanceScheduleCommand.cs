using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Commands.UpdateMaintenanceSchedule;

public record UpdateMaintenanceScheduleCommand(
    int Id,
    string Frequency,
    DateTime NextServiceDate
) : IRequest;
