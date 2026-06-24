using CompuTech.Application.MaintenanceSchedules.DTOs;
using MediatR;

namespace CompuTech.Application.MaintenanceSchedules.Queries.GetMaintenanceScheduleByEquipment;

public record GetMaintenanceScheduleByEquipmentQuery(int EquipmentId) : IRequest<MaintenanceScheduleDto?>;
