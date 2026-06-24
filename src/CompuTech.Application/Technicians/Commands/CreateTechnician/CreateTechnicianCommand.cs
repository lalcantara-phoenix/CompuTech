using MediatR;

namespace CompuTech.Application.Technicians.Commands.CreateTechnician;

public record CreateTechnicianCommand(
    string FullName,
    string Email,
    string Phone,
    string Specialty) : IRequest<int>;
