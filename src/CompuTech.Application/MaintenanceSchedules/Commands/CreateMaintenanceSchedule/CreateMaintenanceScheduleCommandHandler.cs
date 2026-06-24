using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using MediatR;
using MaintenanceScheduleEntity = CompuTech.Domain.Entities.MaintenanceSchedule;

namespace CompuTech.Application.MaintenanceSchedules.Commands.CreateMaintenanceSchedule;

public class CreateMaintenanceScheduleCommandHandler(
    IMaintenanceScheduleRepository maintenanceScheduleRepository,
    IEquipmentRepository equipmentRepository)
    : IRequestHandler<CreateMaintenanceScheduleCommand, int>
{
    public async Task<int> Handle(CreateMaintenanceScheduleCommand request, CancellationToken ct)
    {
        var equipment = await equipmentRepository.GetByIdAsync(request.EquipmentId, ct)
            ?? throw new KeyNotFoundException($"Equipment with id {request.EquipmentId} not found");

        if (!equipment.IsActive)
            throw new InvalidOperationException("Equipment is not active");

        var existing = await maintenanceScheduleRepository.GetByEquipmentIdAsync(request.EquipmentId, ct);
        if (existing is not null && existing.IsActive)
            throw new InvalidOperationException("Equipment already has an active maintenance schedule");

        var frequency = Enum.Parse<MaintenanceFrequency>(request.Frequency);

        var schedule = MaintenanceScheduleEntity.Create(request.EquipmentId, frequency, request.NextServiceDate);

        await maintenanceScheduleRepository.AddAsync(schedule, ct);
        return schedule.Id;
    }
}
