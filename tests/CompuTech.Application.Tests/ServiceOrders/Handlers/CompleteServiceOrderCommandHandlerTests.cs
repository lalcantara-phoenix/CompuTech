using CompuTech.Application.Common.Interfaces;
using CompuTech.Application.ServiceOrders.Commands.CompleteServiceOrder;
using CompuTech.Domain.Entities;
using CompuTech.Domain.Enums;
using CompuTech.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace CompuTech.Application.Tests.ServiceOrders.Handlers;

public class CompleteServiceOrderCommandHandlerTests
{
    private readonly Mock<IServiceOrderRepository> _orderRepo = new();
    private readonly Mock<IGenerateNextMaintenanceOrderService> _generateService = new();
    private readonly CompleteServiceOrderCommandHandler _handler;

    public CompleteServiceOrderCommandHandlerTests()
    {
        _handler = new CompleteServiceOrderCommandHandler(_orderRepo.Object, _generateService.Object);
    }

    private static ServiceOrder CreateInProgressOrder(
        ServiceOrderSubType subType = ServiceOrderSubType.Hardware,
        int? scheduleId = null)
    {
        var order = ServiceOrder.Create(
            "ORD-2026-0001", 1, 1,
            ServiceOrderType.Repair, subType,
            ServiceOrderPriority.Medium, "Test order",
            scheduleId: scheduleId);
        order.Start();
        return order;
    }

    [Fact]
    public async Task Handle_InProgressOrder_CompletesSuccessfully()
    {
        var order = CreateInProgressOrder();
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);
        _orderRepo.Setup(r => r.UpdateAsync(order, default)).Returns(Task.CompletedTask);
        _generateService.Setup(s => s.GenerateAsync(It.IsAny<ServiceOrder>(), default)).Returns(Task.CompletedTask);

        await _handler.Handle(new CompleteServiceOrderCommand(1, "Fixed"), default);

        order.Status.Should().Be(ServiceOrderStatus.Completed);
        order.CompletedAt.Should().NotBeNull();
        order.ResolutionNotes.Should().Be("Fixed");
    }

    [Fact]
    public async Task Rule8_PreventiveOrder_CallsGenerateNextMaintenanceOrder()
    {
        var order = ServiceOrder.Create(
            "ORD-2026-0001", 1, 1,
            ServiceOrderType.Maintenance, ServiceOrderSubType.Preventive,
            ServiceOrderPriority.Medium, "Preventive maintenance");
        order.Start();

        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);
        _orderRepo.Setup(r => r.UpdateAsync(order, default)).Returns(Task.CompletedTask);
        _generateService.Setup(s => s.GenerateAsync(order, default)).Returns(Task.CompletedTask);

        await _handler.Handle(new CompleteServiceOrderCommand(1), default);

        _generateService.Verify(s => s.GenerateAsync(order, default), Times.Once);
    }

    [Fact]
    public async Task Rule8_RepairOrder_DoesNotCallGenerateNextMaintenanceOrder()
    {
        var order = CreateInProgressOrder(ServiceOrderSubType.Hardware);
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);
        _orderRepo.Setup(r => r.UpdateAsync(order, default)).Returns(Task.CompletedTask);

        await _handler.Handle(new CompleteServiceOrderCommand(1), default);

        _generateService.Verify(s => s.GenerateAsync(It.IsAny<ServiceOrder>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_PlannedOrder_ThrowsInvalidOperationException()
    {
        var order = ServiceOrder.Create(
            "ORD-2026-0001", 1, 1,
            ServiceOrderType.Repair, ServiceOrderSubType.Hardware,
            ServiceOrderPriority.Medium, "Test order");
        _orderRepo.Setup(r => r.GetByIdAsync(1, default)).ReturnsAsync(order);

        var act = () => _handler.Handle(new CompleteServiceOrderCommand(1), default);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*InProgress*");
    }

    [Fact]
    public async Task Handle_OrderNotFound_ThrowsKeyNotFoundException()
    {
        _orderRepo.Setup(r => r.GetByIdAsync(99, default)).ReturnsAsync((ServiceOrder?)null);

        var act = () => _handler.Handle(new CompleteServiceOrderCommand(99), default);

        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
