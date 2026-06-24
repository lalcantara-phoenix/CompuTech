using MediatR;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.ServiceOrders.Commands.CancelServiceOrder;

public class CancelServiceOrderCommandHandler(IServiceOrderRepository serviceOrderRepository)
    : IRequestHandler<CancelServiceOrderCommand>
{
    public async Task Handle(CancelServiceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await serviceOrderRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"ServiceOrder {request.Id} not found");

        if (order.Status == ServiceOrderStatus.Completed || order.Status == ServiceOrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot cancel a completed or already cancelled order");

        order.Cancel();

        await serviceOrderRepository.UpdateAsync(order, cancellationToken);
    }
}
