using CompuTech.Application.Technicians.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Technicians.Queries.GetAllTechnicians;

public class GetAllTechniciansQueryHandler : IRequestHandler<GetAllTechniciansQuery, IReadOnlyList<TechnicianDto>>
{
    private readonly ITechnicianRepository _repository;

    public GetAllTechniciansQueryHandler(ITechnicianRepository repository)
        => _repository = repository;

    public async Task<IReadOnlyList<TechnicianDto>> Handle(GetAllTechniciansQuery request, CancellationToken ct)
    {
        var technicians = await _repository.GetAllAsync(ct);

        return technicians
            .Select(t => new TechnicianDto(
                t.Id,
                t.FullName,
                t.Email,
                t.Phone,
                t.Specialty,
                t.IsActive,
                t.CreatedAt))
            .ToList()
            .AsReadOnly();
    }
}
