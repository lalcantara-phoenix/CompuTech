using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Customers.Commands.DeactivateCustomerLocation;

public class DeactivateCustomerLocationCommandHandler(ICustomerLocationRepository repository)
    : IRequestHandler<DeactivateCustomerLocationCommand>
{
    public async Task Handle(DeactivateCustomerLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Ubicación con ID {request.Id} no encontrada.");

        location.Deactivate();
        repository.Update(location);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
