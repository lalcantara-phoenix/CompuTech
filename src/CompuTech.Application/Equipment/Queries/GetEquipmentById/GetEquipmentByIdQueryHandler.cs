using CompuTech.Application.Equipment.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Equipment.Queries.GetEquipmentById;

public class GetEquipmentByIdQueryHandler : IRequestHandler<GetEquipmentByIdQuery, EquipmentDto?>
{
    private readonly IEquipmentRepository _repository;

    public GetEquipmentByIdQueryHandler(IEquipmentRepository repository)
        => _repository = repository;

    public async Task<EquipmentDto?> Handle(GetEquipmentByIdQuery request, CancellationToken ct)
    {
        var equipment = await _repository.GetByIdAsync(request.Id, ct);

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
