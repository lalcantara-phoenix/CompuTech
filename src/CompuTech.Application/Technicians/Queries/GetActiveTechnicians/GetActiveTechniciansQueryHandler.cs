using CompuTech.Application.Technicians.DTOs;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Technicians.Queries.GetActiveTechnicians;

public class GetActiveTechniciansQueryHandler : IRequestHandler<GetActiveTechniciansQuery, IReadOnlyList<TechnicianDto>>
{
    private readonly ITechnicianRepository _repository;

    public GetActiveTechniciansQueryHandler(ITechnicianRepository repository)
        => _repository = repository;

    public async Task<IReadOnlyList<TechnicianDto>> Handle(GetActiveTechniciansQuery request, CancellationToken ct)
    {
        var technicians = await _repository.GetActiveAsync(ct);

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
