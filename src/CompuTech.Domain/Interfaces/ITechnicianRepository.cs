using CompuTech.Domain.Entities;

namespace CompuTech.Domain.Interfaces;

public interface ITechnicianRepository
{
    Task<Technician?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Technician?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<IReadOnlyList<Technician>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Technician>> GetActiveAsync(CancellationToken ct = default);
    Task AddAsync(Technician technician, CancellationToken ct = default);
    Task UpdateAsync(Technician technician, CancellationToken ct = default);
}
