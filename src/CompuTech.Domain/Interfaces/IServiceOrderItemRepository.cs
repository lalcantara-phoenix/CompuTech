using CompuTech.Domain.Entities;

namespace CompuTech.Domain.Interfaces;

public interface IServiceOrderItemRepository
{
    Task<ServiceOrderItem?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrderItem>> GetByServiceOrderIdAsync(int serviceOrderId, CancellationToken ct = default);
    Task AddAsync(ServiceOrderItem item, CancellationToken ct = default);
    Task RemoveAsync(ServiceOrderItem item, CancellationToken ct = default);
}
