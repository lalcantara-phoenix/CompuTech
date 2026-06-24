using CompuTech.Application.Equipment.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Equipment.Queries.GetEquipmentBySerialNumber;

public class GetEquipmentBySerialNumberQueryHandler : IRequestHandler<GetEquipmentBySerialNumberQuery, EquipmentDto?>
{
    private readonly IEquipmentRepository _repository;

    public GetEquipmentBySerialNumberQueryHandler(IEquipmentRepository repository)
        => _repository = repository;

    public async Task<EquipmentDto?> Handle(GetEquipmentBySerialNumberQuery request, CancellationToken ct)
    {
        var equipment = await _repository.GetBySerialNumberAsync(request.SerialNumber, ct);

        if (equipment is null)
            return null;

        return new EquipmentDto(
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
            equipment.CreatedAt);
    }
}
