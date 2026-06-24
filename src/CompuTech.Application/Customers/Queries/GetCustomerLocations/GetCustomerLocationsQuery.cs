using CompuTech.Application.Customers.DTOs;
using MediatR;

namespace CompuTech.Application.Customers.Queries.GetCustomerLocations;

public record GetCustomerLocationsQuery(int CustomerId) : IRequest<IReadOnlyList<CustomerLocationDto>>;
