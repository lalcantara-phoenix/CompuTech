using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuTech.Infrastructure.Persistence.Repositories;

public class TechnicianRepository : ITechnicianRepository
{
    private readonly CompuTechDbContext _context;

    public TechnicianRepository(CompuTechDbContext context)
    {
        _context = context;
    }

    public async Task<Technician?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Technicians
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<Technician?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _context.Technicians
            .FirstOrDefaultAsync(t => t.Email.ToLower() == email.ToLower(), ct);
    }

    public async Task<IReadOnlyList<Technician>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Technicians
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<Technician>> GetActiveAsync(CancellationToken ct = default)
    {
        return await _context.Technicians
            .Where(t => t.IsActive)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Technician technician, CancellationToken ct = default)
    {
        _context.Technicians.Add(technician);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Technician technician, CancellationToken ct = default)
    {
        _context.Technicians.Update(technician);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<(int TechnicianId, string FullName, int ActiveOrderCount)>> GetTopByActiveOrdersAsync(int top, CancellationToken ct = default)
    {
        var activeStatuses = new[] { ServiceOrderStatus.Planned, ServiceOrderStatus.InProgress };

        var results = await _context.Technicians
            .Select(t => new
            {
                t.Id,
                t.FullName,
                ActiveOrderCount = _context.ServiceOrders.Count(o => o.TechnicianId == t.Id && activeStatuses.Contains(o.Status))
            })
            .OrderByDescending(t => t.ActiveOrderCount)
            .Take(top)
            .ToListAsync(ct);

        return results.Select(r => (r.Id, r.FullName, r.ActiveOrderCount)).ToList();
    }
}
