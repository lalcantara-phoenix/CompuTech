using CompuTech.Application.Equipment.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Equipment.Queries.GetEquipmentByCustomer;

public class GetEquipmentByCustomerQueryHandler : IRequestHandler<GetEquipmentByCustomerQuery, IReadOnlyList<EquipmentDto>>
{
    private readonly IEquipmentRepository _repository;

    public GetEquipmentByCustomerQueryHandler(IEquipmentRepository repository)
        => _repository = repository;

    public async Task<IReadOnlyList<EquipmentDto>> Handle(GetEquipmentByCustomerQuery request, CancellationToken ct)
    {
        var equipments = await _repository.GetByCustomerIdAsync(request.CustomerId, ct);

        return equipments
            .Select(equipment => new EquipmentDto(
                equipment.Id,
                equipment.CustomerId,
                equipment.LocationId,
                equipment.SerialNumber,
                equipment.Brand,
                equipment.Model,
                equipment.Type.ToString(),
                equipment.OperatingSystem,
                equipment.PurchaseDate,
                equipment.Notes,
                equipment.IsActive,
                equipment.CreatedAt))
            .ToList()
            .AsReadOnly();
    }
}
