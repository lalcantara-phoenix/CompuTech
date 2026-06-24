using MediatR;

namespace CompuTech.Application.Customers.Commands.DeactivateCustomerLocation;

public record DeactivateCustomerLocationCommand(int Id) : IRequest;
