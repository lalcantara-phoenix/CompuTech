using MediatR;
using CompuTech.Application.ServiceOrders.DTOs;

namespace CompuTech.Application.ServiceOrders.Queries.GetServiceOrdersByTechnician;

public record GetServiceOrdersByTechnicianQuery(int TechnicianId) : IRequest<IReadOnlyList<ServiceOrderDto>>;
