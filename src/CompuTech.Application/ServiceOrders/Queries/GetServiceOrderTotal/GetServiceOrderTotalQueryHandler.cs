using MediatR;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrderTotal;

public class GetServiceOrderTotalQueryHandler(IServiceOrderRepository serviceOrderRepository)
    : IRequestHandler<GetServiceOrderTotalQuery, decimal>
{
    public async Task<decimal> Handle(GetServiceOrderTotalQuery request, CancellationToken ct)
    {
        var order = await serviceOrderRepository.GetByIdAsync(request.ServiceOrderId, ct)
            ?? throw new KeyNotFoundException($"ServiceOrder {request.ServiceOrderId} not found");

        return order.Items.Sum(i => i.Subtotal);
    }
}
