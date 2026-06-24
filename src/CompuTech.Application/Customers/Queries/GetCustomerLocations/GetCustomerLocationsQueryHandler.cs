using CompuTech.Application.Customers.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Customers.Queries.GetCustomerLocations;

public class GetCustomerLocationsQueryHandler : IRequestHandler<GetCustomerLocationsQuery, IReadOnlyList<CustomerLocationDto>>
{
    private readonly ICustomerLocationRepository _locationRepository;

    public GetCustomerLocationsQueryHandler(ICustomerLocationRepository locationRepository)
        => _locationRepository = locationRepository;

    public async Task<IReadOnlyList<CustomerLocationDto>> Handle(GetCustomerLocationsQuery request, CancellationToken cancellationToken)
    {
        var locations = await _locationRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

        return locations
            .Where(l => l.IsActive)
            .Select(l => new CustomerLocationDto(
                l.Id,
                l.CustomerId,
                l.Name,
                l.Address,
                l.City,
                l.ContactPhone,
                l.IsActive))
            .ToList()
            .AsReadOnly();
    }
}
