using MediatR;
using CompuTech.Application.ServiceOrders.DTOs;
using CompuTech.Domain.Interfaces;
using ServiceOrderEntity = CompuTech.Domain.Entities.ServiceOrder;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrdersByEquipment;

public class GetServiceOrdersByEquipmentQueryHandler(IServiceOrderRepository serviceOrderRepository)
    : IRequestHandler<GetServiceOrdersByEquipmentQuery, IReadOnlyList<ServiceOrderDto>>
{
    public async Task<IReadOnlyList<ServiceOrderDto>> Handle(GetServiceOrdersByEquipmentQuery request, CancellationToken ct)
    {
        var orders = await serviceOrderRepository.GetByEquipmentIdAsync(request.EquipmentId, ct);
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
