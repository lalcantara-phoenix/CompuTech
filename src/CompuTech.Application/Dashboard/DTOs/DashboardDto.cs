namespace CompuTech.Application.Dashboard.DTOs;

public record DashboardDto(
    IReadOnlyList<OrderStatusCountDto> OrdersByStatus,
    IReadOnlyList<TechnicianWorkloadDto> TopTechnicians,
    IReadOnlyList<LowStockItemDto> LowStockItems,
    IReadOnlyList<DueScheduleDto> DueMaintenanceSchedules);

public record OrderStatusCountDto(string Status, int Count);

public record TechnicianWorkloadDto(int TechnicianId, string FullName, int ActiveOrderCount);

public record LowStockItemDto(int Id, string Name, string Sku, int Stock);

public record DueScheduleDto(int Id, int EquipmentId, string Frequency, DateTime NextServiceDate);
