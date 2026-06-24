using CompuTech.Domain.Entities;

namespace CompuTech.Domain.Interfaces;

public interface IMaintenanceScheduleRepository
{
    Task<MaintenanceSchedule?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<MaintenanceSchedule?> GetByEquipmentIdAsync(int equipmentId, CancellationToken ct = default);
    Task<IReadOnlyList<MaintenanceSchedule>> GetDueAsync(DateTime referenceDate, CancellationToken ct = default);
    Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken ct = default);
    Task<int> AddAsync(MaintenanceSchedule schedule, CancellationToken ct = default);
    Task UpdateAsync(MaintenanceSchedule schedule, CancellationToken ct = default);
}
