using MediatR;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.ServiceOrders.Commands.StartServiceOrder;

public class StartServiceOrderCommandHandler(IServiceOrderRepository serviceOrderRepository)
    : IRequestHandler<StartServiceOrderCommand>
{
    public async Task Handle(StartServiceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await serviceOrderRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"ServiceOrder {request.Id} not found");

        if (order.Status != ServiceOrderStatus.Planned)
            throw new InvalidOperationException(
                $"Only Planned orders can be started. Current status: {order.Status}");

        order.Start();

        await serviceOrderRepository.UpdateAsync(order, cancellationToken);
    }
}
