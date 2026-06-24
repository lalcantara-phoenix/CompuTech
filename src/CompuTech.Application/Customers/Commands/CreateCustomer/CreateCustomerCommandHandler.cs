using CompuTech.Domain.Entities;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler(ICustomerRepository repository)
    : IRequestHandler<CreateCustomerCommand, int>
{
    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.FullName, request.Email, request.Phone, request.TaxId);
        await repository.AddAsync(customer, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return customer.Id;
    }
}
