using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Customers.Commands.DeactivateCustomer;

public class DeactivateCustomerCommandHandler(ICustomerRepository repository)
    : IRequestHandler<DeactivateCustomerCommand>
{
    public async Task Handle(DeactivateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Cliente con ID {request.Id} no encontrado.");

        customer.Deactivate();
        repository.Update(customer);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
