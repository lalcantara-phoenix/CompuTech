using MediatR;
using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.ServiceOrders.Commands.AddServiceOrderItem;

public class AddServiceOrderItemCommandHandler(
    IServiceOrderRepository serviceOrderRepository,
    IServiceOrderItemRepository serviceOrderItemRepository,
    IInventoryItemRepository inventoryItemRepository)
    : IRequestHandler<AddServiceOrderItemCommand>
{
    public async Task Handle(AddServiceOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await serviceOrderRepository.GetByIdAsync(request.ServiceOrderId, cancellationToken)
            ?? throw new KeyNotFoundException($"ServiceOrder {request.ServiceOrderId} not found");

        if (order.Status == ServiceOrderStatus.Completed || order.Status == ServiceOrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot add items to a completed or cancelled service order");

        var itemType = Enum.Parse<ServiceOrderItemType>(request.ItemType);

        if (itemType == ServiceOrderItemType.Part)
        {
            var inventoryItem = await inventoryItemRepository.GetByIdAsync(request.InventoryItemId!.Value, cancellationToken)
                ?? throw new KeyNotFoundException($"InventoryItem {request.InventoryItemId.Value} not found");

            if (!inventoryItem.IsActive)
                throw new InvalidOperationException("InventoryItem is not active");

            inventoryItem.AdjustStock(-request.Quantity);
            await inventoryItemRepository.UpdateAsync(inventoryItem, cancellationToken);
        }

        var item = ServiceOrderItem.Create(
            request.ServiceOrderId,
            itemType,
            request.Description,
            request.Quantity,
            request.UnitPrice,
            request.InventoryItemId,
            request.Notes);

        await serviceOrderItemRepository.AddAsync(item, cancellationToken);
    }
}
