using CompuTech.Application.Common.Interfaces;
using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;

namespace CompuTech.Application.MaintenanceSchedules.Services;

public class GenerateNextMaintenanceOrderService(
    IMaintenanceScheduleRepository maintenanceScheduleRepository,
    IServiceOrderRepository serviceOrderRepository)
    : IGenerateNextMaintenanceOrderService
{
    public async Task GenerateAsync(ServiceOrder completedOrder, CancellationToken ct = default)
    {
        if (completedOrder.ScheduleId is null) return;

        var schedule = await maintenanceScheduleRepository.GetByIdAsync(completedOrder.ScheduleId.Value, ct);
        if (schedule is null || !schedule.IsActive) return;

        var completedAt = completedOrder.CompletedAt ?? DateTime.UtcNow;
        schedule.RecordServiceCompletion(completedAt);
        await maintenanceScheduleRepository.UpdateAsync(schedule, ct);

        var year = schedule.NextServiceDate.Year;
        var sequence = await serviceOrderRepository.GetNextSequenceAsync(year, ct);
        var orderNumber = ServiceOrder.GenerateOrderNumber(year, sequence);

        var nextOrder = ServiceOrder.Create(
            orderNumber,
            completedOrder.EquipmentId,
            completedOrder.TechnicianId,
            ServiceOrderType.Maintenance,
            ServiceOrderSubType.Preventive,
            ServiceOrderPriority.Medium,
            $"Scheduled preventive maintenance (auto-generated from order {completedOrder.OrderNumber})",
            schedule.NextServiceDate,
            schedule.Id);

        await serviceOrderRepository.AddAsync(nextOrder, ct);
    }
}
