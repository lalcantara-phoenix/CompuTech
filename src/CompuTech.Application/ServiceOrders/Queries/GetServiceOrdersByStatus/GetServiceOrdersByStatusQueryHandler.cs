using MediatR;
using CompuTech.Application.ServiceOrders.DTOs;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using ServiceOrderEntity = CompuTech.Domain.Entities.ServiceOrder;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrdersByStatus;

public class GetServiceOrdersByStatusQueryHandler(IServiceOrderRepository serviceOrderRepository)
    : IRequestHandler<GetServiceOrdersByStatusQuery, IReadOnlyList<ServiceOrderDto>>
{
    public async Task<IReadOnlyList<ServiceOrderDto>> Handle(GetServiceOrdersByStatusQuery request, CancellationToken ct)
    {
        var status = Enum.Parse<ServiceOrderStatus>(request.Status);
        var orders = await serviceOrderRepository.GetByStatusAsync(status, ct);
        return orders.Select(MapToDto).ToList();
    }

    private static ServiceOrderDto MapToDto(ServiceOrderEntity order) => new(
        order.Id,
        order.OrderNumber,
        order.EquipmentId,
        order.TechnicianId,
        order.ScheduleId,
        order.Type.ToString(),
        order.SubType.ToString(),
        order.Status.ToString(),
        order.Priority.ToString(),
        order.Description,
        order.DiagnosisNotes,
        order.ResolutionNotes,
        order.ScheduledAt,
        order.StartedAt,
        order.CompletedAt,
        order.CreatedAt
    );
}
