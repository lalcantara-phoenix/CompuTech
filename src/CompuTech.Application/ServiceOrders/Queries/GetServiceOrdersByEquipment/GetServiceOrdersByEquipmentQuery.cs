using MediatR;
using CompuTech.Application.ServiceOrders.DTOs;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrdersByEquipment;

public record GetServiceOrdersByEquipmentQuery(int EquipmentId) : IRequest<IReadOnlyList<ServiceOrderDto>>;
