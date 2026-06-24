using CompuTech.Application.Technicians.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Technicians.Queries.GetTechnicianById;

public class GetTechnicianByIdQueryHandler : IRequestHandler<GetTechnicianByIdQuery, TechnicianDto?>
{
    private readonly ITechnicianRepository _repository;

    public GetTechnicianByIdQueryHandler(ITechnicianRepository repository)
        => _repository = repository;

    public async Task<TechnicianDto?> Handle(GetTechnicianByIdQuery request, CancellationToken ct)
    {
        var technician = await _repository.GetByIdAsync(request.Id, ct);

        if (technician is null)
            return null;

        return new TechnicianDto(
            technician.Id,
            technician.FullName,
            technician.Email,
            technician.Phone,
            technician.Specialty,
            technician.IsActive,
            technician.CreatedAt);
    }
}
