using MediatR;

namespace CompuTech.Application.Customers.Commands.DeactivateCustomer;

public record DeactivateCustomerCommand(int Id) : IRequest;
