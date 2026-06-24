using CompuTech.Domain.Entities;
using CompuTech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuTech.Infrastructure.Persistence.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly CompuTechDbContext _context;

    public EquipmentRepository(CompuTechDbContext context)
    {
        _context = context;
    }

    public async Task<Equipment?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Equipments
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<IReadOnlyList<Equipment>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default)
    {
        return await _context.Equipments
            .Where(e => e.CustomerId == customerId)
            .ToListAsync(ct);
    }

    public async Task<Equipment?> GetBySerialNumberAsync(string serialNumber, CancellationToken ct = default)
    {
        return await _context.Equipments
            .FirstOrDefaultAsync(e => e.SerialNumber.ToLower() == serialNumber.ToLower(), ct);
    }

    public async Task<IReadOnlyList<Equipment>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Equipments
            .ToListAsync(ct);
    }

    public async Task AddAsync(Equipment equipment, CancellationToken ct = default)
    {
        _context.Equipments.Add(equipment);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Equipment equipment, CancellationToken ct = default)
    {
        _context.Equipments.Update(equipment);
        await _context.SaveChangesAsync(ct);
    }
}
