using CompuTech.Application.Technicians.DTOs;
using MediatR;

namespace CompuTech.Application.Technicians.Queries.GetAllTechnicians;

public record GetAllTechniciansQuery : IRequest<IReadOnlyList<TechnicianDto>>;
