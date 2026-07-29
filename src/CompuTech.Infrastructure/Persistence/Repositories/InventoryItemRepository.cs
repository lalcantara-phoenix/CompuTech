using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuTech.Infrastructure.Persistence.Repositories;

public class InventoryItemRepository : IInventoryItemRepository
{
    private readonly CompuTechDbContext _context;

    public InventoryItemRepository(CompuTechDbContext context)
    {
        _context = context;
    }

    public async Task<InventoryItem?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.InventoryItems
            .FirstOrDefaultAsync(i => i.Id == id, ct);
    }

    public async Task<InventoryItem?> GetBySkuAsync(string sku, CancellationToken ct = default)
    {
        return await _context.InventoryItems
            .FirstOrDefaultAsync(i => i.SKU.ToLower() == sku.ToLower(), ct);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.InventoryItems
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetByTypeAsync(InventoryItemType type, CancellationToken ct = default)
    {
        return await _context.InventoryItems
            .Where(i => i.Type == type)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetByCategoryAsync(InventoryCategory category, CancellationToken ct = default)
    {
        return await _context.InventoryItems
            .Where(i => i.Category == category)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetBelowStockThresholdAsync(int threshold, CancellationToken ct = default)
    {
        return await _context.InventoryItems
            .Where(i => i.IsActive && i.StockQuantity <= threshold)
            .OrderBy(i => i.StockQuantity)
            .ToListAsync(ct);
    }

    public async Task AddAsync(InventoryItem item, CancellationToken ct = default)
    {
        _context.InventoryItems.Add(item);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(InventoryItem item, CancellationToken ct = default)
    {
        _context.InventoryItems.Update(item);
        await _context.SaveChangesAsync(ct);
    }
}
