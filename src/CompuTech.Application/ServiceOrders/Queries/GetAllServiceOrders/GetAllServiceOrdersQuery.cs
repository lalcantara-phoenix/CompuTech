using MediatR;
using CompuTech.Application.ServiceOrders.DTOs;

namespace CompuTech.Application.ServiceOrders.Queries.GetAllServiceOrders;

public record GetAllServiceOrdersQuery : IRequest<IReadOnlyList<ServiceOrderDto>>;
