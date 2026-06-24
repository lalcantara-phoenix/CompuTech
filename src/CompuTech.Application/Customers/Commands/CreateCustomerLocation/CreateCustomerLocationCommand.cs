using MediatR;

namespace CompuTech.Application.Customers.Commands.CreateCustomerLocation;

public record CreateCustomerLocationCommand(
    int CustomerId,
    string Name,
    string Address,
    string City,
    string? ContactPhone) : IRequest<int>;
