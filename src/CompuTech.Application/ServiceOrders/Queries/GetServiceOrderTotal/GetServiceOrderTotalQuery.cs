using MediatR;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrderTotal;

public record GetServiceOrderTotalQuery(int ServiceOrderId) : IRequest<decimal>;
