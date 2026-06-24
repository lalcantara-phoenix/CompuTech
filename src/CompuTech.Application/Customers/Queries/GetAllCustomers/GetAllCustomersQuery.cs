using CompuTech.Application.Customers.DTOs;
using MediatR;

namespace CompuTech.Application.Customers.Queries.GetAllCustomers;

public record GetAllCustomersQuery : IRequest<IReadOnlyList<CustomerDto>>;
