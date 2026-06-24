using MediatR;

namespace CompuTech.Application.ServiceOrders.Commands.DelayServiceOrder;

public record DelayServiceOrderCommand(int Id) : IRequest;
