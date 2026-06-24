using MediatR;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.ServiceOrders.Commands.DelayServiceOrder;

public class DelayServiceOrderCommandHandler(IServiceOrderRepository serviceOrderRepository)
    : IRequestHandler<DelayServiceOrderCommand>
{
    public async Task Handle(DelayServiceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await serviceOrderRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"ServiceOrder {request.Id} not found");

        if (order.Status != ServiceOrderStatus.Planned)
            throw new InvalidOperationException(
                $"Only Planned orders can be delayed. Current status: {order.Status}");

        order.Delay();

        await serviceOrderRepository.UpdateAsync(order, cancellationToken);
    }
}
