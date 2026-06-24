using MediatR;
using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.ServiceOrders.Commands.CreateServiceOrder;

public class CreateServiceOrderCommandHandler(
    IServiceOrderRepository serviceOrderRepository,
    IEquipmentRepository equipmentRepository,
    ITechnicianRepository technicianRepository)
    : IRequestHandler<CreateServiceOrderCommand, int>
{
    public async Task<int> Handle(CreateServiceOrderCommand request, CancellationToken cancellationToken)
    {
        var equipment = await equipmentRepository.GetByIdAsync(request.EquipmentId, cancellationToken)
            ?? throw new KeyNotFoundException($"Equipment {request.EquipmentId} not found");

        if (!equipment.IsActive)
            throw new InvalidOperationException("Equipment is not active");

        var technician = await technicianRepository.GetByIdAsync(request.TechnicianId, cancellationToken)
            ?? throw new KeyNotFoundException($"Technician {request.TechnicianId} not found");

        if (!technician.IsActive)
            throw new InvalidOperationException("Technician is not active");

        var type = Enum.Parse<ServiceOrderType>(request.Type);
        var subType = Enum.Parse<ServiceOrderSubType>(request.SubType);
        var priority = Enum.Parse<ServiceOrderPriority>(request.Priority);

        var year = DateTime.UtcNow.Year;
        var sequence = await serviceOrderRepository.GetNextSequenceAsync(year, cancellationToken);
        var orderNumber = ServiceOrder.GenerateOrderNumber(year, sequence);

        var order = ServiceOrder.Create(
            orderNumber,
            request.EquipmentId,
            request.TechnicianId,
            type,
            subType,
            priority,
            request.Description,
            request.ScheduledAt,
            request.ScheduleId);

        return await serviceOrderRepository.AddAsync(order, cancellationToken);
    }
}
