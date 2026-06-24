using CompuTech.Application.MaintenanceSchedules.DTOs;
using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Queries.GetAllMaintenanceSchedules;

public record GetAllMaintenanceSchedulesQuery : IRequest<IReadOnlyList<MaintenanceScheduleDto>>;
