using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuTech.Infrastructure.Persistence.Repositories;

public class ServiceOrderRepository : IServiceOrderRepository
{
    private readonly CompuTechDbContext _context;

    public ServiceOrderRepository(CompuTechDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceOrder?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _context.ServiceOrders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<ServiceOrder?> GetByNumberAsync(string orderNumber, CancellationToken ct = default)
        => await _context.ServiceOrders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.OrderNumber == orderNumber, ct);

    public async Task<IReadOnlyList<ServiceOrder>> GetByEquipmentIdAsync(int equipmentId, CancellationToken ct = default)
        => await _context.ServiceOrders
            .Where(x => x.EquipmentId == equipmentId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<ServiceOrder>> GetByTechnicianIdAsync(int technicianId, CancellationToken ct = default)
        => await _context.ServiceOrders
            .Where(x => x.TechnicianId == technicianId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<ServiceOrder>> GetByStatusAsync(ServiceOrderStatus status, CancellationToken ct = default)
        => await _context.ServiceOrders
            .Where(x => x.Status == status)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<ServiceOrder>> GetAllAsync(CancellationToken ct = default)
        => await _context.ServiceOrders
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);

    public async Task<int> GetNextSequenceAsync(int year, CancellationToken ct = default)
    {
        var prefix = $"ORD-{year}-";
        var orderNumbers = await _context.ServiceOrders
            .Where(x => x.OrderNumber.StartsWith(prefix))
            .Select(x => x.OrderNumber)
            .ToListAsync(ct);

        if (orderNumbers.Count == 0) return 1;

        var maxSeq = orderNumbers
            .Select(n => int.TryParse(n.Substring(prefix.Length), out var seq) ? seq : 0)
            .Max();

        return maxSeq + 1;
    }

    public async Task<int> AddAsync(ServiceOrder order, CancellationToken ct = default)
    {
        _context.ServiceOrders.Add(order);
        await _context.SaveChangesAsync(ct);
        return order.Id;
    }

    public async Task UpdateAsync(ServiceOrder order, CancellationToken ct = default)
    {
        _context.ServiceOrders.Update(order);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<(ServiceOrderStatus Status, int Count)>> GetCountsByStatusAsync(CancellationToken ct = default)
    {
        var results = await _context.ServiceOrders
            .GroupBy(x => x.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync(ct);

        return results.Select(r => (r.Status, r.Count)).ToList();
    }
}
