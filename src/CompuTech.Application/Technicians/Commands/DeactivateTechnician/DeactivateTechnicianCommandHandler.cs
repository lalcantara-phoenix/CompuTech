using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Technicians.Commands.DeactivateTechnician;

public class DeactivateTechnicianCommandHandler(ITechnicianRepository repository)
    : IRequestHandler<DeactivateTechnicianCommand>
{
    public async Task Handle(DeactivateTechnicianCommand request, CancellationToken ct)
    {
        var technician = await repository.GetByIdAsync(request.Id, ct)
            ?? throw new InvalidOperationException($"Technician with ID {request.Id} not found");

        technician.Deactivate();
        await repository.UpdateAsync(technician, ct);
    }
}
