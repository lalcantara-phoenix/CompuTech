using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Customers.Commands.UpdateCustomerLocation;

public class UpdateCustomerLocationCommandHandler(ICustomerLocationRepository repository)
    : IRequestHandler<UpdateCustomerLocationCommand>
{
    public async Task Handle(UpdateCustomerLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Ubicación con ID {request.Id} no encontrada.");

        location.Update(request.Name, request.Address, request.City, request.ContactPhone);
        repository.Update(location);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
