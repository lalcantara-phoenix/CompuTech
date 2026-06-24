using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;

namespace CompuTech.Domain.Interfaces;

public interface IServiceOrderRepository
{
    Task<ServiceOrder?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ServiceOrder?> GetByNumberAsync(string orderNumber, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrder>> GetByEquipmentIdAsync(int equipmentId, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrder>> GetByTechnicianIdAsync(int technicianId, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrder>> GetByStatusAsync(ServiceOrderStatus status, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceOrder>> GetAllAsync(CancellationToken ct = default);
    Task<int> GetNextSequenceAsync(int year, CancellationToken ct = default);
    Task<int> AddAsync(ServiceOrder order, CancellationToken ct = default);
    Task UpdateAsync(ServiceOrder order, CancellationToken ct = default);
}
