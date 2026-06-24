using CompuTech.Application.Equipment.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Equipment.Queries.GetAllEquipment;

public class GetAllEquipmentQueryHandler : IRequestHandler<GetAllEquipmentQuery, IReadOnlyList<EquipmentDto>>
{
    private readonly IEquipmentRepository _repository;

    public GetAllEquipmentQueryHandler(IEquipmentRepository repository)
        => _repository = repository;

    public async Task<IReadOnlyList<EquipmentDto>> Handle(GetAllEquipmentQuery request, CancellationToken ct)
    {
        var equipments = await _repository.GetAllAsync(ct);

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
