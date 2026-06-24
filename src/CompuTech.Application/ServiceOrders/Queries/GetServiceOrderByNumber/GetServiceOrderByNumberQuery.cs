using MediatR;
using CompuTech.Application.ServiceOrders.DTOs;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrderByNumber;

public record GetServiceOrderByNumberQuery(string OrderNumber) : IRequest<ServiceOrderDetailDto>;
