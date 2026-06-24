using MediatR;

namespace CompuTech.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(
    int Id,
    string FullName,
    string Phone,
    string? TaxId) : IRequest;
