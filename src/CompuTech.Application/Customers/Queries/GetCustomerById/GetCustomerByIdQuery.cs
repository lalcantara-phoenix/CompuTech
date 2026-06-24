using CompuTech.Application.Customers.DTOs;
using MediatR;

namespace CompuTech.Application.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(int Id) : IRequest<CustomerDto?>;
