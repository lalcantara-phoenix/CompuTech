using MediatR;

namespace CompuTech.Application.Technicians.Commands.DeactivateTechnician;

public record DeactivateTechnicianCommand(int Id) : IRequest;
