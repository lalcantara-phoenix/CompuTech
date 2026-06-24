using MediatR;

namespace CompuTech.Application.Technicians.Commands.UpdateTechnician;

public record UpdateTechnicianCommand(
    int Id,
    string FullName,
    string Phone,
    string Specialty) : IRequest;
