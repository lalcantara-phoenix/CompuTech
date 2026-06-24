using MediatR;

namespace CompuTech.Application.ServiceOrders.Commands.CancelServiceOrder;

public record CancelServiceOrderCommand(int Id) : IRequest;
