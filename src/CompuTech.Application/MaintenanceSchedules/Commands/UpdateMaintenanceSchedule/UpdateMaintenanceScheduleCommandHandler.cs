using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Commands.UpdateMaintenanceSchedule;

public class UpdateMaintenanceScheduleCommandHandler(
    IMaintenanceScheduleRepository maintenanceScheduleRepository)
    : IRequestHandler<UpdateMaintenanceScheduleCommand>
{
    public async Task Handle(UpdateMaintenanceScheduleCommand request, CancellationToken ct)
    {
        var schedule = await maintenanceScheduleRepository.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException($"MaintenanceSchedule with id {request.Id} not found");

        if (!schedule.IsActive)
            throw new InvalidOperationException("Cannot update an inactive schedule");

        var frequency = Enum.Parse<MaintenanceFrequency>(request.Frequency);

        schedule.Update(frequency, request.NextServiceDate);
        await maintenanceScheduleRepository.UpdateAsync(schedule, ct);
    }
}
