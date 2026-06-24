using CompuTech.Domain.Interfaces;
using MediatR;
using EquipmentEntity = CompuTech.Domain.Entities.Equipment;

namespace CompuTech.Application.Equipment.Commands.RegisterEquipment;

public class RegisterEquipmentCommandHandler(
    IEquipmentRepository equipmentRepository,
    ICustomerRepository customerRepository,
    ICustomerLocationRepository locationRepository)
    : IRequestHandler<RegisterEquipmentCommand, int>
{
    public async Task<int> Handle(RegisterEquipmentCommand request, CancellationToken ct)
    {
        var customer = await customerRepository.GetByIdAsync(request.CustomerId, ct);
        if (customer is null)
            throw new InvalidOperationException("Customer not found");

        var location = await locationRepository.GetByIdAsync(request.LocationId, ct);
        if (location is null)
            throw new InvalidOperationException("Location not found");

        var existing = await equipmentRepository.GetBySerialNumberAsync(request.SerialNumber, ct);
        if (existing is not null && existing.IsActive)
            throw new InvalidOperationException("Serial number already registered");

        var entity = EquipmentEntity.Create(
            request.CustomerId,
            request.LocationId,
            request.SerialNumber,
            request.Brand,
            request.Model,
            request.Type,
            request.OperatingSystem,
            request.PurchaseDate,
            request.Notes);

        await equipmentRepository.AddAsync(entity, ct);
        return entity.Id;
    }
}
