using CompuTech.Application.Customers.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IReadOnlyList<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        => _customerRepository = customerRepository;

    public async Task<IReadOnlyList<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        return customers
            .Select(c => new CustomerDto(
                c.Id,
                c.FullName,
                c.Email,
                c.Phone,
                c.TaxId,
                c.IsActive,
                c.CreatedAt,
                new List<CustomerLocationDto>()))
            .ToList()
            .AsReadOnly();
    }
}
