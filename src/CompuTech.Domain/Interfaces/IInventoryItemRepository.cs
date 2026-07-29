using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;

namespace CompuTech.Domain.Interfaces;

public interface IInventoryItemRepository
{
    Task<InventoryItem?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<InventoryItem?> GetBySkuAsync(string sku, CancellationToken ct = default);
    Task<IReadOnlyList<InventoryItem>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<InventoryItem>> GetByTypeAsync(InventoryItemType type, CancellationToken ct = default);
    Task<IReadOnlyList<InventoryItem>> GetByCategoryAsync(InventoryCategory category, CancellationToken ct = default);
    Task<IReadOnlyList<InventoryItem>> GetBelowStockThresholdAsync(int threshold, CancellationToken ct = default);
    Task AddAsync(InventoryItem item, CancellationToken ct = default);
    Task UpdateAsync(InventoryItem item, CancellationToken ct = default);
}
