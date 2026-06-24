using CompuTech.Application.MaintenanceSchedules.Services;
using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using MaintenanceScheduleEntity = CompuTech.Domain.Entities.MaintenanceSchedule;

namespace CompuTech.Application.Tests.MaintenanceSchedules.Services;

public class GenerateNextMaintenanceOrderServiceTests
{
    private readonly Mock<IMaintenanceScheduleRepository> _scheduleRepo = new();
    private readonly Mock<IServiceOrderRepository> _orderRepo = new();
    private readonly GenerateNextMaintenanceOrderService _service;

    public GenerateNextMaintenanceOrderServiceTests()
    {
        _service = new GenerateNextMaintenanceOrderService(_scheduleRepo.Object, _orderRepo.Object);
    }

    private static ServiceOrder CompletedPreventiveOrder(int? scheduleId = 5)
    {
        var order = ServiceOrder.Create(
            "ORD-2026-0001", 1, 1,
            ServiceOrderType.Maintenance, ServiceOrderSubType.Preventive,
            ServiceOrderPriority.Medium, "Preventive maintenance",
            scheduleId: scheduleId);
        order.Start();
        order.Complete();
        return order;
    }

    private static MaintenanceScheduleEntity ActiveSchedule() =>
        MaintenanceScheduleEntity.Create(1, MaintenanceFrequency.Monthly, DateTime.UtcNow.AddMonths(1));

    [Fact]
    public async Task Rule8_ScheduleIdNull_DoesNotCreateNextOrder()
    {
        var order = CompletedPreventiveOrder(scheduleId: null);

        await _service.GenerateAsync(order);

        _scheduleRepo.Verify(r => r.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        _orderRepo.Verify(r => r.AddAsync(It.IsAny<ServiceOrder>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Rule8_ScheduleNotFound_DoesNotCreateNextOrder()
    {
        var order = CompletedPreventiveOrder(scheduleId: 5);
        _scheduleRepo.Setup(r => r.GetByIdAsync(5, default)).ReturnsAsync((MaintenanceScheduleEntity?)null);

        await _service.GenerateAsync(order);

        _orderRepo.Verify(r => r.AddAsync(It.IsAny<ServiceOrder>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Rule8_ScheduleInactive_DoesNotCreateNextOrder()
    {
        var order = CompletedPreventiveOrder(scheduleId: 5);
        var inactiveSchedule = ActiveSchedule();
        inactiveSchedule.Deactivate();
        _scheduleRepo.Setup(r => r.GetByIdAsync(5, default)).ReturnsAsync(inactiveSchedule);

        await _service.GenerateAsync(order);

        _orderRepo.Verify(r => r.AddAsync(It.IsAny<ServiceOrder>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Rule8_ActiveSchedule_CreatesNextPreventiveOrder()
    {
        var order = CompletedPreventiveOrder(scheduleId: 5);
        var schedule = ActiveSchedule();

        _scheduleRepo.Setup(r => r.GetByIdAsync(5, default)).ReturnsAsync(schedule);
        _scheduleRepo.Setup(r => r.UpdateAsync(schedule, default)).Returns(Task.CompletedTask);
        _orderRepo.Setup(r => r.GetNextSequenceAsync(It.IsAny<int>(), default)).ReturnsAsync(2);
        _orderRepo.Setup(r => r.AddAsync(It.IsAny<ServiceOrder>(), default)).ReturnsAsync(10);

        await _service.GenerateAsync(order);

        _orderRepo.Verify(r => r.AddAsync(
            It.Is<ServiceOrder>(o =>
                o.SubType == ServiceOrderSubType.Preventive &&
                o.Status == ServiceOrderStatus.Planned &&
                o.Type == ServiceOrderType.Maintenance),
            default), Times.Once);
    }

    [Fact]
    public async Task Rule8_AfterCompletion_ScheduleNextServiceDateAdvancesByFrequency()
    {
        var order = CompletedPreventiveOrder(scheduleId: 5);
        var schedule = ActiveSchedule(); // Monthly = 1 month
        var serviceDate = order.CompletedAt ?? DateTime.UtcNow;
        var expectedNextDate = serviceDate.AddMonths(1);

        _scheduleRepo.Setup(r => r.GetByIdAsync(5, default)).ReturnsAsync(schedule);
        _scheduleRepo.Setup(r => r.UpdateAsync(schedule, default)).Returns(Task.CompletedTask);
        _orderRepo.Setup(r => r.GetNextSequenceAsync(It.IsAny<int>(), default)).ReturnsAsync(2);
        _orderRepo.Setup(r => r.AddAsync(It.IsAny<ServiceOrder>(), default)).ReturnsAsync(10);

        await _service.GenerateAsync(order);

        schedule.LastServiceDate.Should().NotBeNull();
        schedule.NextServiceDate.Should().BeCloseTo(expectedNextDate, TimeSpan.FromSeconds(5));
    }
}
