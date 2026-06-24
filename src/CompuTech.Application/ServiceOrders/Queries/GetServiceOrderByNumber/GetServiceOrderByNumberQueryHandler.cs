using MediatR;
using CompuTech.Application.ServiceOrders.DTOs;
using CompuTech.Domain.Interfaces;
using ServiceOrderEntity = CompuTech.Domain.Entities.ServiceOrder;
using ServiceOrderItemEntity = CompuTech.Domain.Entities.ServiceOrderItem;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrderByNumber;

public class GetServiceOrderByNumberQueryHandler(IServiceOrderRepository serviceOrderRepository)
    : IRequestHandler<GetServiceOrderByNumberQuery, ServiceOrderDetailDto>
{
    public async Task<ServiceOrderDetailDto> Handle(GetServiceOrderByNumberQuery request, CancellationToken ct)
    {
        var order = await serviceOrderRepository.GetByNumberAsync(request.OrderNumber, ct)
            ?? throw new KeyNotFoundException($"ServiceOrder '{request.OrderNumber}' not found");

        return MapToDetailDto(order);
    }

    private static ServiceOrderDetailDto MapToDetailDto(ServiceOrderEntity order)
    {
        var items = order.Items.Select(MapItemToDto).ToList();
        var total = items.Sum(i => i.Subtotal);

        return new ServiceOrderDetailDto(
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
            order.CreatedAt,
            items,
            total
        );
    }

    private static ServiceOrderItemDto MapItemToDto(ServiceOrderItemEntity item) => new(
        item.Id,
        item.ServiceOrderId,
        item.ItemType.ToString(),
        item.InventoryItemId,
        item.Description,
        item.Quantity,
        item.UnitPrice,
        item.Subtotal,
        item.Notes
    );
}
