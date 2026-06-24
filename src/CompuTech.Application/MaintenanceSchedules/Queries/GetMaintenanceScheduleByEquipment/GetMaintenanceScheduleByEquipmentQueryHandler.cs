using CompuTech.Application.MaintenanceSchedules.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Queries.GetMaintenanceScheduleByEquipment;

public class GetMaintenanceScheduleByEquipmentQueryHandler(IMaintenanceScheduleRepository repository)
    : IRequestHandler<GetMaintenanceScheduleByEquipmentQuery, MaintenanceScheduleDto?>
{
    public async Task<MaintenanceScheduleDto?> Handle(
        GetMaintenanceScheduleByEquipmentQuery request,
        CancellationToken cancellationToken)
    {
        var schedule = await repository.GetByEquipmentIdAsync(request.EquipmentId, cancellationToken);
        if (schedule is null)
            return null;

        return new MaintenanceScheduleDto
        {
            Id = schedule.Id,
            EquipmentId = schedule.EquipmentId,
            Frequency = schedule.Frequency.ToString(),
            LastServiceDate = schedule.LastServiceDate,
            NextServiceDate = schedule.NextServiceDate,
            IsActive = schedule.IsActive,
            CreatedAt = schedule.CreatedAt
        };
    }
}
