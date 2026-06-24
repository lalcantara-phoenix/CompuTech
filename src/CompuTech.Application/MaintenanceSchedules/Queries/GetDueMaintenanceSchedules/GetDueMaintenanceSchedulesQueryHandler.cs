using CompuTech.Application.MaintenanceSchedules.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Queries.GetDueMaintenanceSchedules;

public class GetDueMaintenanceSchedulesQueryHandler(IMaintenanceScheduleRepository repository)
    : IRequestHandler<GetDueMaintenanceSchedulesQuery, IReadOnlyList<MaintenanceScheduleDto>>
{
    public async Task<IReadOnlyList<MaintenanceScheduleDto>> Handle(
        GetDueMaintenanceSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        var schedules = await repository.GetDueAsync(request.ReferenceDate, cancellationToken);

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
