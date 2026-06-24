using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Technicians.Commands.UpdateTechnician;

public class UpdateTechnicianCommandHandler(ITechnicianRepository repository)
    : IRequestHandler<UpdateTechnicianCommand>
{
    public async Task Handle(UpdateTechnicianCommand request, CancellationToken ct)
    {
        var technician = await repository.GetByIdAsync(request.Id, ct)
            ?? throw new InvalidOperationException($"Technician with ID {request.Id} not found");

        technician.Update(request.FullName, request.Phone, request.Specialty);
        await repository.UpdateAsync(technician, ct);
    }
}
