using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Commands.DeactivateMaintenanceSchedule;

public class DeactivateMaintenanceScheduleCommandHandler(
    IMaintenanceScheduleRepository maintenanceScheduleRepository)
    : IRequestHandler<DeactivateMaintenanceScheduleCommand>
{
    public async Task Handle(DeactivateMaintenanceScheduleCommand request, CancellationToken ct)
    {
        var schedule = await maintenanceScheduleRepository.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException($"MaintenanceSchedule with id {request.Id} not found");

        schedule.Deactivate();
        await maintenanceScheduleRepository.UpdateAsync(schedule, ct);
    }
}
