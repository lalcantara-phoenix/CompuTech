using CompuTech.Application.MaintenanceSchedules.DTOs;
using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Queries.GetDueMaintenanceSchedules;

public record GetDueMaintenanceSchedulesQuery(DateTime ReferenceDate) : IRequest<IReadOnlyList<MaintenanceScheduleDto>>;
