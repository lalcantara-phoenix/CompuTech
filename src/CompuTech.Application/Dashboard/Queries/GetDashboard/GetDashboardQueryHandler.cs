using CompuTech.Application.Dashboard.DTOs;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using MediatR;

namespace CompuTech.Application.Dashboard.Queries.GetDashboard;

public class GetDashboardQueryHandler(
    IServiceOrderRepository serviceOrderRepository,
    ITechnicianRepository technicianRepository,
    IInventoryItemRepository inventoryItemRepository,
    IMaintenanceScheduleRepository maintenanceScheduleRepository)
    : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private const int LowStockThreshold = 5;
    private const int TopTechniciansCount = 5;
    private const int DueWithinDays = 30;

    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var statusCountsTask = serviceOrderRepository.GetCountsByStatusAsync(cancellationToken);
        var topTechniciansTask = technicianRepository.GetTopByActiveOrdersAsync(TopTechniciansCount, cancellationToken);
        var lowStockItemsTask = inventoryItemRepository.GetBelowStockThresholdAsync(LowStockThreshold, cancellationToken);
        var dueSchedulesTask = maintenanceScheduleRepository.GetDueAsync(DateTime.UtcNow.AddDays(DueWithinDays), cancellationToken);

        await Task.WhenAll(statusCountsTask, topTechniciansTask, lowStockItemsTask, dueSchedulesTask);

        var statusCounts = await statusCountsTask;
        var topTechnicians = await topTechniciansTask;
        var lowStockItems = await lowStockItemsTask;
        var dueSchedules = await dueSchedulesTask;

        var allStatuses = Enum.GetValues<ServiceOrderStatus>();
        var countsByStatus = allStatuses
            .Select(s => new OrderStatusCountDto(
                s.ToString(),
                statusCounts.FirstOrDefault(x => x.Status == s).Count))
            .ToList();

        return new DashboardDto(
            OrdersByStatus: countsByStatus,
            TopTechnicians: topTechnicians
                .Select(t => new TechnicianWorkloadDto(t.TechnicianId, t.FullName, t.ActiveOrderCount))
                .ToList(),
            LowStockItems: lowStockItems
                .Select(i => new LowStockItemDto(i.Id, i.Name, i.SKU, i.StockQuantity))
                .ToList(),
            DueMaintenanceSchedules: dueSchedules
                .Select(s => new DueScheduleDto(s.Id, s.EquipmentId, s.Frequency.ToString(), s.NextServiceDate))
                .ToList());
    }
}
