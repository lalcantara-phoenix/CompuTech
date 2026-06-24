using MediatR;
using CompuTech.Application.ServiceOrders.DTOs;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrderById;

public record GetServiceOrderByIdQuery(int Id) : IRequest<ServiceOrderDetailDto>;
