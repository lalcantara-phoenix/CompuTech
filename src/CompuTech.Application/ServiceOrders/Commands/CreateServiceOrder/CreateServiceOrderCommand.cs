using MediatR;

namespace CompuTech.Application.ServiceOrders.Commands.CreateServiceOrder;

public record CreateServiceOrderCommand(
    int EquipmentId,
    int TechnicianId,
    string Type,
    string SubType,
    string Priority,
    string Description,
    DateTime? ScheduledAt = null,
    int? ScheduleId = null
) : IRequest<int>;
