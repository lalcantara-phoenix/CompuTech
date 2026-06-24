using CompuTech.Application.Equipment.DTOs;
using MediatR;

namespace CompuTech.Application.Equipment.Queries.GetEquipmentByCustomer;

public record GetEquipmentByCustomerQuery(int CustomerId) : IRequest<IReadOnlyList<EquipmentDto>>;
