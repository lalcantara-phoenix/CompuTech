using MediatR;
using CompuTech.Application.Common.Interfaces;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.ServiceOrders.Commands.CompleteServiceOrder;

public class CompleteServiceOrderCommandHandler(
    IServiceOrderRepository serviceOrderRepository,
    IGenerateNextMaintenanceOrderService generateNextMaintenanceOrderService)
    : IRequestHandler<CompleteServiceOrderCommand>
{
    public async Task Handle(CompleteServiceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await serviceOrderRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"ServiceOrder {request.Id} not found");

        if (order.Status != ServiceOrderStatus.InProgress)
            throw new InvalidOperationException(
                $"Only InProgress orders can be completed. Current status: {order.Status}");

        order.Complete(request.ResolutionNotes);
        await serviceOrderRepository.UpdateAsync(order, cancellationToken);

        // Regla #8: auto-generate next order for Preventive maintenance
        if (order.SubType == ServiceOrderSubType.Preventive)
            await generateNextMaintenanceOrderService.GenerateAsync(order, cancellationToken);
    }
}
