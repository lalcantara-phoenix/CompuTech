using MediatR;

namespace CompuTech.Application.ServiceOrders.Commands.CompleteServiceOrder;

public record CompleteServiceOrderCommand(int Id, string? ResolutionNotes = null) : IRequest;
