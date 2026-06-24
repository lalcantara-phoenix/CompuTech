using CompuTech.Domain.Entities;

namespace CompuTech.Domain.Interfaces;

public interface IEquipmentRepository
{
    Task<Equipment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Equipment>> GetByCustomerIdAsync(int customerId, CancellationToken ct = default);
    Task<Equipment?> GetBySerialNumberAsync(string serialNumber, CancellationToken ct = default);
    Task<IReadOnlyList<Equipment>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Equipment equipment, CancellationToken ct = default);
    Task UpdateAsync(Equipment equipment, CancellationToken ct = default);
}
