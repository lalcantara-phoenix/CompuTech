using MediatR;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.ServiceOrders.Commands.UpdateServiceOrder;

public class UpdateServiceOrderCommandHandler(
    IServiceOrderRepository serviceOrderRepository,
    ITechnicianRepository technicianRepository)
    : IRequestHandler<UpdateServiceOrderCommand>
{
    public async Task Handle(UpdateServiceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await serviceOrderRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"ServiceOrder {request.Id} not found");

        if (order.Status != ServiceOrderStatus.Planned)
            throw new InvalidOperationException("Only Planned orders can be updated");

        var technician = await technicianRepository.GetByIdAsync(request.TechnicianId, cancellationToken)
            ?? throw new KeyNotFoundException($"Technician {request.TechnicianId} not found");

        if (!technician.IsActive)
            throw new InvalidOperationException("Technician is not active");

        var priority = Enum.Parse<ServiceOrderPriority>(request.Priority);

        order.Update(request.TechnicianId, priority, request.Description, request.ScheduledAt);

        await serviceOrderRepository.UpdateAsync(order, cancellationToken);
    }
}
