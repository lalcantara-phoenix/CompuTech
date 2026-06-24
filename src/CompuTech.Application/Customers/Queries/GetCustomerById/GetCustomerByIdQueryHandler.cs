using CompuTech.Application.Customers.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        => _customerRepository = customerRepository;

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (customer is null)
            return null;

        var locations = customer.Locations
            .Where(l => l.IsActive)
            .Select(l => new CustomerLocationDto(
                l.Id,
                l.CustomerId,
                l.Name,
                l.Address,
                l.City,
                l.ContactPhone,
                l.IsActive))
            .ToList();

        return new CustomerDto(
            customer.Id,
            customer.FullName,
            customer.Email,
            customer.Phone,
            customer.TaxId,
            customer.IsActive,
            customer.CreatedAt,
            locations);
    }
}
