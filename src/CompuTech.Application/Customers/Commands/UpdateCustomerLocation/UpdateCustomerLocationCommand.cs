using MediatR;

namespace CompuTech.Application.Customers.Commands.UpdateCustomerLocation;

public record UpdateCustomerLocationCommand(
    int Id,
    string Name,
    string Address,
    string City,
    string? ContactPhone) : IRequest;
