using MediatR;

namespace CompuTech.Application.ServiceOrders.Commands.RemoveServiceOrderItem;

public record RemoveServiceOrderItemCommand(int ServiceOrderId, int ItemId) : IRequest;
