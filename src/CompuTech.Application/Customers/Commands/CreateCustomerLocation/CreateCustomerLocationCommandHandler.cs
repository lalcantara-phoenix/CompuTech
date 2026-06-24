using CompuTech.Domain.Entities;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Customers.Commands.CreateCustomerLocation;

public class CreateCustomerLocationCommandHandler(
    ICustomerRepository customerRepository,
    ICustomerLocationRepository locationRepository)
    : IRequestHandler<CreateCustomerLocationCommand, int>
{
    public async Task<int> Handle(CreateCustomerLocationCommand request, CancellationToken cancellationToken)
    {
        var customerExists = await customerRepository.ExistsAsync(request.CustomerId, cancellationToken);
        if (!customerExists)
            throw new KeyNotFoundException($"Cliente con ID {request.CustomerId} no encontrado.");

        var location = CustomerLocation.Create(
            request.CustomerId, request.Name, request.Address, request.City, request.ContactPhone);

        await locationRepository.AddAsync(location, cancellationToken);
        await locationRepository.SaveChangesAsync(cancellationToken);
        return location.Id;
    }
}
