using CompuTech.Application.Equipment.DTOs;
using MediatR;

namespace CompuTech.Application.Equipment.Queries.GetEquipmentById;

public record GetEquipmentByIdQuery(int Id) : IRequest<EquipmentDto?>;
