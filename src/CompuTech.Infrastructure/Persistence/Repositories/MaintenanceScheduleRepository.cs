using CompuTech.Domain.Entities;
using CompuTech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuTech.Infrastructure.Persistence.Repositories;

public class MaintenanceScheduleRepository : IMaintenanceScheduleRepository
{
    private readonly CompuTechDbContext _context;

    public MaintenanceScheduleRepository(CompuTechDbContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceSchedule?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _context.MaintenanceSchedules.FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<MaintenanceSchedule?> GetByEquipmentIdAsync(int equipmentId, CancellationToken ct = default)
        => await _context.MaintenanceSchedules.FirstOrDefaultAsync(x => x.EquipmentId == equipmentId, ct);

    public async Task<IReadOnlyList<MaintenanceSchedule>> GetDueAsync(DateTime referenceDate, CancellationToken ct = default)
        => await _context.MaintenanceSchedules
            .Where(x => x.IsActive && x.NextServiceDate <= referenceDate)
            .OrderBy(x => x.NextServiceDate)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken ct = default)
        => await _context.MaintenanceSchedules
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);

    public async Task<int> AddAsync(MaintenanceSchedule schedule, CancellationToken ct = default)
    {
        _context.MaintenanceSchedules.Add(schedule);
        await _context.SaveChangesAsync(ct);
        return schedule.Id;
    }

    public async Task UpdateAsync(MaintenanceSchedule schedule, CancellationToken ct = default)
    {
        _context.MaintenanceSchedules.Update(schedule);
        await _context.SaveChangesAsync(ct);
    }
}
