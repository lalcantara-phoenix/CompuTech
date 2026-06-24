using MediatR;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.ServiceOrders.Commands.RemoveServiceOrderItem;

public class RemoveServiceOrderItemCommandHandler(
    IServiceOrderRepository serviceOrderRepository,
    IServiceOrderItemRepository serviceOrderItemRepository,
    IInventoryItemRepository inventoryItemRepository)
    : IRequestHandler<RemoveServiceOrderItemCommand>
{
    public async Task Handle(RemoveServiceOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await serviceOrderRepository.GetByIdAsync(request.ServiceOrderId, cancellationToken)
            ?? throw new KeyNotFoundException($"ServiceOrder {request.ServiceOrderId} not found");

        if (order.Status == ServiceOrderStatus.Completed || order.Status == ServiceOrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot remove items from a completed or cancelled service order");

        var item = await serviceOrderItemRepository.GetByIdAsync(request.ItemId, cancellationToken)
            ?? throw new KeyNotFoundException($"ServiceOrderItem {request.ItemId} not found");

        if (item.ServiceOrderId != request.ServiceOrderId)
            throw new InvalidOperationException("Item does not belong to this order");

        if (item.ItemType == ServiceOrderItemType.Part)
        {
            var inventoryItem = await inventoryItemRepository.GetByIdAsync(item.InventoryItemId!.Value, cancellationToken);
            if (inventoryItem is not null)
            {
                inventoryItem.AdjustStock(item.Quantity);
                await inventoryItemRepository.UpdateAsync(inventoryItem, cancellationToken);
            }
        }

        await serviceOrderItemRepository.RemoveAsync(item, cancellationToken);
    }
}
