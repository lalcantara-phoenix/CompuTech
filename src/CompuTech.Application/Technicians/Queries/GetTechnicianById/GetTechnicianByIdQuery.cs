using CompuTech.Application.Technicians.DTOs;
using MediatR;

namespace CompuTech.Application.Technicians.Queries.GetTechnicianById;

public record GetTechnicianByIdQuery(int Id) : IRequest<TechnicianDto?>;
