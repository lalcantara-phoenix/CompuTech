using CompuTech.Application.Equipment.DTOs;
using MediatR;

namespace CompuTech.Application.Equipment.Queries.GetAllEquipment;

public record GetAllEquipmentQuery : IRequest<IReadOnlyList<EquipmentDto>>;
