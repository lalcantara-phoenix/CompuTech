using MediatR;

namespace CompuTech.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FullName,
    string Email,
    string Phone,
    string? TaxId) : IRequest<int>;
