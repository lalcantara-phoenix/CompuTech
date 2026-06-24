using MediatR;
using CompuTech.Application.ServiceOrders.DTOs;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrdersByStatus;

public record GetServiceOrdersByStatusQuery(string Status) : IRequest<IReadOnlyList<ServiceOrderDto>>;
