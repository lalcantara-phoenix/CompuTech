using CompuTech.Application.Technicians.DTOs;
using MediatR;

namespace CompuTech.Application.Technicians.Queries.GetActiveTechnicians;

public record GetActiveTechniciansQuery : IRequest<IReadOnlyList<TechnicianDto>>;
