using CompuTech.Domain.Entities;
using CompuTech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuTech.Infrastructure.Persistence.Repositories;

public class ServiceOrderItemRepository : IServiceOrderItemRepository
{
    private readonly CompuTechDbContext _context;

    public ServiceOrderItemRepository(CompuTechDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceOrderItem?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _context.ServiceOrderItems.FindAsync([id], ct);

    public async Task<IReadOnlyList<ServiceOrderItem>> GetByServiceOrderIdAsync(int serviceOrderId, CancellationToken ct = default)
        => await _context.ServiceOrderItems
            .Where(x => x.ServiceOrderId == serviceOrderId)
            .ToListAsync(ct);

    public async Task AddAsync(ServiceOrderItem item, CancellationToken ct = default)
    {
        _context.ServiceOrderItems.Add(item);
        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveAsync(ServiceOrderItem item, CancellationToken ct = default)
    {
        _context.ServiceOrderItems.Remove(item);
        await _context.SaveChangesAsync(ct);
    }
}
