using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Commands.DeactivateMaintenanceSchedule;

public record DeactivateMaintenanceScheduleCommand(int Id) : IRequest;
