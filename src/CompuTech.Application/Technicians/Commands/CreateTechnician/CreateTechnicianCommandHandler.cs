using CompuTech.Domain.Entities;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Technicians.Commands.CreateTechnician;

public class CreateTechnicianCommandHandler(ITechnicianRepository repository)
    : IRequestHandler<CreateTechnicianCommand, int>
{
    public async Task<int> Handle(CreateTechnicianCommand request, CancellationToken ct)
    {
        var existing = await repository.GetByEmailAsync(request.Email, ct);
        if (existing is not null)
            throw new InvalidOperationException("Email already registered");

        var technician = Technician.Create(request.FullName, request.Email, request.Phone, request.Specialty);
        await repository.AddAsync(technician, ct);
        return technician.Id;
    }
}
