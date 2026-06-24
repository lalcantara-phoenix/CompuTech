using CompuTech.Application.MaintenanceSchedules.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Queries.GetAllMaintenanceSchedules;

public class GetAllMaintenanceSchedulesQueryHandler(IMaintenanceScheduleRepository repository)
    : IRequestHandler<GetAllMaintenanceSchedulesQuery, IReadOnlyList<MaintenanceScheduleDto>>
{
    public async Task<IReadOnlyList<MaintenanceScheduleDto>> Handle(
        GetAllMaintenanceSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        var schedules = await repository.GetAllAsync(cancellationToken);

        return schedules.Select(s => new MaintenanceScheduleDto
        {
            Id = s.Id,
            EquipmentId = s.EquipmentId,
            Frequency = s.Frequency.ToString(),
            LastServiceDate = s.LastServiceDate,
            NextServiceDate = s.NextServiceDate,
            IsActive = s.IsActive,
            CreatedAt = s.CreatedAt
        }).ToList().AsReadOnly();
    }
}
