using MediatR;

namespace CompuTech.Application.ServiceOrders.Commands.StartServiceOrder;

public record StartServiceOrderCommand(int Id) : IRequest;
