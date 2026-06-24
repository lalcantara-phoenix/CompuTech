using MediatR;

namespace CompuTech.Application.ServiceOrders.Commands.UpdateServiceOrder;

public record UpdateServiceOrderCommand(
    int Id,
    int TechnicianId,
    string Priority,
    string Description,
    DateTime? ScheduledAt = null
) : IRequest;
