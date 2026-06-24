using CompuTech.Domain.Entities;

namespace CompuTech.Domain.Interfaces;

public interface ICustomerLocationRepository
{
    Task<CustomerLocation?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CustomerLocation>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
    Task AddAsync(CustomerLocation location, CancellationToken cancellationToken = default);
    void Update(CustomerLocation location);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
