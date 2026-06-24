using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler(ICustomerRepository repository)
    : IRequestHandler<UpdateCustomerCommand>
{
    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Cliente con ID {request.Id} no encontrado.");

        customer.Update(request.FullName, request.Phone, request.TaxId);
        repository.Update(customer);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
